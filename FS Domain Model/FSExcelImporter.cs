using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace FitnessTool
{
    internal class FSExcelImporter
    {
        public static async Task ImportAfcdAsync(string filePath, FoodDbContext db, string worksheetName, CancellationToken ct = default)
        {
            await ImportGenericAsync<AfcdFoodEntryRaw>(
                db, filePath, worksheetName, db.AfcdFoods,
                FSExcelMapper.MapAfcdRow,
                FSExcelMapper.GetUnmappedHeadersForAfcd, ct);
        }

        public static async Task ImportAusnutNutrientsAsync(string filePath, FoodDbContext db, string worksheetName, CancellationToken ct = default)
        {
            await ImportGenericAsync<AusnutFoodEntryRaw>(
                db, filePath, worksheetName, db.AusnutFoods,
                FSExcelMapper.MapAusnutRow,
                FSExcelMapper.GetUnmappedHeadersForAusnut, ct);
        }

        public static async Task ImportAusnutMetadataAsync(string filePath, FoodDbContext db, string worksheetName, CancellationToken ct = default)
        {
            await ImportGenericAsync<AusnutFoodMetadataRaw>(
                db, filePath, worksheetName, db.AusnutFoodMetadata,
                (row) => FSExcelMapper.MapByConvention<AusnutFoodMetadataRaw>(row, new Dictionary<string, string>()),
                (headers) => FSExcelMapper.GetUnmappedHeaders<AusnutFoodMetadataRaw>(headers, new Dictionary<string, string>()),
                ct);
        }

        private static async Task ImportGenericAsync<T>(
    FoodDbContext db,
    string filePath,
    string worksheetName,
    DbSet<T> dbSet,
    Func<IDictionary<string, object?>, T> mapFunc,
    Func<IEnumerable<string>, List<string>> unmappedCheckFunc,
    CancellationToken ct) where T : class, new()
        {
            // 1. DISABLE FOREIGN KEY CHECKS
            // This prevents the "Foreign Key constraint failed" error if the 
            // Excel file references a code that isn't in the lookup table.
            await db.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = OFF;");

            try
            {
                string cleanedPath = filePath.Trim('"');
                using var workbook = new XLWorkbook(cleanedPath);

                IXLWorksheet worksheet;
                if (!string.IsNullOrEmpty(worksheetName) && workbook.Worksheets.Contains(worksheetName))
                    worksheet = workbook.Worksheet(worksheetName);
                else if (workbook.Worksheets.Count >= 2)
                    worksheet = workbook.Worksheet(2);
                else
                    worksheet = workbook.Worksheet(1);

                var usedRange = worksheet.RangeUsed();
                if (usedRange == null) throw new Exception($"Worksheet empty in {filePath}");

                // --- Header Hunting ---
                int headerRowNumber = 0;
                for (int r = 1; r <= 10; r++)
                {
                    var cells = worksheet.Row(r).Cells().Select(c => c.GetString()).ToList();
                    if (cells.Any(c => c.Contains("Key", StringComparison.OrdinalIgnoreCase) ||
                                       c.Contains("Code", StringComparison.OrdinalIgnoreCase) ||
                                       c.Contains("Name", StringComparison.OrdinalIgnoreCase)))
                    {
                        headerRowNumber = r;
                        break;
                    }
                }
                if (headerRowNumber == 0) throw new Exception($"Header not found in {worksheetName}");

                var headerRow = worksheet.Row(headerRowNumber);
                var lastCol = worksheet.RangeUsed().LastColumnUsed().ColumnNumber();
                var lastRow = worksheet.RangeUsed().LastRowUsed().RowNumber();

                var headers = new List<string>();
                for (int col = 1; col <= lastCol; col++)
                    headers.Add(headerRow.Cell(col).GetString());

                var primaryKeyName = db.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(x => x.Name).FirstOrDefault();
                var uniqueEntries = new Dictionary<object, T>();

                for (int rowNum = headerRowNumber + 1; rowNum <= lastRow; rowNum++)
                {
                    ct.ThrowIfCancellationRequested();
                    var row = worksheet.Row(rowNum);
                    var rowData = new Dictionary<string, object?>();
                    for (int col = 1; col <= lastCol; col++)
                        rowData[headers[col - 1]] = row.Cell(col).Value;

                    try
                    {
                        var entity = mapFunc(rowData);
                        var pkValue = typeof(T).GetProperty(primaryKeyName)?.GetValue(entity);
                        if (pkValue == null || (pkValue is string s && string.IsNullOrWhiteSpace(s)) || (pkValue is int i && i == 0))
                            continue;
                        uniqueEntries[pkValue] = entity;
                    }
                    catch { }
                }

                // Clear and Insert
                dbSet.RemoveRange(dbSet);
                await db.SaveChangesAsync(ct);

                var finalEntries = uniqueEntries.Values.ToList();
                await dbSet.AddRangeAsync(finalEntries, ct);
                await db.SaveChangesAsync(ct);
            }
            finally
            {
                // 2. RE-ENABLE FOREIGN KEY CHECKS
                // Always re-enable them so your domain logic stays safe.
                await db.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = ON;");
            }
        }
    }
}
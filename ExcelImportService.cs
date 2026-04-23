using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public class ExcelImportService
    {

        public static async Task ImportAfcdAsync(string filePath, FoodDbContext db, CancellationToken ct = default)
        {
            // Added 'db' as the first argument
            await ImportGenericAsync<AfcdFoodEntryRaw>(
                db,
                filePath,
                db.AfcdFoods,
                ExcelSpreadsheetMapper.MapAfcdRow,
                ExcelSpreadsheetMapper.GetUnmappedHeadersForAfcd,
                ct);
        }

        public static async Task ImportAusnutNutrientsAsync(string filePath, FoodDbContext db, CancellationToken ct = default)
        {
            // Added 'db' as the first argument
            await ImportGenericAsync<AusnutFoodEntryRaw>(
                db,
                filePath,
                db.AusnutFoods,
                ExcelSpreadsheetMapper.MapAusnutRow,
                ExcelSpreadsheetMapper.GetUnmappedHeadersForAusnut,
                ct);
        }

        public static async Task ImportAusnutMetadataAsync(string filePath, FoodDbContext db, CancellationToken ct = default)
        {
            // Added 'db' as the first argument
            await ImportGenericAsync<AusnutFoodMetadataRaw>(
                db,
                filePath,
                db.AusnutFoodMetadata,
                (row) => ExcelSpreadsheetMapper.MapByConvention<AusnutFoodMetadataRaw>(row, new Dictionary<string, string>()),
                (headers) => ExcelSpreadsheetMapper.GetUnmappedHeaders<AusnutFoodMetadataRaw>(headers, new Dictionary<string, string>()),
                ct);
        }

        private static async Task ImportGenericAsync<T>(
            FoodDbContext db,           // 1. DbContext passed in here
            string filePath,            // 2.
            DbSet<T> dbSet,             // 3.
            Func<IDictionary<string, object?>, T> mapFunc, // 4.
            Func<IEnumerable<string>, List<string>> unmappedCheckFunc, // 5.
            CancellationToken ct)        // 6.
            where T : class, new()
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var usedRange = worksheet.RangeUsed();

            if (usedRange == null) throw new Exception($"The file {filePath} appears to be empty.");

            var headerRow = usedRange.FirstRowUsed();
            var lastRow = usedRange.LastRowUsed().RowNumber();
            var lastCol = usedRange.LastColumnUsed().ColumnNumber();

            // 1. Extract Headers
            var headers = new List<string>();
            for (int col = 1; col <= lastCol; col++)
            {
                headers.Add(headerRow.Cell(col).GetString());
            }

            // 2. Validation
            var unmapped = unmappedCheckFunc(headers);
            if (unmapped.Any())
            {
                Console.WriteLine($"Warning: The following headers in {filePath} were not mapped: {string.Join(", ", unmapped)}");
            }

            // 3. Process Rows
            var entries = new List<T>();
            for (int rowNum = headerRow.RowNumber() + 1; rowNum <= lastRow; rowNum++)
            {
                ct.ThrowIfCancellationRequested();
                var row = worksheet.Row(rowNum);

                var rowData = new Dictionary<string, object?>();
                for (int col = 1; col <= lastCol; col++)
                {
                    var header = headers[col - 1];
                    var value = row.Cell(col).Value;
                    rowData[header] = value;
                }

                try
                {
                    var entity = mapFunc(rowData);
                    entries.Add(entity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error mapping row {rowNum} in {filePath}: {ex.Message}");
                }
            }

            // 4. Bulk Insert
            await dbSet.AddRangeAsync(entries, ct);
            await db.SaveChangesAsync(ct); // Now 'db' is available from the parameter list
        }
    }
}

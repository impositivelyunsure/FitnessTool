using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace FitnessTool
{
    internal class FSClassificationImporter
    {
        public static async Task ImportFoodClassificationLookupAsync(string filePath, FoodDbContext db, string? worksheetName = null, CancellationToken cancellationToken = default)
        {
            string cleanedPath = filePath.Trim('"');
            using var workbook = new XLWorkbook(cleanedPath);

            // ternary operator establishing if the given worksheet name is valid
            // if null, grab the first sheet
            // if not null, grab the sheet with the given name
            var worksheet = worksheetName is null ? workbook.Worksheet(1) : workbook.Worksheet(worksheetName); 

            
            var rows = ReadLookupRows(worksheet).ToList();

            if (!rows.Any())
            {
                throw new InvalidOperationException($"No data rows were found in {cleanedPath}.");
            }

            // Use lists to collect items for batch insertion
            var groups = new List<FoodGroup>();
            var subgroups = new List<FoodSubgroup>();
            var classifications = new List<FoodClassification>();

            // Single pass through the data to categorize by hierarchy (Optimization)
            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row.Code)) continue;

                if (row.Code.Length == 2)
                {
                    groups.Add(new FoodGroup { foodGroupID = row.Code, foodGroupName = row.Name });
                }
                else if (row.Code.Length == 3)
                {
                    subgroups.Add(new FoodSubgroup { foodSubgroupID = row.Code, foodSubgroupName = row.Name, foodGroupID = row.Code[..2] });
                }
                else if (row.Code.Length == 5)
                {
                    classifications.Add(new FoodClassification { classificationID = row.Code, classificationName = row.Name, foodSubgroupID = row.Code[..3] });
                }
            }

            // Use a transaction to ensure data integrity (All or Nothing)
            using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                if (groups.Any())
                {
                    await db.FoodGroups.AddRangeAsync(groups, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                }

                if (subgroups.Any())
                {
                    await db.FoodSubgroups.AddRangeAsync(subgroups, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                }

                if (classifications.Any())
                {
                    await db.FoodClassifications.AddRangeAsync(classifications, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        // Searches & extracts all data rows under Code and Name headers.
        private static IEnumerable<(string Code, string Name)> ReadLookupRows(IXLWorksheet worksheet)
        {
            int codeCol = 0;
            int nameCol = 0;
            int headerRowNumber = 0;

            // Scan the first 20 rows for the required headers
            for (int r = 1; r <= 20; r++)
            {
                for (int c = 1; c <= 20; c++)
                {
                    string cellValue = worksheet.Cell(r, c).GetString().Trim();
                    if (string.IsNullOrEmpty(cellValue)) continue;

                    if (codeCol == 0 && cellValue.Equals("Code", StringComparison.OrdinalIgnoreCase))
                        codeCol = c;

                    if (nameCol == 0 && cellValue.Equals("Group name", StringComparison.OrdinalIgnoreCase))
                        nameCol = c;
                }

                if (codeCol != 0 && nameCol != 0)
                {
                    headerRowNumber = r;
                    break;
                }
                codeCol = 0;
                nameCol = 0;
            }

            if (headerRowNumber == 0) yield break;

            int emptyRowCount = 0;
            int currentRow = headerRowNumber + 1;
            const int MaxEmptyRowsBeforeStop = 10;

            while (emptyRowCount < MaxEmptyRowsBeforeStop && currentRow <= 100000)
            {
                string code = worksheet.Cell(currentRow, codeCol).GetString().Trim();
                string name = worksheet.Cell(currentRow, nameCol).GetString().Trim();

                if (string.IsNullOrWhiteSpace(code) && string.IsNullOrWhiteSpace(name))
                {
                    emptyRowCount++;
                }
                else
                {
                    emptyRowCount = 0;
                    yield return (code, name);
                }
                currentRow++;
            }
        }
    }
}
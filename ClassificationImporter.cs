using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics; // Added for Debug.WriteLine
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace FitnessTool
{
    internal class ClassificationImporter
    {
        public static async Task ImportFoodClassificationLookupAsync(
            string filePath,
            FoodDbContext db,
            string? worksheetName = null,
            CancellationToken cancellationToken = default)
        {
            string cleanedPath = filePath.Trim('"');
            Debug.WriteLine($"---> Starting Import from: {cleanedPath}");

            using var workbook = new XLWorkbook(cleanedPath);
            var worksheet = worksheetName is null
                ? workbook.Worksheet(1)
                : workbook.Worksheet(worksheetName);

            var rows = ReadLookupRows(worksheet).ToList();

            if (!rows.Any())
            {
                throw new InvalidOperationException($"No data rows were found in {cleanedPath}. Check the Visual Studio Output window to see what the importer actually read.");
            }

            Debug.WriteLine($"---> Found {rows.Count} total rows. Processing hierarchy...");

            var allRows = rows.Where(x => !string.IsNullOrWhiteSpace(x.Code)).ToList();

            var groups = allRows.Where(x => x.Code.Length == 2)
                .Select(x => new FoodGroup { foodGroupID = x.Code, foodGroupName = x.Name }).ToList();

            var subgroups = allRows.Where(x => x.Code.Length == 3)
                .Select(x => new FoodSubgroup { foodSubgroupID = x.Code, foodSubgroupName = x.Name, foodGroupID = x.Code[..2] }).ToList();

            var classifications = allRows.Where(x => x.Code.Length == 5)
                .Select(x => new FoodClassification { classificationID = x.Code, classificationName = x.Name, foodSubgroupID = x.Code[..3] }).ToList();

            Debug.WriteLine($"---> Mapping complete: {groups.Count} Groups, {subgroups.Count} Subgroups, {classifications.Count} Classifications.");

            await db.FoodGroups.AddRangeAsync(groups, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            await db.FoodSubgroups.AddRangeAsync(subgroups, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            await db.FoodClassifications.AddRangeAsync(classifications, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            Debug.WriteLine($"---> Database save successful.");
        }

        private static IEnumerable<(string Code, string Name)> ReadLookupRows(IXLWorksheet worksheet)
        {
            int headerRowNumber = 0;
            int codeCol = 0;
            int nameCol = 0;

            Debug.WriteLine("---> Scanning for header row...");

            for (int r = 1; r <= 20; r++)
            {
                for (int c = 1; c <= 20; c++)
                {
                    string cellValue = worksheet.Cell(r, c).GetString().Trim();
                    if (string.IsNullOrEmpty(cellValue)) continue;

                    // LOG EVERYTHING: This will show you exactly what the code sees in your Output window
                    Debug.WriteLine($"Checking Row {r}, Col {c}: '{cellValue}'");

                    if (codeCol == 0 && cellValue.Equals("Code", StringComparison.OrdinalIgnoreCase))
                    {
                        codeCol = c;
                        Debug.WriteLine($"FOUND CODE COLUMN at Row {r}, Col {c}");
                    }
                    if (nameCol == 0 && cellValue.Equals("Group name", StringComparison.OrdinalIgnoreCase))
                    {
                        nameCol = c;
                        Debug.WriteLine($"FOUND GROUP NAME COLUMN at Row {r}, Col {c}");
                    }
                }

                if (codeCol != 0 && nameCol != 0)
                {
                    headerRowNumber = r;
                    Debug.WriteLine($"---> SUCCESS: Header row confirmed at Row {headerRowNumber}");
                    break;
                }

                codeCol = 0;
                nameCol = 0;
            }

            if (headerRowNumber == 0)
            {
                Debug.WriteLine("---> FAILURE: Could not find 'Code' and 'Group name' in first 20 rows.");
                yield break;
            }

            int emptyRowCount = 0;
            int currentRow = headerRowNumber + 1;
            const int MaxEmptyRowsBeforeStop = 10;

            while (emptyRowCount < MaxEmptyRowsBeforeStop)
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
                if (currentRow > 100000) break;
            }
        }
    }
}
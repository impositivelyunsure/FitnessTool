using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace FitnessTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // temporarily having the data read on start up for now
            try
            {
                var options = new DbContextOptionsBuilder<FoodDbContext>()
                    .UseSqlite("Data Source=foods.db")
                    .Options;

                using var db = new FoodDbContext(options);
                await db.Database.EnsureCreatedAsync();

                // 1. Import Hierarchies first (Required for Foreign Keys)
                await ClassificationImporter.ImportFoodClassificationLookupAsync(
                    @"C:\FOOD DATA\AUSNUT-2023-Food-and-dietary-supplement-classification-system-1.xlsx",
                    db);

                // 2. Import AFCD (Simple one-table import)
                await ExcelImportService.ImportAfcdAsync(
                    @"C:\FOOD DATA\AFCD Release 3 - Nutrient profiles.xlsx",
                    db);

                // 3. Import AUSNUT (Two-table import)
                await ExcelImportService.ImportAusnutNutrientsAsync(
                    @"C:\FOOD DATA\AUSNUT 2023 - Food details.xlsx",
                    db);

                await ExcelImportService.ImportAusnutMetadataAsync(
                    @"C:\FOOD DATA\AUSNUT 2023 - Food nutrient profiles.xlsx",
                    db);

                MessageBox.Show("All datasets imported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Import Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

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

            try
            {
                var options = new DbContextOptionsBuilder<FoodDbContext>()
                    .UseSqlite("Data Source=foods.db")
                    .Options;

                using var db = new FoodDbContext(options);

                // Uncomment only if you want to wipe and rebuild the DB every run.
                // await db.Database.EnsureDeletedAsync();

                await db.Database.EnsureCreatedAsync();

                await ClassificationImportService.ImportFoodClassificationLookupAsync(
                    @"C:\Users\hagri\Downloads\AUSNUT-2023-Food-and-dietary-supplement-classification-system-1.xlsx",
                    db);

                // Add these back in when their import services are ready.
                // await ExcelImportService.ImportAusnutNutrientsAsync(
                //     @"C:\data\AUSNUT 2023 - Nutrients.xlsx",
                //     db);

                // await ExcelImportService.ImportAusnutMetadataAsync(
                //     @"C:\data\AUSNUT 2023 - Food details.xlsx",
                //     db);

                // await ExcelImportService.ImportAfcdAsync(
                //     @"C:\data\AFCD.xlsx",
                //     db);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Database import failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
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

            // temporarily load the db on startup
            try
            {
                var options = new DbContextOptionsBuilder<FoodDbContext>()
                    .UseSqlite("Data Source=foods.db")
                    .Options;

                using var db = new FoodDbContext(options);

                // We only call EnsureCreatedAsync() just in case the DB file is missing
                await db.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

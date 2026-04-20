using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessTool
{
    /// Author: Ethan Daly
    /// Date: 19/03/2026
    /// Fitness app project used to track macros of created meals & food items
    
    public partial class MainWindow : Window
    {
        FitnessProcessor processorObj = new FitnessProcessor();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnCalcMacros_Click(object sender, RoutedEventArgs e)
        {
            //processorObj.AddMacros(txtBoxProteins.Text, txtBoxFats.Text, txtBoxCarbs.Text);
        }

        private void btnCreateCustomMeal_Click(object sender, RoutedEventArgs e)
        {
            //string inputBrandName = txtBoxMealName.Text;
            //double inputPrice = 4;
            //double inputProtein = 2;
            //double inputFats = 5;
            //double inputCarbs = 7;
            //double inputGrams = 6;
            //CustomFoodItem test2 = new CustomFoodItem(inputBrandName, inputPrice, inputProtein, inputFats, inputCarbs, inputGrams);
            //MealComponent test = new MealComponent(test2);

            //List<MealComponent> testList = new List<MealComponent>();
            //testList.Add(test);
            //processorObj.CreateCustomMeal(testList, "test name", "4", "5", "8");
        }

        
    }
}
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
            processorObj.AddMacros(txtBoxProteins.Text, txtBoxFats.Text, txtBoxCarbs.Text);
        }
    }
}
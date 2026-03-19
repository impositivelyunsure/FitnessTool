using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static FitnessLibrary.FitnessProcessorLib;


namespace FitnessTool
{
    class FitnessProcessor
    {
        public void AddMacros(string proteinsInput, string fatsInput, string carbsInput)
        {
            if (Double.TryParse(proteinsInput, out double resultProteins))
            {
                if (Double.TryParse(fatsInput, out double resultFats))
                {
                    if (Double.TryParse(carbsInput, out double resultCarbs))
                    {
                        var APIobject = new FitnessAPIClient();
                    }
                    else
                    {
                        MessageBox.Show("protein input is not a valid number", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("fats input is not a valid number", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("carbs input is not a valid number", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


    }
}

using FitnessContracts;
using FitnessLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static FitnessLibrary.FitnessProcessorLib;



namespace FitnessTool
{
    class FitnessProcessor
    {
        public List<double> macroList = new List<double>();

        public void AddMacros(string proteinsInput, string fatsInput, string carbsInput)
        {
            if (Double.TryParse(proteinsInput, out double resultProteins))
            {
                if (Double.TryParse(fatsInput, out double resultFats))
                {
                    if (Double.TryParse(carbsInput, out double resultCarbs))
                    {
                        FitnessMethods temp = new FitnessMethods();

                        this.macroList.Add(temp.CalcMacros(resultProteins, resultFats, resultCarbs));
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

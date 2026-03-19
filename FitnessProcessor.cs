using FitnessContracts;
using FitnessLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static FitnessLibrary.FitnessProcessorLib;



namespace FitnessTool
{
    // back-end processor class
    public class FitnessProcessor
    {
        public List<double> macroList = new List<double>();


        // method for adding macros to list
        public void AddMacros(string proteinsInput, string fatsInput, string carbsInput)
        {
            if (Double.TryParse(proteinsInput, out double resultProteins))
            {
                if (Double.TryParse(fatsInput, out double resultFats))
                {
                    if (Double.TryParse(carbsInput, out double resultCarbs))
                    {
                        // create library object with reference to fitnessmethods classi nside the library
                        FitnessMethods temp = new FitnessMethods();

                        // calculate macros from this object given the inputs, add result to the macro list
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

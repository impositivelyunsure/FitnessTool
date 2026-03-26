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
        public List<CustomMealItem> customMealsList = new List<CustomMealItem>();

        public List<MealComponent> componentList = new List<MealComponent>();


        // method for adding macros to list
        public void AddMacros(string proteinsInput, string fatsInput, string carbsInput)
        {
            if (Double.TryParse(proteinsInput, out double resultProteins))
            {
                if (Double.TryParse(fatsInput, out double resultFats))
                {
                    if (Double.TryParse(carbsInput, out double resultCarbs))
                    {
                        // create library object with reference to fitnessmethods class inside the library
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

        public void AddCustomMeal(List<MealComponent> componentsInputList)
        {
            customMealsList


            // clear comp list after creation
        }

        public void AddComponent(MealComponent component)
        {
            componentList.Add(component);
        }

        public CustomMealItem CreateCustomMealItem(List<MealComponent> mealItemsList)
        {
            componentList = mealItemsList;

            CustomMealItem custMealItem = new CustomMealItem();

            return custMealItem;
        }

        // Create and return a custom food item 
        public CustomFoodItem CreateCustomFoodItem(string inputBrandName, double inputPrice, double inputProtein, double inputFats, double inputCarbs, double inputGrams)
        {
            CustomFoodItem custFoodItem = new CustomFoodItem(inputBrandName, inputPrice, inputProtein, inputFats, inputCarbs, inputGrams);
            return custFoodItem;
        }

        // Create and return an existing food item
        public ExistingFoodItem CreateExistingFoodItem(string inputBrandName, double inputPrice, double inputProtein, double inputFats, double inputCarbs, double inputGrams)
        {
            ExistingFoodItem exstFoodItem = new ExistingFoodItem(inputBrandName, inputPrice, inputProtein, inputFats, inputCarbs, inputGrams);
            return exstFoodItem;
        }

        


    }
}

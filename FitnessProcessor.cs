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
        
        public List<CustomMealItem> customMealsList = new();

        public List<MealComponent> componentList = new();

       
        public void AddCustomMeal()
        {
           
        }

        public void AddComponent(MealComponent component)
        {
            componentList.Add(component);
        }

        // method for creating a custom meal
        // add feature:
        // comp list shouldnt need to clear every time, its tedious especially if youre manually adding items there
        // remove the need for meal component section, it should be as simple as searching the database & dragging food items into a meal.
        public CustomMealItem CreateCustomMeal(List<MealComponent> componentsInputList, string inputName, string inputQuantity, string inputServingSize, string inputGrams)
        {
            if (Double.TryParse(inputQuantity, out double resultQuantity))
            {
                if (Double.TryParse(inputServingSize, out double resultServingSize))
                {
                    if (Double.TryParse(inputGrams, out double resultGrams))
                    {
                        // i need to later add error trapping to check if comp list is actually valid
                        CustomMealItem temp = new CustomMealItem(componentsInputList, inputName, resultQuantity, resultServingSize, resultGrams);
                        componentsInputList.Clear(); // clear comp list as the meal has been created
                        return temp;
                    }
                    else
                    {
                        MessageBox.Show("grams input is not a valid number", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("serving size input is not a valid number", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("quantity input is not a valid number", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            // return null
            return null;
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

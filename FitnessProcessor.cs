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


       
        public void AddCustomMeal(CustomMealItem custMealItem)
        {
            customMealsList.Add(custMealItem);
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
            var inputs = new List<string> { inputQuantity, inputServingSize, inputGrams };
            var result = Parser.ParseAllToDoubles(inputs);

            if (result.success == true)
            {
                // i need to later add error trapping to check if comp list is actually valid
                CustomMealItem custMeal = new CustomMealItem(componentsInputList, inputName, result.validList[0], result.validList[1], result.validList[2]);
                componentsInputList.Clear(); // clear comp list as the meal has been created
                return custMeal;
            }
            else
            {
                Console.WriteLine("Invalid inputs: " + string.Join(", ", result.invalidList));
            }
            return null;
        }


        // Create and return a custom food item 
        public CustomFoodItem CreateCustomFoodItem(string inputBrandName, string inputPrice, string inputProtein, string inputFats, string inputCarbs, string inputGrams)
        {
            var inputs = new List<string> { inputBrandName, inputPrice, inputProtein, inputFats, inputCarbs, inputGrams };
            var result = Parser.ParseAllToDoubles(inputs);

            if (result.success == true)
            {
                CustomFoodItem custFoodItem = new CustomFoodItem(inputBrandName, result.validList[0], result.validList[1], result.validList[2], result.validList[3], result.validList[4]);
                return custFoodItem;

            }
            else
            {
                Console.WriteLine("Invalid inputs: " + string.Join(", ", result.invalidList));
            }
            return null;
        }
        // Create and return an existing food item
        public ExistingFoodItem CreateExistingFoodItem(string inputBrandName, double inputPrice, double inputProtein, double inputFats, double inputCarbs, double inputGrams)
        {
            ExistingFoodItem exstFoodItem = new ExistingFoodItem(inputBrandName, inputPrice, inputProtein, inputFats, inputCarbs, inputGrams);
            return exstFoodItem;
        }

        


    }
}

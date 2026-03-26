using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public class CustomMealItem : Meal
    {
        List<MealComponent> componentsList = new();

        public string mealName { get;set; }
        public double quantity { get; set; }
        public double servingSize { get; set; }
        public double grams { get; set; }


        public CustomMealItem(List<MealComponent> inputCompList, string inputName)
        {
            componentsList = inputCompList;
            mealName = inputName;
        }
        public CustomMealItem(List<MealComponent> inputCompList, string inputName, double inputQuantity, double inputServingSize, double inputGrams)
        {
            componentsList = inputCompList;
            mealName = inputName;
            quantity = inputQuantity;
            servingSize = inputServingSize;
            grams = inputGrams;
        }
        
    }
}

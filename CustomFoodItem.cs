using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public class CustomFoodItem : Food
    {
        public string brandName { get; set; }
        public double price { get; set; }
        public double protein { get; set; }
        public double fats { get; set; }
        public double carbs { get; set; }
        public double grams { get; set; }

        // Constructor for creating a custom food item
        public CustomFoodItem(string inputBrandName, double inputPrice, double inputProtein, double inputFats, double inputCarbs, double inputGrams)
        {
            this.brandName = inputBrandName;
            this.price = inputPrice;
            this.protein = inputProtein;
            this.fats = inputFats;
            this.carbs = inputCarbs;
            this.grams = inputGrams;
        }

        // Create a new custom food item and return it
        public CustomFoodItem CreateCustomFoodItem(string inputBrandName, double inputPrice, double inputProtein, double inputFats, double inputCarbs, double inputGrams)
        {
            CustomFoodItem var = new CustomFoodItem(inputBrandName, inputPrice, inputProtein, inputFats, inputCarbs, inputGrams);
            return var;
        }

    }
}

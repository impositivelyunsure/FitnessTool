using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FitnessTool
{
    public class CustomFoodItem : Food
    {
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
    }
}

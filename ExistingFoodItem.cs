using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public class ExistingFoodItem : Food
    {
        public ExistingFoodItem(string inputBrandName, double inputPrice, double inputProtein, double inputFats, double inputCarbs, double inputGrams)
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

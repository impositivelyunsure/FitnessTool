using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public class CustomMealItem : Meal
    {
        public double quantity { get; set; }
        public double servingSize { get; set; }
        public double grams { get; set; }
        public CustomMealItem CreateCustomMealItem(List<MealComponent> listMealItems)
        {
            CustomMealItem custMealItem = new CustomMealItem();

            return custMealItem;
        }
    }
}

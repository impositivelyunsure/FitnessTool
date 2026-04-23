using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public abstract class Food
    {
        public string foodName { get; set; } = "";

        public string publicFoodKey { get; set; } = "";
        public int foodClassificationID { get; set; }

        FoodClassification foodClassification { get; set; }
        Nutrients nutrientProfile = new();


        //public string brandName { get; set; }
        //public double price { get; set; }
    }
}

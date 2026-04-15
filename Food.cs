using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public abstract class Food
    {
        Nutrients nutrientProfile = new();

        public string publicFoodKey { get; set; } = "";
        public string foodName { get; set; } = "";

        public int foodClassificationID { get; set; }
        FoodClassification foodClassification { get; set; }


        //public string brandName { get; set; }
        //public double price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{

    // a separate meal comp is better aggregation than inheriting food class
    public class MealComponent
    {
        public Food compFood { get; set; }

        public MealComponent (Food inputFood)
        {
            compFood = inputFood;
        }
    }
}

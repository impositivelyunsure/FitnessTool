using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{

    // decided on a separate meal comp class as it's better aggregation than inheriting food class directly
    // allows for meals to have objects of both existing and custom meal items in the form of a mealcomponent list
    public class MealComponent
    {
        public Food compFood { get; set; }

        public MealComponent (Food inputFood)
        {
            compFood = inputFood;
        }
    }
}

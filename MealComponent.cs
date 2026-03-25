using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    // i think creating a separate class of meal components is more coherent than i`nheriting directly from food
    // 
    public class MealComponent
    {
        public Food food { get; set; }
    }
}

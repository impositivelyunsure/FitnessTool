using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public class Meal
    {
        public Guid mealID { get; set; }
        public List<MealComponent> listMealComps { get; set; }
    }
}

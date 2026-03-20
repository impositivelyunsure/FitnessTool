using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public interface Food
    {
        public string brandName { get; set; }
        public double price { get; set; }
        public double protein { get; set; }
        public double fats { get; set; }
        public double carbs { get; set; }
        public double grams { get; set; }

        // ADD IN FUTURE:
        // micronutrients
    }
}

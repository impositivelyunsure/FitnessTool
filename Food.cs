using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public abstract class Food
    {
        Nutrients nutrientProfile = new();
        public string brandName { get; set; }
        public double price { get; set; }
    }
}

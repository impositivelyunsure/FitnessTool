using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    internal class CombinedFood
    {
        public string publicFoodKey { get; set; }
        public string foodName { get; set; }

        public AusnutFoodEntryRaw ausnutEntry { get; set; }
        public AfcdFoodEntryRaw afcdEntry { get; set; }

    }
}

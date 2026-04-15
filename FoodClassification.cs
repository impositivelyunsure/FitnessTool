using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    // e.g 18001 -> 18 (Meat, poultry and game products and dishes), -> 180 (Wild harvested meat, and meat dishes), -> 18001 (Wild harvested mallamian meat)
    internal class FoodClassification
    {
        public int classificationID { get; set; } // 5 digit classification id, the classification of food. 
        public string classificationName { get; set; } = "";

        public string foodSubgroupID { get; set; } = "";
        public FoodGroup foodGroup { get; set; } 

    }
}

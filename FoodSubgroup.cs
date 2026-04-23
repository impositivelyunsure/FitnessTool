using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public class FoodSubgroup
    {
        public string foodSubgroupID { get; set; } = ""; // 3 digit classification ID, the subgroup
        public string foodSubgroupName { get; set; } = "";

        public string foodGroupID { get; set; } = "";
        public FoodGroup foodGroup { get; set; }

    }
}

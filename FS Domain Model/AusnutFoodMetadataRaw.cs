using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{

    /// <summary>
    /// AUSNUT metadata
    /// </summary>

    public class AusnutFoodMetadataRaw
    {
        public int surveyID { get; set; }
        public string publicFoodKey { get; set; } = "";
        public string derivation { get; set; } = "";
        public string foodName { get; set; } = "";
        public FoodClassification foodClassification { get; set; } = null!;


        public string? foodDescription { get; set; }
        public string? samplingDetails { get; set; }

        public decimal? nitrogenFactor { get; set; }
        public decimal? fatFactor { get; set; }
        public decimal? specificGravity { get; set; }
        public decimal? adgConversionFactor { get; set; }

        public string? ediblePortion { get; set; }
        public string? inediblePortion { get; set; }

        public string foodAndDietarySupplementClassificationCode { get; set; } = "";
        public string? foodAndDietarySupplementClassificationName { get; set; }

        public string? adgClassificationCode1 { get; set; }
        public string? adgClassificationName1 { get; set; }

        public string? adgClassificationCode2 { get; set; }
        public string? adgClassificationName2 { get; set; }

        public string? discretionaryFoodClassification { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    internal class AusnutFoodEntryRaw
    {
        public int surveyId { get; set; }
        public string publicFoodKey { get; set; } = "";
        public string derivation { get; set; } = "";
        public string foodName { get; set; } = "";

        public decimal? energyWithDietaryFibreKj { get; set; }
        public decimal? energyWithoutDietaryFibreKj { get; set; }
        public decimal? moistureG { get; set; }
        public decimal? proteinG { get; set; }
        public decimal? totalFatG { get; set; }
        public decimal? availableCarbohydrateWithSugarAlcoholsG { get; set; }
        public decimal? availableCarbohydrateWithoutSugarAlcoholsG { get; set; }
        public decimal? starchG { get; set; }
        public decimal? totalSugarsG { get; set; }
        public decimal? addedSugarsG { get; set; }
        public decimal? freeSugarsG { get; set; }
        public decimal? dietaryFibreG { get; set; }
        public decimal? alcoholG { get; set; }
        public decimal? ashG { get; set; }

        public decimal? calciumCaMg { get; set; }
        public decimal? iodineIMcg { get; set; }
        public decimal? ironFeMg { get; set; }
        public decimal? magnesiumMgMg { get; set; }
        public decimal? phosphorusPMg { get; set; }
        public decimal? potassiumKMg { get; set; }
        public decimal? seleniumSeMcg { get; set; }
        public decimal? sodiumNaMg { get; set; }
        public decimal? zincZnMg { get; set; }

        public decimal? preformedVitaminARetinolMcg { get; set; }
        public decimal? betaCaroteneMcg { get; set; }
        public decimal? proVitaminABetaCaroteneEquivalentsMcg { get; set; }
        public decimal? vitaminARetinolEquivalentsMcg { get; set; }

        public decimal? thiaminB1Mg { get; set; }
        public decimal? riboflavinB2Mg { get; set; }
        public decimal? niacinB3Mg { get; set; }
        public decimal? niacinDerivedEquivalentsMg { get; set; }
        public decimal? pyridoxineB6Mg { get; set; }
        public decimal? folateNaturalMcg { get; set; }
        public decimal? folicAcidMcg { get; set; }
        public decimal? totalFolatesMcg { get; set; }
        public decimal? dietaryFolateEquivalentsMcg { get; set; }
        public decimal? cobalaminB12Mcg { get; set; }
        public decimal? vitaminCMg { get; set; }

        public decimal? cholecalciferolD3Mcg { get; set; }
        public decimal? ergocalciferolD2Mcg { get; set; }
        public decimal? n25HydroxyCholecalciferol25OhD3Mcg { get; set; }
        public decimal? n25HydroxyErgocalciferol25OhD2Mcg { get; set; }
        public decimal? vitaminD3EquivalentsMcg { get; set; }
        public decimal? alphaTocopherolMg { get; set; }
        public decimal? vitaminEMg { get; set; }

        public decimal? totalSaturatedFatG { get; set; }
        public decimal? totalMonounsaturatedFatG { get; set; }
        public decimal? linoleicAcidG { get; set; }
        public decimal? alphaLinolenicAcidG { get; set; }
        public decimal? eicosapentaenoicAcidEpaMg { get; set; }
        public decimal? docosapentaenoicAcidDpaMg { get; set; }
        public decimal? docosahexaenoicAcidDhaMg { get; set; }
        public decimal? totalPolyunsaturatedFatG { get; set; }
        public decimal? totalLongChainOmega3FattyAcidsMg { get; set; }
        public decimal? totalTransFattyAcidsMg { get; set; }

        public decimal? caffeineMg { get; set; }
        public decimal? cholesterolMg { get; set; }
        public decimal? tryptophanMg { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    internal class AusnutFoodEntryRaw
    {
        public int SurveyId { get; set; }
        public string PublicFoodKey { get; set; } = "";
        public string Derivation { get; set; } = "";
        public string FoodName { get; set; } = "";

        public decimal? EnergyWithDietaryFibreKj { get; set; }
        public decimal? EnergyWithoutDietaryFibreKj { get; set; }
        public decimal? MoistureG { get; set; }
        public decimal? ProteinG { get; set; }
        public decimal? TotalFatG { get; set; }
        public decimal? AvailableCarbohydrateWithSugarAlcoholsG { get; set; }
        public decimal? AvailableCarbohydrateWithoutSugarAlcoholsG { get; set; }
        public decimal? StarchG { get; set; }
        public decimal? TotalSugarsG { get; set; }
        public decimal? AddedSugarsG { get; set; }
        public decimal? FreeSugarsG { get; set; }
        public decimal? DietaryFibreG { get; set; }
        public decimal? AlcoholG { get; set; }
        public decimal? AshG { get; set; }

        public decimal? CalciumCaMg { get; set; }
        public decimal? IodineIMcg { get; set; }
        public decimal? IronFeMg { get; set; }
        public decimal? MagnesiumMgMg { get; set; }
        public decimal? PhosphorusPMg { get; set; }
        public decimal? PotassiumKMg { get; set; }
        public decimal? SeleniumSeMcg { get; set; }
        public decimal? SodiumNaMg { get; set; }
        public decimal? ZincZnMg { get; set; }

        public decimal? PreformedVitaminARetinolMcg { get; set; }
        public decimal? BetaCaroteneMcg { get; set; }
        public decimal? ProVitaminABetaCaroteneEquivalentsMcg { get; set; }
        public decimal? VitaminARetinolEquivalentsMcg { get; set; }

        public decimal? ThiaminB1Mg { get; set; }
        public decimal? RiboflavinB2Mg { get; set; }
        public decimal? NiacinB3Mg { get; set; }
        public decimal? NiacinDerivedEquivalentsMg { get; set; }
        public decimal? PyridoxineB6Mg { get; set; }
        public decimal? FolateNaturalMcg { get; set; }
        public decimal? FolicAcidMcg { get; set; }
        public decimal? TotalFolatesMcg { get; set; }
        public decimal? DietaryFolateEquivalentsMcg { get; set; }
        public decimal? CobalaminB12Mcg { get; set; }
        public decimal? VitaminCMg { get; set; }

        public decimal? CholecalciferolD3Mcg { get; set; }
        public decimal? ErgocalciferolD2Mcg { get; set; }
        public decimal? N25HydroxyCholecalciferol25OhD3Mcg { get; set; }
        public decimal? N25HydroxyErgocalciferol25OhD2Mcg { get; set; }
        public decimal? VitaminD3EquivalentsMcg { get; set; }
        public decimal? AlphaTocopherolMg { get; set; }
        public decimal? VitaminEMg { get; set; }

        public decimal? TotalSaturatedFatG { get; set; }
        public decimal? TotalMonounsaturatedFatG { get; set; }
        public decimal? LinoleicAcidG { get; set; }
        public decimal? AlphaLinolenicAcidG { get; set; }
        public decimal? EicosapentaenoicAcidEpaMg { get; set; }
        public decimal? DocosapentaenoicAcidDpaMg { get; set; }
        public decimal? DocosahexaenoicAcidDhaMg { get; set; }
        public decimal? TotalPolyunsaturatedFatG { get; set; }
        public decimal? TotalLongChainOmega3FattyAcidsMg { get; set; }
        public decimal? TotalTransFattyAcidsMg { get; set; }

        public decimal? CaffeineMg { get; set; }
        public decimal? CholesterolMg { get; set; }
        public decimal? TryptophanMg { get; set; }
    }
}

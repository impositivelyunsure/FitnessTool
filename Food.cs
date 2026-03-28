using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    public abstract class Food
    {
        public string brandName { get; set; }
        public double price { get; set; }
        decimal macroEnergyValue { get; set; }
        decimal dbEnergyValue { get; set; }
        public double protein { get; set; }
        public double fats { get; set; }
        public double carbs { get; set; }
        public double grams { get; set; }
        decimal iron_mg { get; set; }
        decimal magnesium_mg{ get; set; }
        decimal phosphorus_mg { get; set; }
        decimal potassium_mg { get; set; }
        decimal selenium_ug { get; set; }
        decimal sodium_mg { get; set; }
        decimal zinc_mg { get; set; }
        decimal retinol_ug { get; set; }
        decimal proVitaminA_BetaCaroteneEq_ug { get; set; }
        decimal thiamin_mg { get; set; }
        decimal ribloflavinB2_mg { get; set; }
        decimal niacin_mg { get; set; }
        decimal _mg { get; set; }
        decimal pyridoxine_mg { get; set; } // vitamin b6
        decimal naturalfolate_ug { get; set; }
        decimal folicAcid_ug { get; set; }

        decimal totalFolates_ug { get; set; }
        decimal dietaryFolateEq_mg { get; set; }
        decimal coblamin_ug { get; set; }
        decimal vitaminC_mg { get; set; }

        decimal cholecalciferol_ug { get; set; }
        decimal ergocalciferol_ug { get; set; }

        decimal vitaminD3_25OH_ug { get; set; }
        decimal vitaminD2_25OH_ug { get; set; }
        decimal vitaminD3Eq_ug { get; set; }
        decimal alphatocopherol_mg { get; set; }
        decimal vitaminE_mg { get; set; }
        decimal totalSaturedFat_g { get; set; }

        decimal totalMonounsaturedFat_g { get; set; }
        decimal linoleicAcid_g { get; set; }
        decimal alphaLinoleicAcid_g { get; set; }
        decimal eicosapentaenoicAcid_mg { get; set; }
        decimal docosapentaenoicAcid_mg { get; set; }
        decimal docosahexaenoicAcid_mg { get; set; }
        decimal totalPolyunsaturedFat_g { get; set; }
        decimal totalLongChainOmega3_mg { get; set; }
        decimal totalTransFat_mg { get; set; }
        decimal cholesterol_mg { get; set; }
        decimal tryptophan_mg { get; set; }

    }
}

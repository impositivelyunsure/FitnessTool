using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    // KEY:
    // Carbon amount, double bonds
    // C, 0 = saturated
    // C, 1 = monounsaturated
    // C, 2 = polyunsaturated

    // CALCULATING TOTALS
    // for saturated, sum of all carbons with no double bonds (e.g C18_0, C19_0)
    // for monounsaturated, sum of all carbons with 1 double bonds (e.g C18_1, C19_1)
    // for polyunsaturated, sum of all carbons with 2 double bonds (e.g C18_2, C19_2)
    public class FattyAcids
    {
        decimal total_saturated_fatty_acids_equated_pct_t { get; set; }
        decimal c4_pct_t { get; set; }
        decimal c6_pct_t { get; set; }
        decimal c8_pct_t { get; set; }
        decimal c10_pct_t { get; set; }
        decimal c11_pct_t { get; set; }
        decimal c12_pct_t { get; set; }
        decimal c13_pct_t { get; set; }
        decimal c14_pct_t { get; set; }
        decimal c15_pct_t { get; set; }
        decimal c16_pct_t { get; set; }
        decimal c17_pct_t { get; set; }
        decimal c18_pct_t { get; set; }
        decimal c19_pct_t { get; set; }
        decimal c20_pct_t { get; set; }
        decimal c21_pct_t { get; set; }
        decimal c22_pct_t { get; set; }
        decimal c23_pct_t { get; set; }
        decimal c24_pct_t { get; set; }


        decimal total_monounsaturated_fatty_acids_equated_pct_t { get; set; }
        decimal c12_1_pct_t { get; set; }
        decimal c14_1_pct_t { get; set; }
        decimal c15_1_pct_t { get; set; }
        decimal c16_1_pct_t { get; set; }
        decimal c17_1_pct_t { get; set; }
        decimal c18_1_pct_t { get; set; }
        decimal c18_1w7_pct_t { get; set; }
        decimal c20_1_pct_t { get; set; }
        decimal c20_1w11_pct_t { get; set; }
        decimal c22_1_pct_t { get; set; }
        decimal c22_1w11_pct_t { get; set; }
        decimal c24_1_pct_t { get; set; }


        decimal total_polyunsaturated_fatty_acids_equated_pct_t { get; set; }
        decimal total_long_chain_omega_3_fatty_acids_equated_pct_t { get; set; }
        decimal total_undifferentiated_fatty_acids_pct_t { get; set; }
        decimal total_trans_fatty_acids_imputed_pct_t { get; set; }
        decimal c12_2_pct_t { get; set; }
        decimal c16_2w4_pct_t { get; set; }
        decimal c16_3_pct_t { get; set; }
        decimal c18_2w6_pct_t { get; set; }
        decimal c18_3w3_pct_t { get; set; }
        decimal c18_3w4_pct_t { get; set; }
        decimal c18_3w6_pct_t { get; set; }
        decimal c18_4w1_pct_t { get; set; }
        decimal c18_4w3_pct_t { get; set; }
        decimal c20_2_pct_t { get; set; }
        decimal c20_2w6_pct_t { get; set; }
        decimal c20_4_pct_t { get; set; }
        decimal c20_3w3_pct_t { get; set; }
        decimal c20_3w6_pct_t { get; set; }
        decimal c20_4w3_pct_t { get; set; }
        decimal c20_4w6_pct_t { get; set; }
        decimal c20_5w3_pct_t { get; set; }
        decimal c21_5w3_pct_t { get; set; }
        decimal c22_2_pct_t { get; set; }
        decimal c22_2w6_pct_t { get; set; }
        decimal c22_4w6_pct_t { get; set; }
        decimal c22_5w3_pct_t { get; set; }
        decimal c22_5w6_pct_t { get; set; }
        decimal c22_6w3_pct_t { get; set; }
       

        decimal total_saturated_fatty_acids_equated_g { get; set; }
        decimal c4_g { get; set; }
        decimal c6_g { get; set; }
        decimal c8_g { get; set; }
        decimal c10_g { get; set; }
        decimal c11_g { get; set; }
        decimal c12_g { get; set; }
        decimal c13_g { get; set; }
        decimal c14_g { get; set; }
        decimal c15_g { get; set; }
        decimal c16_g { get; set; }
        decimal c17_g { get; set; }
        decimal c18_g { get; set; }
        decimal c19_g { get; set; }
        decimal c20_g { get; set; }
        decimal c21_g { get; set; }
        decimal c22_g { get; set; }
        decimal c23_g { get; set; }
        decimal c24_g { get; set; }


        decimal total_monounsaturated_fatty_acids_equated_g { get; set; }
        decimal c12_1_g { get; set; }
        decimal c14_1_g { get; set; }
        decimal c15_1_g { get; set; }
        decimal c16_1_g { get; set; }
        decimal c17_1_g { get; set; }
        decimal c18_1_g { get; set; }
        decimal c18_1w7_g { get; set; }
        decimal c20_1_g { get; set; }
        decimal c20_1w11_mg { get; set; }
        decimal c22_1_g { get; set; }
        decimal c22_1w11_mg { get; set; }
        decimal c24_1_g { get; set; }


        decimal total_polyunsaturated_fatty_acids_equated_g { get; set; }
        decimal total_long_chain_omega_3_fatty_acids_equated_mg { get; set; }
        decimal total_undifferentiated_fatty_acids_mass_basis_mg { get; set; }
        decimal total_trans_fatty_acids_imputed_mg { get; set; }
        decimal c12_2_g { get; set; }
        decimal c16_2w4_mg { get; set; }
        decimal c16_3_g { get; set; }
        decimal c18_2w6_g { get; set; }
        decimal c18_3w3_g { get; set; }
        decimal c18_3w4_g { get; set; }
        decimal c18_3w6_mg { get; set; }
        decimal c18_4w1_g { get; set; }
        decimal c18_4w3_mg { get; set; }
        decimal c20_2_mg { get; set; }
        decimal c20_2w6_mg { get; set; }
        decimal c20_3_mg { get; set; }
        decimal c20_3w3_mg { get; set; }
        decimal c20_3w6_mg { get; set; }
        decimal c20_4_g { get; set; }
        decimal c20_4w3_mg { get; set; }
        decimal c20_4w6_mg { get; set; }
        decimal c20_5w3_mg { get; set; }
        decimal c21_5w3_g { get; set; }
        decimal c22_2_g { get; set; }
        decimal c22_2w6_mg { get; set; }
        decimal c22_4w6_mg { get; set; }
        decimal c22_5w3_mg { get; set; }
        decimal c22_5w6_g { get; set; }
        decimal c22_6w3_mg { get; set; }
    }
}

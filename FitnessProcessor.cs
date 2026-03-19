using System;
using System.Collections.Generic;
using System.Text;
using static FitnessLibrary.FitnessProcessorLib;


namespace FitnessTool
{
    class FitnessProcessor
    {
        public void AddMacros(string proteinsInput, string fatsInput, string carbsInput)
        {
            if (Double.TryParse(proteinsInput, out double resultProteins))
            {
                if (Double.TryParse(fatsInput, out double resultFats))
                {
                    if (Double.TryParse(carbsInput, out double resultCarbs))
                    {
                        var APIobject = new FitnessAPIClient();
                    }

                }
            }
        }




    }
}

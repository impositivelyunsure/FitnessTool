using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTool
{
    // class for parsing inputs
    public class Parser
    {
        public bool success { get; set; }
        public List<double> validList { get; set; } = new();
        public List<string> invalidList { get; set; } = new();

     

        // this method attempts to parse a collection of strings to doubles
        // returns true only when the whole collection was successfuly parsed, otherwise false
        // returns back a list of parsed values if true
        // returns back a list of the values that couldnt be parsed if false
        public static Parser ParseAllDoubles(IEnumerable<string> inputList)
        {
            var result = new Parser();

            // loop through the list given
            foreach(var input in inputList)
            {
                // try parse as a double
                if (Double.TryParse(input, out double parsed))
                {
                    // if parsed, add to valid list
                    result.validList.Add(parsed);
                }
                else
                {
                    // if failed to parse, add to invalid list
                    result.invalidList.Add(input);
                }
            }

            // if the invalid list is empty, return true
            if (result.invalidList.Count == 0)
            {
                result.success = true;
            }
            else
            {
                // else return false as the invalid list was not empty
                result.success = false;
            }

            // if parsing failed, clear valid list and return the invalid inputs back to user
            if (result.success == false)
            {
                result.validList.Clear();
            }

            return result;
        }

    }
}

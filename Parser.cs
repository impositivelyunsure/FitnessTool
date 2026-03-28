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
        // return object contains bool, only returns true when whole collection was successfuly parsed, otherwise false
        // return object contains list of parsed values if true
        // return object contains  list of failed parsed values if false, clearing valid inputs as we don't care what parsed, only what failed
        // TO CONSIDER:
        // as it returns an ordered list, methods calling it that want those values must use result.validList[0], result.validList[1], ect.
        // its fine until the structure changes, then all methods calling this will have to be manually amended
        public static Parser ParseAllToDoubles(IEnumerable<string> inputList)
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

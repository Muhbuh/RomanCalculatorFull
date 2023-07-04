using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator.Interface
{
    /// <summary>
    /// This interface is used for number checking
    /// This interface assume that the value of each symbol decreases or increases from left to right
    /// e.g. MCX -> 1000 100 10 or XCM -> 10 100 1000
    /// </summary>
    internal interface NumeralCheck
    {
        // In the long term it would be better to create a configuration class and use a JSON config file

        /// <summary>
        /// List of valid symbols in the numeral string
        /// The value i can be followed by any value with an higher index but not the other way around
        /// </summary>
        public List<string> ValidSymbols { get; set; }

        /// <summary>
        /// List with the number of maximum repeats for the valid symbols. List must have same length
        /// </summary>
        public List<int> MaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// Initiate the configuration of the numeral system
        /// </summary>
        /// <param name="_ValidSymbols"></param>
        /// <param name="_ValidCombinations"></param>
        /// <param name="_ValueDescends"></param>
        public void Init(List<string> _ValidSymbols, List<int> _MaximumNumberOfRepeats);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns>True: valid number, false: not a valid number</returns>
        public bool CheckNumeral(string number);
    }
}

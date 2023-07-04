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
        /// <summary>
        /// Flag wether or not the value of each symbol descends from left to right
        /// Staying the same value is always valid
        /// </summary>
        public bool ValueDescends { get; set; }

        /// <summary>
        /// List of valid symbols in the numeral string
        /// </summary>
        public List<string> ValidSymbols { get; set; }

        /// <summary>
        /// List of valid symbol combinations e.g. IV or IX
        /// </summary>
        public List<string> ValidCombinations  { get; set; }

        /// <summary>
        /// Initiate the configuration of the numeral system
        /// </summary>
        /// <param name="_ValidSymbols"></param>
        /// <param name="_ValidCombinations"></param>
        /// <param name="_ValueDescends"></param>
        public void Init(List<string> _ValidSymbols, List<string> _ValidCombinations, bool _ValueDescends);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns>True: valid number, false: not a valid number</returns>
        public bool CheckNumeral(string number);
    }
}

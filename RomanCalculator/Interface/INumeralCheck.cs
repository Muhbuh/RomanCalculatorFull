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
    public interface INumeralCheck
    {
        /// <summary>
        /// Initiate the configuration of the numeral system
        /// </summary>
        /// <param name="_ValidSymbols"></param>
        /// <param name="_MaximumNumberOfRepeats"></param>
        //public void Init(List<string> _ValidSymbols, List<int> _MaximumNumberOfRepeats);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns>True: valid number, false: not a valid number</returns>
        public bool CheckNumeral(string number);
    }
}

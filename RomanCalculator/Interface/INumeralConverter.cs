using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator.Interface
{
    /// <summary>
    /// Interface used to convert symbolic number into an integer and vice versa
    /// </summary>
    public interface INumeralConverter
    {
        // In the long term it would be better to create a configuration class and use a JSON config file

        /// <summary>
        /// List of valid symbols in the numeral string
        /// The value i can be followed by any value with an higher index but not the other way around
        /// </summary>
        public List<string> ValidSymbols { get; set; }

        /// <summary>
        /// List with the values for the valid symbols. List must have same length
        /// </summary>
        public List<int> SymbolValues { get; set; }

        /// <summary>
        /// Convert a number as symbols to interger
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public int ConvertStringToInt(string Number);

        /// <summary>
        /// Converts an Integer into the symbol represantation
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public string ConvertIntToString(int Number);

        /// <summary>
        /// Method to choose which conversion to use
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="toInt"></param>
        /// <returns></returns>
        public string Convert(string Number, bool toInt);

        /// <summary>
        /// Initialize class
        /// </summary>
        /// <param name="_ValidSymbols"></param>
        /// <param name="_SymbolValues"></param>
        public void Init(List<string> _ValidSymbols, List<int> _SymbolValues);
    }
}

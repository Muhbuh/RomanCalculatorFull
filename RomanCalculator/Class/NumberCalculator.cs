using RomanCalculator.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator.Class
{
    /// <summary>
    /// This class is used to process mathematical operation on roman numerals.
    /// For now the configuration is directly implementet. In later version the
    /// implementation can be replaced with a configuration method
    /// </summary>
    public class NumberCalculator : NumeralCalculator
    {
        /// <summary>
        /// List of valid symbols
        /// </summary>
        List<string> ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        /// <summary>
        /// A list containing all combined values
        /// </summary>
        internal List<string> CombinedValues = new List<string>() { "IV", "IX", "XL", "XC", "CD", "CM" };

        /// <summary>
        /// Summand2 will be added to summand1
        /// </summary>
        /// <param name="summand1"></param>
        /// <param name="summand2"></param>
        /// <returns></returns>
        public string Addition(string summand1, string summand2)
        {

            // Safety in case this class is used without the app
            //Alternatively the NumberCheck cass can be used
            summand1 = summand1.ToUpper();
            summand2 = summand2.ToUpper();

            while (summand2 != "")
            {
                string _tmp = summand2[summand2.Length - 1].ToString();
                summand2 = summand2.Remove(summand2.Length - 1);

                if (_tmp == "M")
                {
                    summand1 = "M" + summand1;
                    continue;
                }

                summand1 = InsertValueIntoResult(summand1, _tmp);

                switch (_tmp)
                {
                    case "I":
                        //summand1 = AddRomanOne(summand1);
                        summand1 = CascadeOne(summand1);
                        break;
                    case "X":
                        summand1 = CascadeTen(summand1);
                        break;
                    case "C":
                        summand1 = CascadeHundred(summand1);
                        break;
                    case "V":
                        summand1 = AddRomanFive(summand1);
                        break;
                    case "L":
                        summand1 = AddRomanOne(summand1);
                        break;
                    case "D":
                        summand1 = AddRomanOne(summand1);
                        break;
                    default:
                        throw new Exception($"The letter {_tmp} is not a valid roman numeral");
                }
            }

            return summand1;
        }

        private string CascadeOne(string summand)
        {
            if (summand.Contains("IIV"))
            {
                summand = summand.Replace("IIV", "V");
                return summand;
            }

            if (summand.Contains("IIX"))
            {
                summand = summand.Replace("IIX", "X");
                return CascadeTen(summand);
            }

            if (summand.Contains("VIIII"))
            {
                return summand.Replace("VIIII", "IX");
            }

            if (summand.Contains("IIII"))
            {
                return summand.Replace("IIII", "IV");
            }

            return summand;
        }

        private string CascadeTen(string summand)
        {
            // Fix wrong formating
            if (summand.Contains("IXX"))
            {
                return summand.Replace("IXX", "XIX");
            }

            // Fix wrong formating
            if (summand.Contains("IXV"))
            {
                return summand.Replace("IXV", "XIV");
            }

            if (summand.Contains("XXL"))
            {
                summand = summand.Replace("XXL", "L");
                return summand;
            }

            if (summand.Contains("XXC"))
            {
                summand = summand.Replace("XXC", "C");
                return CascadeHundred(summand);
            }

            if (summand.Contains("XCX"))
            {
                summand = summand.Replace("XCX", "C");
                return CascadeHundred(summand);
            }

            if (summand.Contains("LXXXX"))
            {
                return summand.Replace("LXXXX", "XC");
            }

            if (summand.Contains("XXXX"))
            {
                return summand.Replace("XXXX", "XL");
            }

            return summand;
        }

        private string CascadeHundred(string summand)
        {
            // Fix wrong formating
            if (summand.Contains("XCC"))
            {
                return summand.Replace("XCC", "CXC");
            }

            // Fix wrong formating
            if (summand.Contains("XCL"))
            {
                return summand.Replace("XCL", "CXL");
            }

            if (summand.Contains("CCD"))
            {
                summand = summand.Replace("CCD", "D");
                return summand;
            }

            if (summand.Contains("CCM"))
            {
                return summand = summand.Replace("CCM", "M");
            }

            if (summand.Contains("CMC"))
            {
                return summand = summand.Replace("CMC", "M");
            }

            if (summand.Contains("DCCCCC"))
            {
                return summand.Replace("DCCCC", "CM");
            }

            if (summand.Contains("CCCC"))
            {
                return summand.Replace("CCCC", "CD");
            }

            return summand;
        }

        /// <summary>
        /// Check at which space the symbol needs to inserted into the string
        /// The symbol will be inserted write next to a copy of itself
        /// If no copy is found the string is searched for the next smaller value
        /// If no match is found the symbol is appended to the string
        /// </summary>
        /// <param name="summand"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public string InsertValueIntoResult(string summand, string symbol)
        {
            for (int i = ValidSymbols.IndexOf(symbol); i < ValidSymbols.Count; i++)
            {
                if (summand.Contains(ValidSymbols[i]))
                {
                    string _tmp;
                    int index = summand.IndexOf(ValidSymbols[i]);
                    if (index > 0 && ValidSymbols.Contains(summand.Substring(index - 1, 2)))
                    {
                        _tmp = summand.Substring(index-1);
                    }
                    else
                    {
                        _tmp = summand.Substring(index);
                    }

                    return summand.Replace(_tmp, symbol + _tmp);
                }
            }

            return summand + symbol;
        }

        /// <summary>
        /// This method adds a roman 1 to the string
        /// </summary>
        /// <param name="summand1"></param>
        /// <returns></returns>
        private string AddRomanOne(string summand)
        {
            string _block = GetLastSymbolBlock(summand);

            // If no I is contained in the last block, one can be appended
            if (!_block.Contains("IV") && !_block.Contains("IX") && !_block.Contains("III"))
            {
                summand += "I";
                return summand;
            }

            // IV can only be replaced by V
            if (_block.Contains("IV"))
            {
                summand = summand.Replace("IV", "V");
                return summand;
            }

            // Check if a cascade is started where XC has to be replaced
            if (_block.Contains("IX"))
            {
                // Replace 999 with 1000
                if (summand.Contains("CMXC"))
                {
                    summand = summand.Replace("CMXCIX", "M");
                }
                // Replace 99 with 1000
                else if (summand.Contains("XC"))
                {
                    summand = summand.Replace("XCIX", "C");
                }
                // Replace 9 with 10
                else
                {
                    summand = summand.Replace("IX", "X");
                }
            }

            // If a III is contained check wether IV or IX musst be set
            if (_block.Contains("III"))
            {
                if (_block[0].ToString() == "V")
                {
                    summand = summand.Replace(_block, "IX");
                }
                else
                {
                    summand = summand.Replace(_block, "IV");
                }
                return summand;
            }

            return summand;
        }

        /// <summary>
        /// Method to add a roman 5 to the string
        /// </summary>
        /// <param name="summand1"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string AddRomanFive(string summand)
        {
            if (summand.Contains("IV"))
            {
                return summand.Replace("IV", "IX");
            }
            else if (summand.Contains("IX"))
            {
                return summand.Replace("IX", "XIV");
            }
            // Check for cascade
            else if (summand.Contains("V"))
            {
                // Replace 995 with 1000
                if (summand.Contains("CMXCV"))
                {
                    summand = summand.Replace("CMXCV", "M");
                }
                else if (summand.Contains("XCV"))
                {
                    summand = summand.Replace("XCV", "C");
                }
                else
                {
                    summand = summand.Replace("V", "X");
                }
                return summand;
            }
            else if (summand.Contains("I"))
            {
                int firstI = summand.IndexOf("I");
                return summand.Replace(summand.Substring(firstI), "V" + summand.Substring(firstI));
            }
            // neither 4 nor 5 means V can be appended
            else
            {
                return summand + "V";
            }
        }

        private string GetLastSymbolBlock(string summand)
        {
            // A roman block cannot contain more than four symbols, e.g. VIII
            // The symbol M is ignored as a special case
            if (summand.All(x => x == 'M'))
            {
                return summand;
            }

            summand.Substring(Math.Max(0, summand.Length - 4));

            // Check if the last value is a combined one, e.g. IV
            if (CombinedValues.Contains(summand.Substring(Math.Max(0, summand.Length - 2))))
            {
                return summand.Substring(Math.Max(0, summand.Length - 2));
            }

            return summand;
        }
    }
}

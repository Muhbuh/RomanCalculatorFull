using RomanCalculator.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator.Class
{
    /// <summary>
    /// This class is used to process mathematical operation on roman numerals.
    /// For now the configuration is directly implementet. In later version the
    /// implementation can be replaced with a configuration method
    /// </summary>
    public class RomanNumberCalculator : INumeralCalculator
    {
        /// <summary>
        /// List of valid symbols
        /// </summary>
        private List<string> ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        /// <summary>
        /// A list containing all combined values
        /// </summary>
        private List<string> CombinedValues = new List<string>() { "IV", "IX", "XL", "XC", "CD", "CM" };

        /// <summary>
        /// List of maximum repeats
        /// </summary>
        private List<int> MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };

        /// <summary>
        /// Number checker
        /// </summary>
        private INumeralCheck Checker { get; set; }

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

            // Replacing the combined values result in less cases to consider in exchange for additional loops
            summand2 = summand2.Replace("IV", "IIII").Replace("IX","VIIII").Replace("XL","XXXX").Replace("XC","LXXXX").Replace("CD","CCCC").Replace("CM","DCCCC");

            // Increment to prevent infinite loop
            int SafteyCount = 0;

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
                        summand1 = CascadeFive(summand1);
                        break;
                    case "L":
                        summand1 = CascadeFifty(summand1);
                        break;
                    case "D":
                        summand1 = CascadeFiveHundred(summand1);
                        break;
                    default:
                        throw new Exception($"The letter {_tmp} is not a valid roman numeral");
                }

                if(SafteyCount >= 40000)
                {
                    throw new Exception($"Maximum amount of loops reached, aborting addition");
                }

                SafteyCount++;
            }

            return summand1;
        }

        private string CascadeFiveHundred(string summand)
        {
            if (summand.Contains("DD"))
            {
                summand = summand.Replace("DD", "M");
                return CascadeHundred(summand);
            }

            if (summand.Contains("DCD"))
            {
                summand = summand.Replace("DCD", "CM");
                return CascadeHundred(summand);
            }

            if (summand.Contains("DCM"))
            {
                summand = summand.Replace("DCM", "MCD");
                return CascadeHundred(summand);
            }

            return summand;
        }

        private string CascadeFifty(string summand)
        {
            if (summand.Contains("LL"))
            {
                summand = summand.Replace("LL", "C");
                return CascadeHundred(summand);
            }

            if (summand.Contains("LXL"))
            {
                summand = summand.Replace("LXL", "XC");
                return CascadeHundred(summand);
            }

            if (summand.Contains("LXC"))
            {
                summand = summand.Replace("LXC", "CXL");
                return CascadeHundred(summand);
            }

            return summand;
        }

        private string CascadeFive(string summand)
        {
            if(summand.Contains("VV"))
            {
                summand = summand.Replace("VV","X");
                return CascadeTen(summand);
            }

            if (summand.Contains("VIV"))
            {
                summand = summand.Replace("VIV", "IX");
                return CascadeTen(summand);
            }

            if (summand.Contains("VIX"))
            {
                summand = summand.Replace("VIX","XIV");
                CascadeTen(summand);
            }

            return summand;
        }

        /// <summary>
        /// Deal with cascade induced by the value I
        /// </summary>
        /// <param name="summand"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deal with cascade induced by the value X
        /// </summary>
        /// <param name="summand"></param>
        /// <returns></returns>
        private string CascadeTen(string summand)
        {
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

        /// <summary>
        /// Deal with cascade induced by the value C
        /// </summary>
        /// <param name="summand"></param>
        /// <returns></returns>
        private string CascadeHundred(string summand)
        {
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

        public RomanNumberCalculator(INumeralCheck _Checker)
        {
            if (_Checker == null)
            {
                throw new AggregateException("The number checker is not valid");
            }

            Checker = _Checker;
        }
    }
}

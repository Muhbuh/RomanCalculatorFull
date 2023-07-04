﻿using RomanCalculator.Interface;
using System;
using System.Collections.Generic;
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

            string _tmp = summand2[summand2.Length - 1].ToString();

            switch (_tmp)
            {
                case "I":
                    summand1 = AddRomanOne(summand1);
                    break;
                case "X":
                case "C":
                case "M":
                    break;
                case "V":
                case "L":
                case "D":
                    break;
                default:
                    throw new Exception($"The letter {_tmp} is not a valid roman numeral");
            }

            return summand1;
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
            if(!_block.Contains("IV") && !_block.Contains("IX") && !_block.Contains("III"))
            {
                summand += "I";
                return summand;
            }

            // IV can only be replaced by V
            if(_block.Contains("IV"))
            {
                summand = summand.Replace("IV", "V");
                return summand;
            }

            // Check if a cascade is started where XC has to be replaced
            if(_block.Contains("IX"))
            {
                // Replace 999 with 1000
                if(summand.Contains("CMXC"))
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
            if(CombinedValues.Contains(summand.Substring(Math.Max(0, summand.Length-2))))
            {
                return summand.Substring(Math.Max(0, summand.Length - 2));
            }

            return summand;
        }
    }
}

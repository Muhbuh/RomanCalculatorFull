using RomanCalculator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator.Class
{
    public class NumberConverter : NumeralConverter
    {
        public List<string> ValidSymbols { get; set; }
        public List<int> SymbolValues { get; set; }

        public string ConvertIntToString(int Number)
        {
            throw new NotImplementedException();
        }

        public int ConvertStringToInt(string Number)
        {
            int result = 0;

            for (int i = 0; i < Number.Length; i++)
            {
                string _tmp = Number[i].ToString();

                // Check if the current symbol belongs to a combined symbol
                if(i < Number.Length -1 && ValidSymbols.Contains(_tmp + Number[i + 1].ToString()))
                {
                    _tmp = _tmp + Number[i+1].ToString();
                    i++; // Incrementing i in the loop is not a good practice. In this case it keeps the code simple
                }

                result += SymbolValues[ValidSymbols.IndexOf(_tmp)];
            }

            return result;
        }

        public void Init(List<string> _ValidSymbols, List<int> _SymbolValues)
        {
            if (_ValidSymbols == null || _ValidSymbols.Count == 0)
            {
                throw new ArgumentException("The list of valid symbols is either no defined or empty", nameof(_ValidSymbols));
            }

            if (_SymbolValues == null || _ValidSymbols.Count != _SymbolValues.Count)
            {
                throw new ArgumentException("The list of symbol values is either no defined or does not have the same length as the valid symbol list", nameof(_SymbolValues));
            }

            ValidSymbols = _ValidSymbols;
            SymbolValues = _SymbolValues;
        }
    }
}

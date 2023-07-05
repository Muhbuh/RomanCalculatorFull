using RomanCalculator.Interface;
using System.Linq;

namespace RomanCalculator.Class
{
    /// <summary>
    /// This class is used to check if the string is a valid roman numeral
    /// Some basic configuration also for changes in values and order
    /// </summary>
    public class NumberCheck : INumeralCheck
    {
        public List<string> ValidSymbols { get; set; }

        public List<int> MaximumNumberOfRepeats { get; set; }

        public bool CheckNumeral(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                string _tmp = number[i].ToString();

                if (!ValidSymbols.Contains(_tmp))
                {
                    return false;
                }

                // Check if the valid symbol occurs more than the maximum amount of times
                if (number.Contains(new string(number[i], MaximumNumberOfRepeats[ValidSymbols.IndexOf(_tmp)] + 1)))
                {
                    return false;
                }

                // Check if this was the last symbol
                if (i < number.Length - 1)
                {
                    // If not the last symbol check if next symbol is NOT the same or a valid following symbol (right of the current symbol)
                    if (ValidSymbols.IndexOf(_tmp) > ValidSymbols.IndexOf(number[i + 1].ToString()))
                    {
                        // Create combined symbol
                        _tmp = _tmp + number[i + 1].ToString();

                        // The next symbol is left of the current one, check if this symbol combined with the next one is valid e.g. I + V = IV
                        if (!ValidSymbols.Contains(_tmp))
                        {
                            return false;
                        }

                        // Check if symbol after combined is right of the current symbol. Another combined symbol would not be valid, so no need to check
                        if (i < number.Length - 2)
                        {
                            // After a combined value the next valid value shifts by 3
                            if (ValidSymbols.IndexOf(_tmp) >= ValidSymbols.IndexOf(number[i + 2].ToString()) - 3)
                            {
                                return false;
                            }
                        }
                    }
                }
            }


            return true;
        }

        public void Init(List<string> _ValidSymbols, List<int> _MaximumNumberOfRepeats)
        {
            if (_ValidSymbols == null || _ValidSymbols.Count == 0)
            {
                throw new ArgumentException("The list of valid symbols is either no defined or empty", nameof(_ValidSymbols));
            }

            if (_MaximumNumberOfRepeats == null || _ValidSymbols.Count != _MaximumNumberOfRepeats.Count)
            {
                throw new ArgumentException("The list of maximum repeats is either no defined or does not have the same length as the valid symbol list", nameof(_MaximumNumberOfRepeats));
            }

            ValidSymbols = _ValidSymbols;
            MaximumNumberOfRepeats = _MaximumNumberOfRepeats;
        }
    }
}
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
        // In the long term it would be better to create a configuration class and use a JSON config file

        /// <summary>
        /// List of valid symbols in the numeral string
        /// The value i can be followed by any value with an higher index but not the other way around
        /// </summary>
        private List<string> ValidSymbols { get; set; }

        /// <summary>
        /// List with the number of maximum repeats for the valid symbols. List must have same length
        /// </summary>
        private List<int> MaximumNumberOfRepeats { get; set; }

        // ToDo: create a symbol class where every symbol contains rules on how it can be combined
        public bool CheckNumeral(string number)
        {
            if (number == "")
            {
                return false;
            }

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
                    // check if next symbol has a higher value than the current value, no need to check for repeat, was checked above
                    if (ValidSymbols.IndexOf(_tmp) > ValidSymbols.IndexOf(number[i + 1].ToString()))
                    {
                        // Create combined symbol
                        _tmp = _tmp + number[i + 1].ToString();

                        // check if this combined symbol is valid e.g. I + V = IV
                        if (!ValidSymbols.Contains(_tmp))
                        {
                            return false;
                        }

                        // Check if there is another symbol after the combined symbol
                        if (i < number.Length - 2)
                        {
                            // After a combined value the next symbol following in value is not allowed
                            // any symbol appearing in the combined symbol is not allowed
                            // This is roman numerals specific and needs to be changed for other systems
                            if (ValidSymbols.IndexOf(_tmp) >= ValidSymbols.IndexOf(number[i + 2].ToString()) - 1 || _tmp.Contains(number[i + 2]))
                            {
                                return false;
                            }
                        }
                    }
                }
            }


            return true;
        }

        public NumberCheck(List<string> _ValidSymbols, List<int> _MaximumNumberOfRepeats)
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

        //public void Init(List<string> _ValidSymbols, List<int> _MaximumNumberOfRepeats)
        //{
        //    if (_ValidSymbols == null || _ValidSymbols.Count == 0)
        //    {
        //        throw new ArgumentException("The list of valid symbols is either no defined or empty", nameof(_ValidSymbols));
        //    }

        //    if (_MaximumNumberOfRepeats == null || _ValidSymbols.Count != _MaximumNumberOfRepeats.Count)
        //    {
        //        throw new ArgumentException("The list of maximum repeats is either no defined or does not have the same length as the valid symbol list", nameof(_MaximumNumberOfRepeats));
        //    }

        //    ValidSymbols = _ValidSymbols;
        //    MaximumNumberOfRepeats = _MaximumNumberOfRepeats;
        //}
    }
}
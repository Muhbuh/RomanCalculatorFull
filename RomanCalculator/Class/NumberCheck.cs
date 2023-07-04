using RomanCalculator.Interface;

namespace RomanCalculator.Class
{
    /// <summary>
    /// This class is used to check if the string is a valid roman numeral
    /// Some basic configuration also for changes in values and order
    /// </summary>
    public class NumberCheck : NumeralCheck
    {
        public bool ValueDescends { get; set; }
        public List<string> ValidSymbols { get; set; }
        public List<string> ValidCombinations { get; set; }

        bool NumeralCheck.CheckNumeral(string number)
        {
            throw new NotImplementedException();
        }

        void NumeralCheck.Init(List<string> _ValidSymbols, List<string> _ValidCombinations, bool _ValueDescends)
        {
            if (_ValidSymbols == null || _ValidSymbols.Count == 0)
            {
                throw new ArgumentException("The list of valid symbols is either no defined or empty", nameof(_ValidSymbols));
            }

            if (_ValidSymbols == null)
            {
                // It is also possible to accept null and create a new list. For now I want it to be explicit
                throw new ArgumentException("The list of valid combinations is not defined", nameof(_ValidSymbols));
            }

            ValidSymbols = _ValidSymbols;
            ValidCombinations = _ValidCombinations;
            ValueDescends = _ValueDescends;
        }
    }
}
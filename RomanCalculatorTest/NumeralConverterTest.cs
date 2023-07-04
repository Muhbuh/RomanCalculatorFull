using RomanCalculator.Class;

namespace RomanCalculatorTest
{
    [TestClass]
    public class NumeralConverterTest
    {
        private NumberConverter Converter = new NumberConverter();

        [TestInitialize]
        public void Init()
        {
            List<string> ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<int> SymbolValues = new List<int>() { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            Converter.Init(ValidSymbols, SymbolValues);
        }

        [TestMethod]
        public void TestNCheckNumeralSimpleTrue()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<int> Expected = new List<int> { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.AreEqual(Converter.ConvertStringToInt(TestCases[i]), Expected[i]);
            }
        }
    }
}
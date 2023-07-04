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
        public void TestCheckNumeralToIntegerSimple()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<int> Expected = new List<int> { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.AreEqual(Converter.ConvertStringToInt(TestCases[i]), Expected[i]);
            }
        }

        [TestMethod]
        public void TestCheckNumeralToIntegerComplex()
        {
            List<string> TestCases = new List<string>() { "MDCLXVI", "MMMDXXXIII", "CIV", "CIX", "CML", "XIV", "MMMCMXCIX", "XCV", "CMX", "CMIV" };
            List<int> Expected = new List<int> { 1666, 3533, 104, 109, 950, 14, 3999, 95, 910, 904 };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.AreEqual(Converter.ConvertStringToInt(TestCases[i]), Expected[i]);
            }
        }

        [TestMethod]
        public void TestCheckNumeralToRomanSimple()
        {
            List<int> TestCases = new List<int> { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            List<string> Expected = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.AreEqual(Converter.ConvertIntToString(TestCases[i]), Expected[i]);
            }
        }

        [TestMethod]
        public void TestCheckNumeralToStringComplex()
        {
            List<int> TestCases = new List<int> { 1666, 3533, 104, 109, 950, 14, 3999, 95, 910, 904 };
            List<string> Expected = new List<string>() { "MDCLXVI", "MMMDXXXIII", "CIV", "CIX", "CML", "XIV", "MMMCMXCIX", "XCV", "CMX", "CMIV" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Console.WriteLine(TestCases[i]);
                Assert.AreEqual(Converter.ConvertIntToString(TestCases[i]), Expected[i]);
            }
        }
    }
}
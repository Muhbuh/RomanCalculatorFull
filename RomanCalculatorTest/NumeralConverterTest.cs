using RomanCalculator.Class;
using RomanCalculator.Interface;

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
            List<int> MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };

            INumeralCheck checker = new NumberCheck();
            checker.Init(ValidSymbols, MaximumNumberOfRepeats);

            Converter.Init(ValidSymbols, SymbolValues, checker);
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

        [TestMethod]
        public void TestCheckNumeralConverter()
        {
            List<string> TestCases = new List<string> { "MDCLXVI", "3533", "CIV", "109", "CML", "14", "MMMCMXCIX", "95", "CMX", "904" };
            List<string> Expected = new List<string> { "1666", "MMMDXXXIII", "104", "CIX", "950", "XIV", "3999", "XCV", "910", "CMIV" };
            List<bool> ConversionType = new List<bool>() { true, false, true, false, true, false, true, false, true, false };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Console.WriteLine(TestCases[i]);
                Assert.AreEqual(Converter.Convert(TestCases[i], ConversionType[i]), Expected[i]);
            }
        }
    }
}
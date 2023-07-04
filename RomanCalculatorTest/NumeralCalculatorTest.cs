using RomanCalculator.Class;

namespace RomanCalculatorTest
{
    [TestClass]
    public class NumeralCalculatorTest
    {
        private NumberCalculator Calculator = new NumberCalculator();

        [TestInitialize]
        public void Init()
        {
            List<string> ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<int> MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };

            //Checker.Init(ValidSymbols, MaximumNumberOfRepeats);
        }

        [TestMethod]
        public void TestAddOne()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "III", "VIII", "CMXCIX", "XCIX" };
            List<string> Expected = new List<string>() { "MI", "CMI", "DI", "CDI", "CI", "XCI", "LI", "XLI", "XI", "X", "VI", "V", "II", "IV", "IX", "M", "C" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.AreEqual(Calculator.Addition(TestCases[i], "I"), Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddFive()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "II", "III", "VIII", "CMXCV", "XCV" };
            List<string> Expected = new List<string>() { "MV", "CMV", "DV", "CDV", "CV", "XCV", "LV", "XLV", "XV", "XIV", "X", "IX", "VI", "VII", "VIII", "XIII", "M", "C" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                //Console.WriteLine(TestCases[i]);
                string result = Calculator.Addition(TestCases[i], "V");
                //Console.WriteLine(result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }
    }
}
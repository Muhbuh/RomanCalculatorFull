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

            Console.WriteLine("Test : Result : Expected");

            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.Addition(TestCases[i], "I");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddTen()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "III", "VIII", "CMXCIX", "XCIX" };
            List<string> Expected = new List<string>() { "MX", "CMX", "DX", "CDX", "CX", "C", "LX", "L", "XX", "XIX", "XV", "XIV", "XI", "XIII", "XVIII", "MIX", "CIX" };

            Console.WriteLine("Test : Result : Expected");

            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.Addition(TestCases[i], "X");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddHundred()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "III", "VIII", "CMXCIX", "XCIX" };
            List<string> Expected = new List<string>() { "MC", "M", "DC", "D", "CC", "CXC", "CL", "CXL", "CX", "CIX", "CV", "CIV", "CI", "CIII", "CVIII", "MXCIX", "CXCIX" };

            Console.WriteLine("Test : Result : Expected");

            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.Addition(TestCases[i], "C");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddThousand()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "III", "VIII", "CMXCIX", "XCIX" };
            List<string> Expected = new List<string>() { "MM", "MCM", "MD", "MCD", "MC", "MXC", "ML", "MXL", "MX", "MIX", "MV", "MIV", "MI", "MIII", "MVIII", "MCMXCIX", "MXCIX" };

            Console.WriteLine("Test : Result : Expected");

            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.Addition(TestCases[i], "M");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddFive()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "II", "III", "VIII", "CMXCV", "XCV" };
            List<string> Expected = new List<string>() { "MV", "CMV", "DV", "CDV", "CV", "XCV", "LV", "XLV", "XV", "XIV", "X", "IX", "VI", "VII", "VIII", "XIII", "M", "C" };

            Console.WriteLine("Test : Result : Expected");
            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.Addition(TestCases[i], "V");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddInsertValueIntoResult()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "II", "III", "VIII", "CMXCV", "XCV", "XCXL", "CXLV" };
            List<string> Expected = new List<string>() { "MX", "CMX", "DX", "CDX", "CX", "XXC", "LX", "XXL", "XX", "XIX", "XV", "XIV", "XI", "XII", "XIII", "XVIII", "CMXXCV", "XXCV", "XXCXL", "CXXLV"};

            Console.WriteLine("Test : Result : Expected");
            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.InsertValueIntoResult(TestCases[i], "X");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }
    }
}
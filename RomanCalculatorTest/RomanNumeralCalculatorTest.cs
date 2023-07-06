using RomanCalculator.Class;
using RomanCalculator.Interface;

namespace RomanCalculatorTest
{
    [TestClass]
    public class RomanNumeralCalculatorTest
    {
        private RomanNumberCalculator Calculator = null;

        [TestInitialize]
        public void Init()
        {
            List<string> ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<int> MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };

            INumeralCheck checker = new NumberCheck(ValidSymbols, MaximumNumberOfRepeats);

            Calculator = new RomanNumberCalculator(checker);
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
        public void TestAddFifty()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "II", "III", "VIII", "CMXCV", "XCV" };
            List<string> Expected = new List<string>() { "MD", "MCD", "M", "CM", "DC", "DXC", "DL", "DXL", "DX", "DIX", "DV", "DIV", "DI", "DII", "DIII", "DVIII", "MCDXCV", "DXCV" };

            Console.WriteLine("Test : Result : Expected");
            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.Addition(TestCases[i], "D");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddFivehundred()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "II", "III", "VIII", "CMXCV", "XCV" };
            List<string> Expected = new List<string>() { "ML", "CML", "DL", "CDL", "CL", "CXL", "C", "XC", "LX", "LIX", "LV", "LIV", "LI", "LII", "LIII", "LVIII", "MXLV", "CXLV" };

            Console.WriteLine("Test : Result : Expected");
            for (int i = 0; i < TestCases.Count; i++)
            {
                string result = Calculator.Addition(TestCases[i], "L");
                Console.WriteLine(TestCases[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddition()
        {
            List<string> Summand1 = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "II", "III", "CMXCV", "XCV", "MMMCMXCIX", "III" };
            List<string> Summand2 = new List<string>() { "ML", "CMX", "DV", "CDXIV", "CCC", "CXL", "XX", "XXIV", "LX", "LIX", "IV", "LIV", "XI", "XIV", "LVIII", "MXLV", "CXLV", "MMMCMXCIX", "III" };
            List<string> Expected = new List<string>() { "MML", "MDCCCX", "MV", "DCCCXIV", "CD", "CCXXX", "LXX", "LXIV", "LXX", "LXVIII", "IX", "LVIII", "XII", "XVI", "LXI", "MMXL", "CCXL", "MMMMMMMCMXCVIII", "VI"};

            Console.WriteLine("Summand1 : Summand2 : Result : Expected");
            for (int i = 0; i < Expected.Count; i++)
            {
                string result = Calculator.Addition(Summand1[i], Summand2[i]);
                Console.WriteLine(Summand1[i] + " : " + Summand2[i] + " : " + result + " : " + Expected[i]);
                Assert.AreEqual(result, Expected[i]);
            }
        }

        [TestMethod]
        public void TestAddInsertValueIntoResult()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I", "II", "III", "VIII", "CMXCV", "XCV", "XCXL", "CXLV" };
            List<string> Expected = new List<string>() { "MX", "CMX", "DX", "CDX", "CX", "XXC", "LX", "XXL", "XX", "XIX", "XV", "XIV", "XI", "XII", "XIII", "XVIII", "CMXXCV", "XXCV", "XXCXL", "CXXLV" };

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
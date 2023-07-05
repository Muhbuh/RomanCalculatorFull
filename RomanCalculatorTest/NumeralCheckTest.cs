using RomanCalculator.Class;

namespace RomanCalculatorTest
{
    [TestClass]
    public class NumeralCheckTest
    {
        private NumberCheck Checker = new NumberCheck();

        [TestInitialize]
        public void Init()
        {
            List<string> ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<int> MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };

            Checker.Init(ValidSymbols, MaximumNumberOfRepeats);
        }

        [TestMethod]
        public void TestCheckNumeralSimpleTrue()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.IsTrue(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestCheckNumeralSimpleFalse()
        {
            List<string> TestCases = new List<string>() { "A", "B", "E", "W", "Y", "Z" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.IsFalse(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestCheckNumeralConcatedTrue()
        {
            List<string> TestCases = new List<string>() { "MDCLXVI", "MMMDXXXIII", "CIV", "CIX", "CML", "XIV", "MMMCMXCIX", "XCV", "CMX", "CMIV" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                //Console.WriteLine(TestCases[i]);
                Assert.IsTrue(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestCheckNumeralConcatedFalse()
        {
            List<string> TestCases = new List<string>() { "MDCLVXI", "MMMDXMXXIII", "CIVD", "CIM", "CMD", "XIL", "MMMCVXCIX", "XCX", "CMC", "CMCD", "IIII", "VV", "XXXX", "LL", "CCCC", "DD", "" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Console.WriteLine(TestCases[i]);
                Assert.IsFalse(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestCheckNumeralRepeatsTrue()
        {
            List<string> TestCases = new List<string>() { "MMM", "CM", "D", "CD", "CCC", "XC", "L", "XL", "XXX", "IX", "V", "IV", "III" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.IsTrue(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestCheckNumeralRepeatFalse()
        {
            List<string> TestCases = new List<string>() { "MMMM", "CMCM", "DD", "CDCD", "CCCC", "XCXC", "LL", "XLXL", "XXXX", "IXIX", "VV", "IVIV", "IIII" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Console.WriteLine(TestCases[i]);
                Assert.IsFalse(Checker.CheckNumeral(TestCases[i]));
            }
        }
    }
}
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
        public void TestNCheckNumeralSimpleTrue()
        {
            List<string> TestCases = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.IsTrue(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestNCheckNumeralSimpleFalse()
        {
            List<string> TestCases = new List<string>() { "A", "B", "E", "W", "Y", "Z" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.IsFalse(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestNCheckNumeralConcatedTrue()
        {
            List<string> TestCases = new List<string>() { "MDCLXVI", "MMMDXXXIII", "CIV", "CIX", "CML", "XIV", "MMMCMXCIX", "XCV", "CMX", "CMIV" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                //Console.WriteLine(TestCases[i]);
                Assert.IsTrue(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestNCheckNumeralConcatedFalse()
        {
            List<string> TestCases = new List<string>() { "MDCLVXI", "MMMDXMXXIII", "CIVD", "CIM", "CMD", "XIL", "MMMCVXCIX", "XCX", "CMC", "CMCD" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                //Console.WriteLine(TestCases[i]);
                Assert.IsFalse(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestNCheckNumeralRepeatsTrue()
        {
            List<string> TestCases = new List<string>() { "MMM", "CM", "D", "CD", "CCC", "XC", "L", "XL", "XXX", "IX", "V", "IV", "III" };

            for (int i = 0; i < TestCases.Count; i++)
            {
                Assert.IsTrue(Checker.CheckNumeral(TestCases[i]));
            }
        }

        [TestMethod]
        public void TestNCheckNumeralRepeatFalse()
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
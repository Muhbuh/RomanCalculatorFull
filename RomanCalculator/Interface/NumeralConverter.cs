using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator.Interface
{
    internal interface NumberConversion
    {
        public int ConvertStringToInt(string Number);

        public string ConvertIntToString(int Number);
    }
}

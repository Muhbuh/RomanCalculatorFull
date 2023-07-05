using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator.Interface
{
    /// <summary>
    /// Interface for calculation of symbolic numbers
    /// </summary>
    public interface INumeralCalculator
    {
        /// <summary>
        /// Method to add two symbolic numbers together
        /// </summary>
        /// <param name="summand1"></param>
        /// <param name="summand2"></param>
        /// <returns></returns>
        public string Addition(string summand1,  String summand2);
    }
}

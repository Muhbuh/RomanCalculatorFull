using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using webapi.Interface;
using RomanCalculator.Interface;
using RomanCalculator.Class;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.Localization;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RomanCalculatorController : ControllerBase
{
    private readonly ILogger<RomanCalculatorController> _logger;

    private INumeralCheck Checker { get; set; } = null;
    private INumeralConverter Converter { get; set; } = null;
    private INumeralCalculator Calculator { get; set; } = null;
    private List<string> ValidSymbols = null;
    private List<int> MaximumNumberOfRepeats = null;
    private List<int> SymbolValues = null;

    /// <summary>
    /// Chached Dropdowndata values
    /// </summary>
    List<DropDownData> DDdata = null;

    public RomanCalculatorController(ILogger<RomanCalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetDropDownData")]
    public IEnumerable<DropDownData> GetDropDownData()
    {

        if (DDdata != null)
        {
            return DDdata;
        }

        DDdata = new List<DropDownData>();

        // Replace with JSON config file later
        DDdata.Add(new DropDownData() { id = 0, text = "Roman" });
        DDdata.Add(new DropDownData() { id = 1, text = "Arabic" });

        return DDdata;
    }

    [HttpGet(Name = "CheckNum")]
    public JsonResult CheckNum(string number = "", int numberType = 0)
    {
        if (Checker == null)
        {
            InitLogic();
        }

        bool success = CheckNumber(number, numberType);

        string text = "";

        if (!success && number != "")
        {
            text = "Input is not a valid number";
            var data1 = new { success = success, text = text };
            JsonResult res1 = new JsonResult(data1);
            return res1;
        }

        //Check limits
        if (numberType == 0 && number.Contains("MMMM"))
        {
            success = false;
            text = "Value needs to be between I and MMMCMXCIX";
        }
        else if (numberType == 1 && Int32.TryParse(number, out _)) // Safety Parse
        {
            int _tmp = Int32.Parse(number); //Always works, since TryParse was successful before

            if (_tmp < 1 || _tmp > 4000)
            {
                success = false;
                text = "Value needs to be between 1 and 4000";
            }

        }

        var data = new { success = success, text = text };
        JsonResult res = new JsonResult(data);
        return res;
    }

    [HttpGet(Name = "ConvertNumbers")]
    public JsonResult ConvertNumbers(string summand1 = "", string summand2 = "", string result = "", int oldType = 0, int newType = 0)
    {
        bool success = true;

        string field1 = "", field2 = "", field3 = "";

        if (Converter == null)
        {
            InitLogic();
        }

        if (oldType == 0)
        {
            if (Checker.CheckNumeral(summand1))
            {
                field1 = ConvertFromTypeToType(summand1, oldType, newType);
            }

            if (Checker.CheckNumeral(summand2))
            {
                field2 = ConvertFromTypeToType(summand2, oldType, newType);
            }

            if (Checker.CheckNumeral(result))
            {
                field3 = ConvertFromTypeToType(result, oldType, newType);
            }
        }
        else if (oldType == 1)
        {
            int _tmp = 0;
            if (Int32.TryParse(summand1, out _tmp))
            {
                if (_tmp > 0 && _tmp < 4000)
                {
                    field1 = ConvertFromTypeToType(summand1, oldType, newType);
                }
            }

            if (Int32.TryParse(summand2, out _tmp))
            {
                if (_tmp > 0 && _tmp < 4000)
                {
                    field2 = ConvertFromTypeToType(summand2, oldType, newType);
                }
            }

            if (Int32.TryParse(result, out _tmp))
            {
                if (_tmp > 0 && _tmp < 4000)
                {
                    field3 = ConvertFromTypeToType(result, oldType, newType);
                }
            }
        }

        var data = new { success = success, field1 = field1, field2 = field2, field3 = field3 };
        JsonResult res = new JsonResult(data);
        return res;
    }

    /// <summary>
    /// Improve this method later to allow conversion from any type to any
    /// </summary>
    /// <param name="number"></param>
    /// <param name="oldType"></param>
    /// <param name="newType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// Change the whole logic: Always convert to Arabic first and then to target
    /// This way there is no Quadratic growth for each new number type
    private string ConvertFromTypeToType(string number, int oldType, int newType)
    {
        if (number == "")
        {
            return "";
        }

        switch (oldType)
        {
            case 0:
                return Converter.Convert(number, true);
            case 1:
                return Converter.Convert(number, false);
            default:
                throw new ArgumentException($"Type {oldType} is not valid");
        }
    }

    [HttpGet(Name = "GetSum")]
    public JsonResult GetSum(string summand1 = "", string summand2 = "", int numberType = 0)
    {
        bool success = false;
        string text = "";

        if (Calculator == null)
        {
            InitLogic();
        }

        // Safety check, should not be needed
        if (!CheckNumber(summand1, numberType))
        {
            text = "The first value is not a valid number!";
            var data1 = new { success = success, text = text };
            JsonResult res1 = new JsonResult(data1);
            return res1;
        }

        // Safety check, should not be needed
        if (!CheckNumber(summand1, numberType))
        {
            text = "The second value is not a valid number!";
            var data2 = new { success = success, text = text };
            JsonResult res2 = new JsonResult(data2);
            return res2;
        }

        summand1 = ConvertToRomanNumber(summand1, numberType);
        summand2 = ConvertToRomanNumber(summand2, numberType);

        text = Calculator.Addition(summand1, summand2);

        if (text.Contains("MMMM"))
        {
            string _limit = "MMMCMXCIX";
            switch (numberType)
            {
                case 0:
                    break;
                case 1:
                    _limit = "3999";
                    break;
                default:
                    break;
            }

            text = $"Sum exceeds limit of {_limit}";
            success = false;
        }
        else
        {
            success = true;
            text = ConvertInputToNumberType(text, numberType);
        }



        var data = new { success = success, text = text };
        JsonResult res = new JsonResult(data);
        return res;
    }

    /// <summary>
    /// Check if input is a valid number based on numbertype
    /// </summary>
    /// <param name="number"></param>
    /// <param name="numberType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private bool CheckNumber(string number, int numberType)
    {
        switch (numberType)
        {
            case 0:
                return Checker.CheckNumeral(number);
            case 1:
                return Int32.TryParse(number, out _);
            default:
                throw new ArgumentException($"The number type {numberType} is not valid");
        }
    }

    private string ConvertToRomanNumber(string number, int numberType)
    {
        if (Converter == null)
        {
            InitLogic();
        }
        // No need to change roman to roman
        if (numberType == 0)
        {
            return number;
        }

        switch (numberType)
        {
            case (1):
                number = Converter.Convert(number, false);
                return number;
            default:
                return "The number type is not valid";
        }
    }

    /// <summary>
    /// Convert the input into a int number for the calculator
    /// </summary>
    /// <param name="summand2"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private string ConvertInputToNumberType(string number, int numberType)
    {
        // No need to change roman to roman
        if (numberType == 0)
        {
            return number;
        }

        switch (numberType)
        {
            case (1):
                number = Converter.Convert(number, true);
                return number;
            default:
                return "The number type is not valid";
        }
    }

    /// <summary>
    /// Initialize all used classes and list
    /// ToDo: add loading config from Json files
    /// </summary>
    private void InitLogic()
    {

        ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };
        SymbolValues = new List<int>() { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        Checker = new NumberCheck(ValidSymbols, MaximumNumberOfRepeats);
        Converter = new NumberConverter(ValidSymbols, SymbolValues, Checker);
        Calculator = new RomanNumberCalculator(Checker);
    }
}

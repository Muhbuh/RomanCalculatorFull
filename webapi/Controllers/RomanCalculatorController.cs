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
        if(Checker == null)
        {
            InitLogic();
        }

        bool success = CheckNumber(number, numberType);

        string text = "";

        if(!success && number != "")
        {
            text = "Input is not a valid number";
        }

        var data = new { success = false, text = text};
        JsonResult res = new JsonResult(data);
        return res;
    }

    [HttpGet(Name = "ConvertNumbers")]
    public JsonResult ConvertNumbers(string summand1 = "", string summand2 = "", string result = "", int numberType = 0)
    {


        var data = new { success = false, text = "" };
        JsonResult res = new JsonResult(data);
        return res;
    }

    [HttpGet(Name = "GetSum")]
    public JsonResult GetSum(string summand1 = "", string summand2 = "", int numberType = 0)
    {
        bool success = false;
        string text = "";

        if(Calculator == null)
        {
            InitLogic();
        }

        Checker.Init(ValidSymbols, MaximumNumberOfRepeats);

        if (!CheckNumber(summand1, numberType))
        {
            text = "The first value is not a valid number!";
        }

        if (!CheckNumber(summand1, numberType))
        {
            text = "The second value is not a valid number!";
        }

        if (text == "")
        {
            summand1 = ConvertToRomanNumber(summand1, numberType);
            summand2 = ConvertToRomanNumber(summand2, numberType);

            text = Calculator.Addition(summand1, summand2);

            if (text.Contains("MMMM"))
            {
                text = "Sum exceeds limit of MMMCMXCIX";
                success = false;
            }
            else
            {
                success = true;
                text = ConvertInputToNumerType(text, numberType);
            }
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
        switch(numberType)
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
        if(Converter == null)
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
    /// Convert the input into a roman number for the calculator
    /// </summary>
    /// <param name="summand2"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private string ConvertInputToNumerType(string number, int numberType)
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
        Checker = new NumberCheck();
        Calculator = new RomanNumberCalculator();
        Converter = new NumberConverter();
        ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };
        SymbolValues = new List<int>() { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        Checker.Init(ValidSymbols, MaximumNumberOfRepeats);
        Converter.Init(ValidSymbols, SymbolValues);
    }
}

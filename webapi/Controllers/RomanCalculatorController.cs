using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using webapi.Interface;
using RomanCalculator.Interface;
using RomanCalculator.Class;
using static System.Net.Mime.MediaTypeNames;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RomanCalculatorController : ControllerBase
{
    private readonly ILogger<RomanCalculatorController> _logger;

    /// <summary>
    /// The current active number type. 0 is the default and in this case Roman
    /// </summary>
    private int NumberType = 0;

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

    [HttpGet(Name = "GetSum")]
    public JsonResult GetSum(string summand1 = "", string summand2 = "")
    {
        bool success = false;
        string text = "";

        if(Calculator == null)
        {
            InitLogic();
        }

        Checker.Init(ValidSymbols, MaximumNumberOfRepeats);

        //if (!Checker.CheckNumeral(summand1))
        //{
        //    text = "The first value is not a valid number!";
        //}

        //if (!Checker.CheckNumeral(summand2))
        //{
        //    text = "The second value is not a valid number!";
        //}

        if (text == "")
        {
            summand1 = ConvertToRomanNumber(summand1);
            summand2 = ConvertToRomanNumber(summand2);

            text = Calculator.Addition(summand1, summand2);

            if (text.Contains("MMMM"))
            {
                text = "Sum exceeds limit of MMMCMXCIX";
                success = false;
            }
            else
            {
                success = true;
                text = ConvertInputToNumerType(text);
            }
        }


        var data = new { success = success, text = text };
        JsonResult res = new JsonResult(data);
        return res;
    }

    private string ConvertToRomanNumber(string number)
    {
        if(Converter == null)
        {
            InitLogic();
        }
        // No need to change roman to roman
        if (NumberType == 0)
        {
            return number;
        }

        switch (NumberType)
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
    private string ConvertInputToNumerType(string number)
    {
        // No need to change roman to roman
        if (NumberType == 0)
        {
            return number;
        }

        switch (NumberType)
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
    }

    /// <summary>
    /// Method to change the number type
    /// No callback needed, potential errors are already filtered out
    /// Option: add check if the input is in range of the drop down data
    /// </summary>
    /// <param name="type"></param>
    [HttpGet(Name = "ChangeNumberType")]
    public void ChangeNumberType(int type)
    {
        NumberType = type;
    }
}

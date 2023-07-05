using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using webapi.Interface;
using RomanCalculator.Interface;
using RomanCalculator.Class;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RomanCalculatorController : ControllerBase
{
    private readonly ILogger<RomanCalculatorController> _logger;

    private INumeralCheck Checker { get; set; }
    private INumeralCalculator Calculator { get; set; }

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
        DDdata.Add(new DropDownData() { id = 1, text = "Roman" });
        DDdata.Add(new DropDownData() { id = 2, text = "Arabic" });

        return DDdata;
    }

    [HttpGet(Name = "GetSum")]
    public JsonResult GetSum(string summand1 = "", string summand2 = "")
    {
        bool success = false;
        string text = "";

        Checker = new NumberCheck();
        Calculator = new RomanNumberCalculator();

        List<string> ValidSymbols = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        List<int> MaximumNumberOfRepeats = new List<int>() { 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3 };

        Checker.Init(ValidSymbols, MaximumNumberOfRepeats);

        if (!Checker.CheckNumeral(summand1))
        {
            text = "The first value is not a valid number!";
        }

        if (!Checker.CheckNumeral(summand2))
        {
            text = "The second value is not a valid number!";
        }

        if (text == "")
        {
            text = Calculator.Addition(summand1, summand2);
            success = true;

            if (text.Contains("MMMM"))
            {
                text = "Sum exceeds limit of MMMCMXCIX";
                success = false;
            } 
        }


       var data = new { success = success, text = text};
        JsonResult res = new JsonResult(data);
        return res;
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using webapi.Interface;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class RomanCalculatorController : ControllerBase
{
    private readonly ILogger<RomanCalculatorController> _logger;

    /// <summary>
    /// Chached Dropdowndata values
    /// </summary>
    List<DropDownData> DDdata = null;

    public RomanCalculatorController(ILogger<RomanCalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetDropDownData")]
    public IEnumerable<DropDownData> Get()
    {

        if(DDdata != null)
        {
            return DDdata;
        }

        DDdata = new List<DropDownData>();

        // Replace with JSON config file later
        DDdata.Add(new DropDownData() { id = 1, text = "Roman" });
        DDdata.Add(new DropDownData() { id = 2, text = "Arabic" });

        return DDdata;
    }
}

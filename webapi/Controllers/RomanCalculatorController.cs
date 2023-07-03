using Microsoft.AspNetCore.Mvc;
using webapi.Interface;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class RomanCalculatorController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public RomanCalculatorController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetDropDownData")]
    public IEnumerable<DropDownData> GetDropDownData()
    {
        return null;
    }
}

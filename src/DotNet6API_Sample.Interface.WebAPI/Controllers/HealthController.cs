using Microsoft.AspNetCore.Mvc;

namespace DotNet6API_Sample.Interface.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : Controller
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogDebug("Get health called");
        
        return Ok($"{DateTime.UtcNow:UTC:yyyy-MM-dd HH:mm:ss}");
    }
}
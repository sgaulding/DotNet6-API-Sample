using DotNet6API_Sample.Library.Models;
using DotNet6API_Sample.Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6API_Sample.Interface.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    private readonly UserService _userService;

    public UsersController(ILogger<UsersController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public IEnumerable<UserRecord> GetUsers()
    {
        _logger.LogDebug("Get all users called");
        
        return _userService.GetUsers();
    }

    [HttpGet("{userID}")]
    public UserRecord? GetUserByID(Guid userID)
    {
        _logger.LogDebug("Get user by ID {UserID} called", userID);

        return _userService.GetUser(userID);
    }

    [HttpPost()]
    public Guid SaveUser(UserRecord userRecord)
    {
        _logger.LogDebug("Save user FN:{FirstName} LN:{LastName} Email:({EmailAddress}) called", 
            userRecord.FirstName,
            userRecord.LastName, 
            userRecord.EmailAddress);
        
        return _userService.SaveUser(userRecord);
    }
}
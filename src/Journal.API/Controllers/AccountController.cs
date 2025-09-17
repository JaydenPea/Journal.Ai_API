using Journal.Application.Consts;
using Journal.Application.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet(ApiRoutes.Account.GetProfile)]
    public async Task<IActionResult> GetProfile(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("User ID is required");
        }

        var profile = await _accountService.GetProfileAsync(userId);
        
        if (profile == null)
        {
            return NotFound($"User with ID '{userId}' not found");
        }

        return Ok(profile);
    }
}
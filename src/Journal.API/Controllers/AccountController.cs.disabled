using Journal.Application.Consts;
using Journal.Application.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Controllers;

/// <summary>
/// User account management operations
/// </summary>
[ApiController]
[Produces("application/json")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// Get user profile information
    /// </summary>
    /// <param name="userId">The user ID to get profile for</param>
    /// <returns>User profile data</returns>
    [HttpGet(ApiRoutes.Account.GetProfile)]
    [ProducesResponseType(typeof(Journal.Application.DTOs.UserProfileDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
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
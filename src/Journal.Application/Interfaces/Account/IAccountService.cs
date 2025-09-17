using Journal.Application.DTOs;

namespace Journal.Application.Interfaces.Account;

public interface IAccountService
{
    Task<UserProfileDto?> GetProfileAsync(string userId);
}

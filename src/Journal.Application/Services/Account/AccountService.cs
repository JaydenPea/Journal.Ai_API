using Journal.Application.DTOs;
using Journal.Application.Interfaces.Account;
using Journal.Application.Mappers;
using Journal.Domain.Repositories;

namespace Journal.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileDto?> GetProfileAsync(string userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user == null ? null : UserMapper.ToDto(user);
    }
}
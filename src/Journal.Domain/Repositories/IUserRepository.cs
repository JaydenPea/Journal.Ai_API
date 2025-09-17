using Journal.Domain.Entities;

namespace Journal.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string userId);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(string userId);
    Task<bool> ExistsAsync(string userId);
}
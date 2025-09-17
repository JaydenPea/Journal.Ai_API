using Journal.Domain.Entities;
using Journal.Domain.Repositories;
using Journal.Infrastructure.Models;
using Supabase;

namespace Journal.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Client _supabaseClient;

    public UserRepository(Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<User?> GetByIdAsync(string userId)
    {
        try
        {
            var userEntity = await _supabaseClient
                .From<UserEntity>()
                .Select("*")
                .Where(x => x.Id == userId)
                .Single();

            return userEntity == null ? null : MapToDomain(userEntity);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<User> CreateAsync(User user)
    {
        var userEntity = MapToEntity(user);
        var result = await _supabaseClient
            .From<UserEntity>()
            .Insert(userEntity);

        return MapToDomain(result.Model!);
    }

    public async Task<User> UpdateAsync(User user)
    {
        var userEntity = MapToEntity(user);
        var result = await _supabaseClient
            .From<UserEntity>()
            .Update(userEntity);

        return MapToDomain(result.Model!);
    }

    public async Task<bool> DeleteAsync(string userId)
    {
        try
        {
            await _supabaseClient
                .From<UserEntity>()
                .Where(x => x.Id == userId)
                .Delete();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ExistsAsync(string userId)
    {
        try
        {
            var result = await _supabaseClient
                .From<UserEntity>()
                .Select("id")
                .Where(x => x.Id == userId)
                .Single();
            return result != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static User MapToDomain(UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            Email = entity.Email,
            DisplayName = entity.DisplayName,
            AvatarUrl = entity.AvatarUrl,
            StartingBalance = entity.StartingBalance,
            GamificationAccountId = entity.GamificationAccountId,
            Timezone = entity.Timezone,
            Currency = entity.Currency,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    private static UserEntity MapToEntity(User domain)
    {
        return new UserEntity
        {
            Id = domain.Id,
            Email = domain.Email,
            DisplayName = domain.DisplayName,
            AvatarUrl = domain.AvatarUrl,
            StartingBalance = domain.StartingBalance,
            GamificationAccountId = domain.GamificationAccountId,
            Timezone = domain.Timezone,
            Currency = domain.Currency,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt
        };
    }
}
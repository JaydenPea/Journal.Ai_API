using Journal.Application.DTOs;
using Journal.Domain.Entities;

namespace Journal.Application.Mappers;

public static class UserMapper
{
    public static UserProfileDto ToDto(User user)
    {
        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            AvatarUrl = user.AvatarUrl,
            StartingBalance = user.StartingBalance,
            GamificationAccountId = user.GamificationAccountId,
            Timezone = user.Timezone,
            Currency = user.Currency,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public static User ToDomain(UserProfileDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Email = dto.Email,
            DisplayName = dto.DisplayName,
            AvatarUrl = dto.AvatarUrl,
            StartingBalance = dto.StartingBalance,
            GamificationAccountId = dto.GamificationAccountId,
            Timezone = dto.Timezone,
            Currency = dto.Currency,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
    }
}
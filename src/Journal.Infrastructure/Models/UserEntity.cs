using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Journal.Infrastructure.Models;

[Table("users")]
public class UserEntity : BaseModel
{
    [PrimaryKey("UID")]
    public string Id { get; set; } = string.Empty;

    [Column("Email")]
    public string Email { get; set; } = string.Empty;

    [Column("display_name")]
    public string? DisplayName { get; set; }

    [Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    [Column("starting_balance")]
    public decimal StartingBalance { get; set; } = 0;

    [Column("gamification_account_id")]
    public string? GamificationAccountId { get; set; }

    [Column("timezone")]
    public string Timezone { get; set; } = "UTC";

    [Column("currency")]
    public string Currency { get; set; } = "USD";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
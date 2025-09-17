using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Journal.Infrastructure.Models;

[Table("trading_accounts")]
public class TradingAccountEntity : BaseModel
{
    [PrimaryKey("id")]
    public string Id { get; set; } = string.Empty;

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("broker")]
    public string Broker { get; set; } = string.Empty;

    [Column("account_type")]
    public string AccountType { get; set; } = string.Empty;

    [Column("currency")]
    public string Currency { get; set; } = "USD";

    [Column("starting_balance")]
    public decimal StartingBalance { get; set; }

    [Column("current_balance")]
    public decimal CurrentBalance { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
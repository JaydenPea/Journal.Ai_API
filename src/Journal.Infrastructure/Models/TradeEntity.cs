using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Journal.Infrastructure.Models;

[Table("trades")]
public class TradeEntity : BaseModel
{
    [PrimaryKey("id")]
    public string Id { get; set; } = string.Empty;

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("trading_account_id")]
    public string TradingAccountId { get; set; } = string.Empty;

    [Column("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [Column("trade_type")]
    public string TradeType { get; set; } = string.Empty;

    [Column("position_size")]
    public decimal PositionSize { get; set; }

    [Column("entry_price")]
    public decimal EntryPrice { get; set; }

    [Column("exit_price")]
    public decimal? ExitPrice { get; set; }

    [Column("entry_date")]
    public DateTime EntryDate { get; set; }

    [Column("exit_date")]
    public DateTime? ExitDate { get; set; }

    [Column("status")]
    public string Status { get; set; } = string.Empty;

    [Column("outcome")]
    public string? Outcome { get; set; }

    [Column("realized_pnl")]
    public decimal? RealizedPnl { get; set; }

    [Column("strategy")]
    public string? Strategy { get; set; }

    [Column("strategy_id")]
    public string? StrategyId { get; set; }

    [Column("setup")]
    public string? Setup { get; set; }

    [Column("stop_loss")]
    public decimal? StopLoss { get; set; }

    [Column("take_profit")]
    public decimal? TakeProfit { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
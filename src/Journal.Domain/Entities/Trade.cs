namespace Journal.Domain.Entities;

public class Trade
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string TradingAccountId { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string TradeType { get; set; } = string.Empty; // 'long', 'short'
    public decimal PositionSize { get; set; }
    public decimal EntryPrice { get; set; }
    public decimal? ExitPrice { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string Status { get; set; } = string.Empty; // 'open', 'closed'
    public string? Outcome { get; set; } // 'win', 'loss', 'breakeven'
    public decimal? RealizedPnl { get; set; }
    public string? Strategy { get; set; }
    public string? StrategyId { get; set; }
    public string? Setup { get; set; }
    public decimal? StopLoss { get; set; }
    public decimal? TakeProfit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
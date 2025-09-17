namespace Journal.Application.DTOs;

public class TradeDto
{
    public string Id { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string TradeType { get; set; } = string.Empty;
    public decimal PositionSize { get; set; }
    public decimal EntryPrice { get; set; }
    public decimal? ExitPrice { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Outcome { get; set; }
    public decimal? RealizedPnl { get; set; }
    public string? Strategy { get; set; }
    public string? StrategyId { get; set; }
    public string? Setup { get; set; }
    public decimal? StopLoss { get; set; }
    public decimal? TakeProfit { get; set; }
    public TradingAccountDto? TradingAccount { get; set; }
    public List<TradeTagDto> Tags { get; set; } = new();
}

public class TradeCreateDto
{
    public string Symbol { get; set; } = string.Empty;
    public string TradeType { get; set; } = string.Empty;
    public decimal PositionSize { get; set; }
    public decimal EntryPrice { get; set; }
    public DateTime EntryDate { get; set; }
    public string TradingAccountId { get; set; } = string.Empty;
    public string? Strategy { get; set; }
    public string? Setup { get; set; }
    public decimal? StopLoss { get; set; }
    public decimal? TakeProfit { get; set; }
}

public class TradeUpdateDto
{
    public decimal? ExitPrice { get; set; }
    public DateTime? ExitDate { get; set; }
    public string? Status { get; set; }
    public string? Outcome { get; set; }
    public decimal? RealizedPnl { get; set; }
    public string? Strategy { get; set; }
    public string? Setup { get; set; }
    public decimal? StopLoss { get; set; }
    public decimal? TakeProfit { get; set; }
}
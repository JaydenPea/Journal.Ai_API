namespace Journal.Application.DTOs;

public class TradeStatsDto
{
    public int TotalTrades { get; set; }
    public int WinningTrades { get; set; }
    public int LosingTrades { get; set; }
    public int BreakEvenTrades { get; set; }
    public decimal TotalPnl { get; set; }
    public decimal WinRate { get; set; }
    public decimal AvgWin { get; set; }
    public decimal AvgLoss { get; set; }
    public decimal ProfitFactor { get; set; }
}

public class MonthlyStatsDto
{
    public string UserId { get; set; } = string.Empty;
    public string? TradingAccountId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int TotalTrades { get; set; }
    public int WinningTrades { get; set; }
    public int LosingTrades { get; set; }
    public decimal TotalPnl { get; set; }
    public decimal TotalVolume { get; set; }
    public decimal AvgWin { get; set; }
    public decimal AvgLoss { get; set; }
    public decimal WinRate { get; set; }
    public decimal LargestWin { get; set; }
    public decimal LargestLoss { get; set; }
}

public class AdvancedMetricsDto
{
    public decimal SharpeRatio { get; set; }
    public decimal MaxDrawdown { get; set; }
    public decimal ProfitFactor { get; set; }
    public decimal Expectancy { get; set; }
    public decimal AvgWin { get; set; }
    public decimal AvgLoss { get; set; }
    public decimal LargestWin { get; set; }
    public decimal LargestLoss { get; set; }
    public decimal WinRate { get; set; }
    public int TotalTrades { get; set; }
}
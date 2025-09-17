using Journal.Application.DTOs;
using Journal.Domain.Entities;

namespace Journal.Application.Mappers;

public static class TradeMapper
{
    public static TradeDto ToDto(Trade trade)
    {
        return new TradeDto
        {
            Id = trade.Id,
            Symbol = trade.Symbol,
            TradeType = trade.TradeType,
            PositionSize = trade.PositionSize,
            EntryPrice = trade.EntryPrice,
            ExitPrice = trade.ExitPrice,
            EntryDate = trade.EntryDate,
            ExitDate = trade.ExitDate,
            Status = trade.Status,
            Outcome = trade.Outcome,
            RealizedPnl = trade.RealizedPnl,
            Strategy = trade.Strategy,
            StrategyId = trade.StrategyId,
            Setup = trade.Setup,
            StopLoss = trade.StopLoss,
            TakeProfit = trade.TakeProfit
        };
    }

    public static Trade ToDomain(TradeCreateDto dto, string userId)
    {
        return new Trade
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            TradingAccountId = dto.TradingAccountId,
            Symbol = dto.Symbol,
            TradeType = dto.TradeType,
            PositionSize = dto.PositionSize,
            EntryPrice = dto.EntryPrice,
            EntryDate = dto.EntryDate,
            Status = "open",
            Strategy = dto.Strategy,
            Setup = dto.Setup,
            StopLoss = dto.StopLoss,
            TakeProfit = dto.TakeProfit,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static void UpdateFromDto(Trade trade, TradeUpdateDto dto)
    {
        if (dto.ExitPrice.HasValue) trade.ExitPrice = dto.ExitPrice;
        if (dto.ExitDate.HasValue) trade.ExitDate = dto.ExitDate;
        if (!string.IsNullOrEmpty(dto.Status)) trade.Status = dto.Status;
        if (!string.IsNullOrEmpty(dto.Outcome)) trade.Outcome = dto.Outcome;
        if (dto.RealizedPnl.HasValue) trade.RealizedPnl = dto.RealizedPnl;
        if (!string.IsNullOrEmpty(dto.Strategy)) trade.Strategy = dto.Strategy;
        if (!string.IsNullOrEmpty(dto.Setup)) trade.Setup = dto.Setup;
        if (dto.StopLoss.HasValue) trade.StopLoss = dto.StopLoss;
        if (dto.TakeProfit.HasValue) trade.TakeProfit = dto.TakeProfit;
        
        trade.UpdatedAt = DateTime.UtcNow;
    }
}
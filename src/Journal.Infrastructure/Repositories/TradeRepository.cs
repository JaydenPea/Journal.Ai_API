using Journal.Domain.Entities;
using Journal.Domain.Repositories;
using Journal.Infrastructure.Models;
using Supabase;

namespace Journal.Infrastructure.Repositories;

public class TradeRepository : ITradeRepository
{
    private readonly Client _supabaseClient;

    public TradeRepository(Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<(List<Trade> trades, int totalCount)> GetTradesAsync(string userId, string? accountId, string? status, string? symbol, DateTime? dateFrom, DateTime? dateTo, int page, int pageSize)
    {
        var query = _supabaseClient
            .From<TradeEntity>()
            .Select("*")
            .Where(x => x.UserId == userId);

        if (!string.IsNullOrEmpty(accountId))
            query = query.Where(x => x.TradingAccountId == accountId);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(x => x.Status == status);

        if (!string.IsNullOrEmpty(symbol))
            query = query.Where(x => x.Symbol == symbol);

        if (dateFrom.HasValue)
            query = query.Where(x => x.EntryDate >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(x => x.EntryDate <= dateTo.Value);

        // Get total count first
        var countResult = await query.Get();
        var totalCount = countResult.Models.Count;

        // Apply pagination
        var offset = (page - 1) * pageSize;
        var result = await query
            .Order(x => x.EntryDate, Supabase.Postgrest.Constants.Ordering.Descending)
            .Range(offset, offset + pageSize - 1)
            .Get();

        var trades = result.Models.Select(MapToDomain).ToList();
        return (trades, totalCount);
    }

    public async Task<Trade?> GetByIdAsync(string userId, string tradeId)
    {
        var result = await _supabaseClient
            .From<TradeEntity>()
            .Select("*")
            .Where(x => x.Id == tradeId && x.UserId == userId)
            .Single();

        return result == null ? null : MapToDomain(result);
    }

    public async Task<Trade> CreateAsync(Trade trade)
    {
        var entity = MapToEntity(trade);
        var result = await _supabaseClient
            .From<TradeEntity>()
            .Insert(entity);

        return MapToDomain(result.Model!);
    }

    public async Task<Trade?> UpdateAsync(Trade trade)
    {
        var entity = MapToEntity(trade);
        var result = await _supabaseClient
            .From<TradeEntity>()
            .Update(entity);

        return result.Model == null ? null : MapToDomain(result.Model);
    }

    public async Task<bool> DeleteAsync(string userId, string tradeId)
    {
        try
        {
            await _supabaseClient
                .From<TradeEntity>()
                .Where(x => x.Id == tradeId && x.UserId == userId)
                .Delete();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ExistsAsync(string userId, string tradeId)
    {
        try
        {
            var result = await _supabaseClient
                .From<TradeEntity>()
                .Select("id")
                .Where(x => x.Id == tradeId && x.UserId == userId)
                .Single();
            return result != null;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Trade>> GetClosedTradesAsync(string userId, string? accountId, DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _supabaseClient
            .From<TradeEntity>()
            .Select("*")
            .Where(x => x.UserId == userId && x.Status == "closed");

        if (!string.IsNullOrEmpty(accountId))
            query = query.Where(x => x.TradingAccountId == accountId);

        if (dateFrom.HasValue)
            query = query.Where(x => x.ExitDate >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(x => x.ExitDate <= dateTo.Value);

        var result = await query.Order(x => x.ExitDate, Supabase.Postgrest.Constants.Ordering.Ascending).Get();
        return result.Models.Select(MapToDomain).ToList();
    }

    private static Trade MapToDomain(TradeEntity entity)
    {
        return new Trade
        {
            Id = entity.Id,
            UserId = entity.UserId,
            TradingAccountId = entity.TradingAccountId,
            Symbol = entity.Symbol,
            TradeType = entity.TradeType,
            PositionSize = entity.PositionSize,
            EntryPrice = entity.EntryPrice,
            ExitPrice = entity.ExitPrice,
            EntryDate = entity.EntryDate,
            ExitDate = entity.ExitDate,
            Status = entity.Status,
            Outcome = entity.Outcome,
            RealizedPnl = entity.RealizedPnl,
            Strategy = entity.Strategy,
            StrategyId = entity.StrategyId,
            Setup = entity.Setup,
            StopLoss = entity.StopLoss,
            TakeProfit = entity.TakeProfit,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    private static TradeEntity MapToEntity(Trade domain)
    {
        return new TradeEntity
        {
            Id = domain.Id,
            UserId = domain.UserId,
            TradingAccountId = domain.TradingAccountId,
            Symbol = domain.Symbol,
            TradeType = domain.TradeType,
            PositionSize = domain.PositionSize,
            EntryPrice = domain.EntryPrice,
            ExitPrice = domain.ExitPrice,
            EntryDate = domain.EntryDate,
            ExitDate = domain.ExitDate,
            Status = domain.Status,
            Outcome = domain.Outcome,
            RealizedPnl = domain.RealizedPnl,
            Strategy = domain.Strategy,
            StrategyId = domain.StrategyId,
            Setup = domain.Setup,
            StopLoss = domain.StopLoss,
            TakeProfit = domain.TakeProfit,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt
        };
    }
}
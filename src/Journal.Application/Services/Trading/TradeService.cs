using Journal.Application.DTOs;
using Journal.Application.Interfaces.Trading;
using Journal.Application.Mappers;
using Journal.Domain.Repositories;

namespace Journal.Application.Services.Trading;

public class TradeService : ITradeService
{
    private readonly ITradeRepository _tradeRepository;

    public TradeService(ITradeRepository tradeRepository)
    {
        _tradeRepository = tradeRepository;
    }

    public async Task<PaginatedResponse<TradeDto>> GetTradesAsync(string userId, TradeFilterRequest request)
    {
        var (trades, totalCount) = await _tradeRepository.GetTradesAsync(
            userId, 
            request.AccountId, 
            request.Status, 
            request.Symbol, 
            request.DateFrom, 
            request.DateTo, 
            request.Page, 
            request.PageSize);

        var tradeDtos = trades.Select(TradeMapper.ToDto).ToList();
        var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

        return new PaginatedResponse<TradeDto>
        {
            Data = tradeDtos,
            Pagination = new PaginationInfo
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = totalCount,
                TotalPages = totalPages,
                HasNextPage = request.Page < totalPages,
                HasPrevPage = request.Page > 1
            }
        };
    }

    public async Task<TradeDto?> GetTradeByIdAsync(string userId, string tradeId)
    {
        var trade = await _tradeRepository.GetByIdAsync(userId, tradeId);
        return trade == null ? null : TradeMapper.ToDto(trade);
    }

    public async Task<TradeDto> CreateTradeAsync(string userId, TradeCreateDto tradeDto)
    {
        var trade = TradeMapper.ToDomain(tradeDto, userId);
        var createdTrade = await _tradeRepository.CreateAsync(trade);
        return TradeMapper.ToDto(createdTrade);
    }

    public async Task<TradeDto?> UpdateTradeAsync(string userId, string tradeId, TradeUpdateDto tradeDto)
    {
        var existingTrade = await _tradeRepository.GetByIdAsync(userId, tradeId);
        if (existingTrade == null)
            return null;

        TradeMapper.UpdateFromDto(existingTrade, tradeDto);
        var updatedTrade = await _tradeRepository.UpdateAsync(existingTrade);
        return updatedTrade == null ? null : TradeMapper.ToDto(updatedTrade);
    }

    public async Task<bool> DeleteTradeAsync(string userId, string tradeId)
    {
        return await _tradeRepository.DeleteAsync(userId, tradeId);
    }

    public async Task<TradeStatsDto> GetTradeStatsAsync(string userId, TradeStatsRequest request)
    {
        var trades = await _tradeRepository.GetClosedTradesAsync(userId, request.AccountId, request.DateFrom, request.DateTo);
        
        var totalTrades = trades.Count;
        var winningTrades = trades.Count(t => t.Outcome == "win");
        var losingTrades = trades.Count(t => t.Outcome == "loss");
        var breakEvenTrades = trades.Count(t => t.Outcome == "breakeven");
        
        var totalPnl = trades.Sum(t => t.RealizedPnl ?? 0);
        var winRate = totalTrades > 0 ? (decimal)winningTrades / totalTrades * 100 : 0;
        
        var winners = trades.Where(t => t.Outcome == "win").ToList();
        var losers = trades.Where(t => t.Outcome == "loss").ToList();
        
        var avgWin = winners.Count > 0 ? winners.Average(t => t.RealizedPnl ?? 0) : 0;
        var avgLoss = losers.Count > 0 ? Math.Abs(losers.Average(t => t.RealizedPnl ?? 0)) : 0;
        
        var grossProfit = winners.Sum(t => t.RealizedPnl ?? 0);
        var grossLoss = Math.Abs(losers.Sum(t => t.RealizedPnl ?? 0));
        var profitFactor = grossLoss > 0 ? grossProfit / grossLoss : (grossProfit > 0 ? decimal.MaxValue : 0);

        return new TradeStatsDto
        {
            TotalTrades = totalTrades,
            WinningTrades = winningTrades,
            LosingTrades = losingTrades,
            BreakEvenTrades = breakEvenTrades,
            TotalPnl = Math.Round(totalPnl, 2),
            WinRate = Math.Round(winRate, 1),
            AvgWin = Math.Round(avgWin, 2),
            AvgLoss = Math.Round(avgLoss, 2),
            ProfitFactor = Math.Round(profitFactor, 2)
        };
    }

    public async Task<List<MonthlyStatsDto>> GetMonthlyStatsAsync(string userId, string? accountId, int? yearFrom, int? yearTo)
    {
        // For now, return empty list - this would typically query a materialized view
        // or calculate from trades grouped by month
        return new List<MonthlyStatsDto>();
    }
}
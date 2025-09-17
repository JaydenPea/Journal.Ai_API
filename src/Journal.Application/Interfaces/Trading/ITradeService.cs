using Journal.Application.DTOs;

namespace Journal.Application.Interfaces.Trading;

public interface ITradeService
{
    Task<PaginatedResponse<TradeDto>> GetTradesAsync(string userId, TradeFilterRequest request);
    Task<TradeDto?> GetTradeByIdAsync(string userId, string tradeId);
    Task<TradeDto> CreateTradeAsync(string userId, TradeCreateDto tradeDto);
    Task<TradeDto?> UpdateTradeAsync(string userId, string tradeId, TradeUpdateDto tradeDto);
    Task<bool> DeleteTradeAsync(string userId, string tradeId);
    Task<TradeStatsDto> GetTradeStatsAsync(string userId, TradeStatsRequest request);
    Task<List<MonthlyStatsDto>> GetMonthlyStatsAsync(string userId, string? accountId, int? yearFrom, int? yearTo);
}
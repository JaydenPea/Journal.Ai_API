using Journal.Domain.Entities;

namespace Journal.Domain.Repositories;

public interface ITradeRepository
{
    Task<(List<Trade> trades, int totalCount)> GetTradesAsync(string userId, string? accountId, string? status, string? symbol, DateTime? dateFrom, DateTime? dateTo, int page, int pageSize);
    Task<Trade?> GetByIdAsync(string userId, string tradeId);
    Task<Trade> CreateAsync(Trade trade);
    Task<Trade?> UpdateAsync(Trade trade);
    Task<bool> DeleteAsync(string userId, string tradeId);
    Task<bool> ExistsAsync(string userId, string tradeId);
    Task<List<Trade>> GetClosedTradesAsync(string userId, string? accountId, DateTime? dateFrom, DateTime? dateTo);
}
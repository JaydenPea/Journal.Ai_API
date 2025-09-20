using Journal.Application.Consts;
using Journal.Application.DTOs;
using Journal.Application.Interfaces.Trading;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Controllers;

/// <summary>
/// Trading operations for managing trades, analytics, and statistics
/// </summary>
[ApiController]
[Produces("application/json")]
public class TradesController : ControllerBase
{
    private readonly ITradeService _tradeService;

    public TradesController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    /// <summary>
    /// Get all trades for a user with optional filtering and pagination
    /// </summary>
    /// <param name="userId">The user ID to get trades for</param>
    /// <param name="request">Filter and pagination parameters</param>
    /// <returns>Paginated list of trades</returns>
    [HttpGet(ApiRoutes.Trades.GetAll)]
    [ProducesResponseType(typeof(PaginatedResponse<TradeDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetTrades(string userId, [FromQuery] TradeFilterRequest request)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("User ID is required");
        }

        var result = await _tradeService.GetTradesAsync(userId, request);
        return Ok(result);
    }

    [HttpGet(ApiRoutes.Trades.GetById)]
    public async Task<IActionResult> GetTrade(string userId, string id)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("User ID is required");
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("Trade ID is required");
        }

        var trade = await _tradeService.GetTradeByIdAsync(userId, id);
        
        if (trade == null)
        {
            return NotFound($"Trade with ID '{id}' not found");
        }

        return Ok(trade);
    }

    /// <summary>
    /// Create a new trade
    /// </summary>
    /// <param name="userId">The user ID creating the trade</param>
    /// <param name="tradeDto">Trade creation data</param>
    /// <returns>The created trade</returns>
    [HttpPost(ApiRoutes.Trades.Create)]
    [ProducesResponseType(typeof(TradeDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateTrade(string userId, [FromBody] TradeCreateDto tradeDto)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("User ID is required");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdTrade = await _tradeService.CreateTradeAsync(userId, tradeDto);
        return CreatedAtAction(nameof(GetTrade), new { userId, id = createdTrade.Id }, createdTrade);
    }

    [HttpPut(ApiRoutes.Trades.Update)]
    public async Task<IActionResult> UpdateTrade(string userId, string id, [FromBody] TradeUpdateDto tradeDto)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("User ID is required");
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("Trade ID is required");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedTrade = await _tradeService.UpdateTradeAsync(userId, id, tradeDto);

        if (updatedTrade == null)
        {
            return NotFound($"Trade with ID '{id}' not found");
        }

        return Ok(updatedTrade);
    }

    [HttpDelete(ApiRoutes.Trades.Delete)]
    public async Task<IActionResult> DeleteTrade(string userId, string id)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("User ID is required");
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("Trade ID is required");
        }

        var success = await _tradeService.DeleteTradeAsync(userId, id);

        if (!success)
        {
            return NotFound($"Trade with ID '{id}' not found");
        }

        return NoContent();
    }

    /// <summary>
    /// Get trading statistics and analytics for a user
    /// </summary>
    /// <param name="userId">The user ID to get stats for</param>
    /// <param name="request">Date range and account filters</param>
    /// <returns>Trading statistics including win rate, profit factor, etc.</returns>
    [HttpGet(ApiRoutes.Trades.GetStats)]
    [ProducesResponseType(typeof(TradeStatsDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetTradeStats(string userId, [FromQuery] TradeStatsRequest request)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("User ID is required");
        }

        var stats = await _tradeService.GetTradeStatsAsync(userId, request);
        return Ok(stats);
    }
}
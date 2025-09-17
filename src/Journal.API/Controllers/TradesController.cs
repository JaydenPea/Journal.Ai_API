using Journal.Application.Consts;
using Journal.Application.DTOs;
using Journal.Application.Interfaces.Trading;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Controllers;

[ApiController]
public class TradesController : ControllerBase
{
    private readonly ITradeService _tradeService;

    public TradesController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [HttpGet(ApiRoutes.Trades.GetAll)]
    public async Task<IActionResult> GetTrades([FromQuery] TradeFilterRequest request)
    {
        // TODO: Get userId from JWT claims instead of hardcoding
        var userId = "temp-user-id";
        var result = await _tradeService.GetTradesAsync(userId, request);
        return Ok(result);
    }

    [HttpGet(ApiRoutes.Trades.GetById)]
    public async Task<IActionResult> GetTrade(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("Trade ID is required");
        }

        // TODO: Get userId from JWT claims instead of hardcoding
        var userId = "temp-user-id";
        var trade = await _tradeService.GetTradeByIdAsync(userId, id);
        
        if (trade == null)
        {
            return NotFound($"Trade with ID '{id}' not found");
        }

        return Ok(trade);
    }

    [HttpPost(ApiRoutes.Trades.Create)]
    public async Task<IActionResult> CreateTrade([FromBody] TradeCreateDto tradeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // TODO: Get userId from JWT claims instead of hardcoding
        var userId = "temp-user-id";
        var createdTrade = await _tradeService.CreateTradeAsync(userId, tradeDto);
        return CreatedAtAction(nameof(GetTrade), new { id = createdTrade.Id }, createdTrade);
    }

    [HttpPut(ApiRoutes.Trades.Update)]
    public async Task<IActionResult> UpdateTrade(string id, [FromBody] TradeUpdateDto tradeDto)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("Trade ID is required");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // TODO: Get userId from JWT claims instead of hardcoding
        var userId = "temp-user-id";
        var updatedTrade = await _tradeService.UpdateTradeAsync(userId, id, tradeDto);

        if (updatedTrade == null)
        {
            return NotFound($"Trade with ID '{id}' not found");
        }

        return Ok(updatedTrade);
    }

    [HttpDelete(ApiRoutes.Trades.Delete)]
    public async Task<IActionResult> DeleteTrade(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("Trade ID is required");
        }

        // TODO: Get userId from JWT claims instead of hardcoding
        var userId = "temp-user-id";
        var success = await _tradeService.DeleteTradeAsync(userId, id);

        if (!success)
        {
            return NotFound($"Trade with ID '{id}' not found");
        }

        return NoContent();
    }

    [HttpGet(ApiRoutes.Trades.GetStats)]
    public async Task<IActionResult> GetTradeStats([FromQuery] TradeStatsRequest request)
    {
        // TODO: Get userId from JWT claims instead of hardcoding
        var userId = "temp-user-id";
        var stats = await _tradeService.GetTradeStatsAsync(userId, request);
        return Ok(stats);
    }
}
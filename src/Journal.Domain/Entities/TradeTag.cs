namespace Journal.Domain.Entities;

public class TradeTag
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class TradeTagAssignment
{
    public string Id { get; set; } = string.Empty;
    public string TradeId { get; set; } = string.Empty;
    public string TagId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
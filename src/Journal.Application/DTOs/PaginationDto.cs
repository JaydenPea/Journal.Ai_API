namespace Journal.Application.DTOs;

public class PaginatedResponse<T>
{
    public List<T> Data { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}

public class PaginationInfo
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPrevPage { get; set; }
}

public class TradeFilterRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string? AccountId { get; set; }
    public string? Status { get; set; }
    public string? Symbol { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}

public class TradeStatsRequest
{
    public string? AccountId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}
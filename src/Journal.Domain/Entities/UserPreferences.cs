namespace Journal.Domain.Entities;

public class UserPreferences
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Theme { get; set; } = "dark";
    public bool EmailNotifications { get; set; } = true;
    public bool TradeReminders { get; set; } = true;
    public bool WeeklyReports { get; set; } = true;
    public bool MarketingEmails { get; set; } = false;
    public string DefaultDateRange { get; set; } = "30d";
    public string PreferredChartType { get; set; } = "bar";
    public bool MarketingConsent { get; set; } = false;
    public bool AnalyticsConsent { get; set; } = false;
    public bool DataProcessingConsent { get; set; } = false;
    public bool DataProcessingAnalytics { get; set; } = false;
    public bool DataSharingInsights { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
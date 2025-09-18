namespace Journal.Application.Consts;

public static class ApiRoutes
{
    public const string ApiBase = "api";
    
    public static class Account
    {
        public const string Base = $"{ApiBase}/{{userId}}/account";
        public const string GetProfile = $"{Base}/profile";
        public const string UpdateProfile = $"{Base}/profile";
        public const string DeleteProfile = $"{Base}/profile";
    }
    
    public static class Trades
    {
        public const string Base = $"{ApiBase}/{{userId}}/trades";
        public const string GetAll = Base;
        public const string GetById = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string GetStats = $"{Base}/stats";
        public const string GetMonthlyStats = $"{Base}/monthly-stats";
    }
    
    public static class TradingAccounts
    {
        public const string Base = $"{ApiBase}/{{userId}}/accounts";
        public const string GetAll = Base;
        public const string GetById = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string GetStats = $"{Base}/{{id}}/stats";
        public const string GetPerformance = $"{Base}/{{id}}/performance";
    }
    
    public static class Analytics
    {
        public const string Base = $"{ApiBase}/{{userId}}/analytics";
        public const string AdvancedMetrics = $"{Base}/advanced-metrics";
        public const string RiskAnalysis = $"{Base}/risk-analysis";
    }
    
    public static class User
    {
        public const string Base = $"{ApiBase}/user";
        public const string Dashboard = $"{Base}/dashboard";
        public const string Preferences = $"{Base}/preferences";
    }
}
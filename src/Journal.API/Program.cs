using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Journal Trading API",
        Version = "v1",
        Description = "A comprehensive trading journal API for managing trades, accounts, and analytics",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Trading Journal API",
            Email = "support@tradingjournal.com"
        }
    });

    // Enable XML comments for better documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configure Supabase
var url = builder.Configuration["SUPABASE_URL"] ?? Environment.GetEnvironmentVariable("SUPABASE_URL");
var key = builder.Configuration["SUPABASE_KEY"] ?? Environment.GetEnvironmentVariable("SUPABASE_KEY");

// Only initialize Supabase if credentials are available
if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(key))
{
    var options = new SupabaseOptions
    {
        AutoConnectRealtime = true
    };

    builder.Services.AddSingleton<Client>(provider =>
    {
        var client = new Client(url, key, options);
        client.InitializeAsync().GetAwaiter().GetResult();
        return client;
    });
}
else
{
    // Add a dummy client for development/testing
    builder.Services.AddSingleton<Client>(provider => null!);
}

// Register repositories
builder.Services.AddScoped<Journal.Domain.Repositories.IUserRepository, Journal.Infrastructure.Repositories.UserRepository>();
builder.Services.AddScoped<Journal.Domain.Repositories.ITradeRepository, Journal.Infrastructure.Repositories.TradeRepository>();

// Register application services
builder.Services.AddScoped<Journal.Application.Interfaces.Account.IAccountService, Journal.Application.Services.AccountService>();
builder.Services.AddScoped<Journal.Application.Interfaces.Trading.ITradeService, Journal.Application.Services.Trading.TradeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger in all environments for Coolify deployment
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Journal Trading API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Journal Trading API Documentation";

    // Enable search and expand all operations by default
    c.EnableFilter();
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
});

// Remove HTTPS redirection for Coolify (proxy handles SSL)
// app.UseHttpsRedirection();

app.UseAuthorization();

// Add health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
    .WithName("HealthCheck")
    .WithOpenApi();

app.MapControllers();

app.Run();

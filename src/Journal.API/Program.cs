using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure Supabase
var url = builder.Configuration["SUPABASE_URL"];
var key = builder.Configuration["SUPABASE_KEY"];

if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("SUPABASE_URL and SUPABASE_KEY must be configured in appsettings.json or environment variables.");
}

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

// Register repositories
builder.Services.AddScoped<Journal.Domain.Repositories.IUserRepository, Journal.Infrastructure.Repositories.UserRepository>();
builder.Services.AddScoped<Journal.Domain.Repositories.ITradeRepository, Journal.Infrastructure.Repositories.TradeRepository>();

// Register application services
builder.Services.AddScoped<Journal.Application.Interfaces.Account.IAccountService, Journal.Application.Services.AccountService>();
builder.Services.AddScoped<Journal.Application.Interfaces.Trading.ITradeService, Journal.Application.Services.Trading.TradeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

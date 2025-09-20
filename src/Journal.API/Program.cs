var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline - enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI();

// Add health check endpoint
app.MapGet("/health", () => Results.Ok(new { 
    status = "healthy", 
    timestamp = DateTime.UtcNow,
    environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"
}))
.WithName("HealthCheck")
.WithOpenApi();

// Add simple root endpoint
app.MapGet("/", () => "Journal API is running! 🚀")
.WithName("Root")
.WithOpenApi();

// Map controllers (only WeatherForecast controller now)
app.MapControllers();

app.Run();
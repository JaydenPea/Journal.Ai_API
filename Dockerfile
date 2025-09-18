# Multi-stage Dockerfile for .NET 9 ASP.NET Core API
# Optimized for production deployment on Coolify

# ===========================
# Build Stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file first for dependency resolution
COPY *.sln ./

# Copy project files for dependency graph
COPY src/Journal.API/Journal.api.csproj src/Journal.API/
COPY src/Journal.Application/Journal.Application.csproj src/Journal.Application/
COPY src/Journal.Domain/Journal.Domain.csproj src/Journal.Domain/
COPY src/Journal.Infrastructure/Journal.Infrastructure.csproj src/Journal.Infrastructure/

# Restore dependencies as a distinct layer for better caching
RUN dotnet restore "src/Journal.API/Journal.api.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR /src/src/Journal.API
RUN dotnet build "Journal.api.csproj" -c Release -o /app/build --no-restore

# ===========================
# Publish Stage
# ===========================
FROM build AS publish
RUN dotnet publish "Journal.api.csproj" -c Release -o /app/publish \
    --no-restore \
    --no-build \
    -p:PublishTrimmed=false \
    -p:PublishReadyToRun=true

# ===========================
# Runtime Stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Create non-root user for security
RUN groupadd -r appuser && useradd -r -g appuser appuser

# Set working directory
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Set ownership to non-root user
RUN chown -R appuser:appuser /app
USER appuser

# Configure environment for production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_EnableDiagnostics=0

# Expose port
EXPOSE 8080

# Add health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Define entrypoint
ENTRYPOINT ["dotnet", "Journal.api.dll"]
# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Journal API project built with ASP.NET Core 9.0 following Clean Architecture principles. The solution is currently in early development with a basic structure containing four main layers.

**Solution Structure:**
- **Journal.API** (Presentation Layer): Web API controllers, HTTP configuration, and API endpoints
- **Journal.Application** (Application Layer): Business logic, use cases, and application services
- **Journal.Domain** (Domain Layer): Core business entities, domain logic, and business rules  
- **Journal.Infrastructure** (Infrastructure Layer): Database implementations and external service integrations

**Current State:** The project contains a basic ASP.NET Core Web API template with a single WeatherForecast controller for demonstration purposes.

## Development Commands

### Building and Running
```bash
# Build the entire solution
dotnet build Journal.AI.sln

# Build specific project
dotnet build src/Journal.API/Journal.api.csproj

# Run the API locally
dotnet run --project src/Journal.API/Journal.api.csproj

# Run with specific profile
dotnet run --project src/Journal.API/Journal.api.csproj --launch-profile https
dotnet run --project src/Journal.API/Journal.api.csproj --launch-profile http
```

### Docker Development
```bash
# Build Docker image (from src/Journal.API directory)
docker build -t journal-api .

# Run in container
docker run -p 8080:8080 -p 8081:8081 journal-api
```

### Testing API Endpoints
- Use the `src/Journal.API/Journal.API.http` file with REST Client extensions
- Default local endpoints:
  - HTTP: http://localhost:5274
  - HTTPS: https://localhost:7207

## Architecture Guidelines

### Clean Architecture Principles
- **Dependency Flow**: Dependencies should flow inward toward the Domain layer
- **Domain Layer**: Contains core business entities and should be framework-agnostic
- **Application Layer**: Orchestrates domain logic and defines use cases
- **Infrastructure Layer**: Implements external concerns (database, APIs, file system)
- **Presentation Layer**: Handles HTTP concerns and user interface

### Project Dependencies
Current dependency structure:
- `Journal.API` → `Journal.Application` + `Journal.Infrastructure`
- `Journal.Application` → `Journal.Domain`
- `Journal.Infrastructure` → `Journal.Domain` + `Journal.Application`
- `Journal.Domain` → No dependencies (pure domain layer)

### Configuration
- Environment-specific settings in `src/Journal.API/appsettings.{Environment}.json`
- User secrets configured (ID: 0aef9af3-12ed-4223-92e3-9255de870156)
- Docker deployment targets Linux containers
- Supports both HTTP and HTTPS with automatic redirection

## Technical Stack

### Framework and Runtime
- .NET 9.0 with nullable reference types enabled
- ASP.NET Core Web API with minimal API configuration
- OpenAPI/Swagger integration for API documentation

### Development Environment
- Development URLs: HTTP (5274), HTTPS (7207)
- Container ports: HTTP (8080), HTTPS (8081)
- Docker multi-stage builds for optimized production images
- Visual Studio launch profiles for different environments

### Current Features
- Basic Web API template with WeatherForecast controller
- OpenAPI documentation (available in Development environment)
- HTTPS redirection and authorization middleware configured
- Docker support with production-ready Dockerfile

## Development Patterns

### Controller Structure
- Use `[ApiController]` attribute for automatic model validation
- Follow RESTful routing conventions with `[Route("[controller]")]`
- Inject dependencies through constructor injection
- Return appropriate HTTP status codes and response types

### Error Handling
- Leverage ASP.NET Core's built-in model validation
- Use ILogger for structured logging throughout the application
- Return consistent error responses following API standards

### Testing
- No testing framework currently configured
- When adding tests, follow the project structure with separate test projects
- Consider integration tests for API endpoints and unit tests for business logic
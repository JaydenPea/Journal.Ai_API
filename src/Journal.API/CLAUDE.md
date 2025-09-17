# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Journal API project built with ASP.NET Core 9.0 following Clean Architecture principles. The solution is organized into four main layers with proper dependency flow:

**Solution Structure:**
- **1. Presentation Layer**
  - `Journal.API` (Journal.Presentation/Journal.API/): Web API controllers, HTTP configuration, and API endpoints
- **2. Application Layer** 
  - `Journal.Application` (Journal.Application/Journal_Application/): Business logic, use cases, and application services
- **3. Domain Layer**
  - `Journal.Domain` (Journal.Domain/Journal_Domain/): Core business entities, domain logic, and business rules
- **4. Infrastructure Layer**
  - `Journal.Persistence.SQL` (Journal.DataLayer/Journal_Persistence.SQL/): SQL database implementation and data access
  - `Journal.MigrationsHandler` (Journal.DataLayer/Journal_MigrationsHandler/): Database migration management

**Solution File:** `Journal.AI.sln` contains all projects organized in solution folders matching the Clean Architecture layers.

## Development Commands

### Building and Running
```bash
# Build the entire solution
dotnet build Journal.AI.sln

# Build specific project
dotnet build Journal.api.csproj

# Run the API locally
dotnet run --project Journal.api.csproj

# Run with specific profile
dotnet run --project Journal.api.csproj --launch-profile https
dotnet run --project Journal.api.csproj --launch-profile http
```

### Docker Development
```bash
# Build Docker image
docker build -t journal-api .

# Run in container
docker run -p 8080:8080 -p 8081:8081 journal-api
```

### Testing API Endpoints
- Use the `Journal.API.http` file with REST Client extensions
- Default local endpoints:
  - HTTP: http://localhost:5274
  - HTTPS: https://localhost:7207

## Architecture Guidelines

### Project Structure
- Follow Clean Architecture layering - dependencies should flow inward toward the Domain layer
- Controllers in the API layer should be thin and delegate to Application layer services
- Domain entities should contain business logic and be framework-agnostic
- Infrastructure concerns (database, external APIs) belong in the DataLayer

### Configuration
- Environment-specific settings in `appsettings.{Environment}.json`
- User secrets are configured (ID: 0aef9af3-12ed-4223-92e3-9255de870156)
- Docker deployment targets Linux containers

### Dependencies
- Uses .NET 9.0 with nullable reference types enabled
- OpenAPI/Swagger integration for API documentation
- ASP.NET Core controllers with attribute routing
- Docker support with multi-stage builds

## Development Environment
- Target Framework: .NET 9.0
- Development URLs: HTTP (5274), HTTPS (7207)
- Container ports: HTTP (8080), HTTPS (8081)
- Supports both local development and containerized deployment
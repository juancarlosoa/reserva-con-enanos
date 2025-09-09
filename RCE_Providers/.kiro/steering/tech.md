# Technology Stack

## Framework & Runtime
- .NET 8.0 Web API
- C# with nullable reference types enabled
- Implicit usings enabled

## Database & ORM
- PostgreSQL database
- Entity Framework Core 9.0.4 with Npgsql provider
- Code-first migrations
- Automatic database migration on startup

## Key Libraries
- **AutoMapper 14.0.0** - Object-to-object mapping
- **FluentValidation.AspNetCore 11.3.0** - Input validation
- **DotNetEnv 3.1.1** - Environment variable management
- **Swashbuckle.AspNetCore 8.1.4** - API documentation
- **NSwag.AspNetCore 14.4.0** - OpenAPI/Swagger tooling

## Configuration
- Environment variables loaded via .env files
- Connection strings use environment variable substitution
- Development-specific settings in appsettings.Development.json

## Common Commands
```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Create new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Restore packages
dotnet restore
```

## Development Tools
- Swagger UI available in development environment
- HTTP request testing via RCE_Providers.http file
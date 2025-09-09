# Project Structure & Architecture

## Overall Architecture
The project follows a **feature-based vertical slice architecture** with clean separation of concerns using a layered approach within each feature.

## Folder Structure

### Core Infrastructure
- **CoreData/** - Database context and shared data access
- **Common/** - Shared utilities (e.g., Slug generation)
- **Migrations/** - Entity Framework database migrations

### Feature Modules
Each business domain follows the same structure pattern:
- **[Feature]/Controllers/** - API controllers
- **[Feature]/DTOs/** - Data Transfer Objects (Request/Response)
- **[Feature]/Entities/** - Domain models/entities
- **[Feature]/Mappings/** - AutoMapper profiles
- **[Feature]/Repositories/** - Data access layer with interfaces
- **[Feature]/Services/** - Business logic layer with interfaces

### Current Features
- **EscapeRoomProviders/** - Provider management
- **Rooms/** - Room management

## Naming Conventions

### Files & Classes
- Controllers: `[Entity]Controller.cs`
- DTOs: `[Entity]RequestDTO.cs`, `[Entity]ResponseDTO.cs`
- Entities: `[Entity].cs`
- Repositories: `I[Entity]Repository.cs`, `[Entity]Repository.cs`
- Services: `I[Entity]Service.cs`, `[Entity]Service.cs`
- Mappings: `[Entity]Profile.cs`

### Database
- Entities use `Guid` primary keys
- Slugs are auto-generated and unique
- UTC timestamps for `CreatedAt` fields
- Cascade deletion for parent-child relationships

## Dependency Injection Pattern
- Repository pattern with interfaces
- Service layer with interfaces
- Scoped lifetime for services and repositories
- AutoMapper profiles registered automatically
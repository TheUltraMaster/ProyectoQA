# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 9 Blazor Server application with a modular architecture for equipment management (proyecto de gestión de equipos). The solution consists of multiple projects organized as separate class libraries with distinct responsibilities:

- **Proyecto/**: Main Blazor Server application using MudBlazor UI components and cookie-based authentication
- **BD/**: Entity Framework Core data layer with MySQL database connectivity  
- **Bucket/**: MinIO object storage service for file/image management
- **Bycript/**: BCrypt password hashing service wrapper

## Architecture

The application follows a layered architecture with separate service projects:

### Database Layer
- Uses Entity Framework Core with MySQL provider (`MySql.EntityFrameworkCore`)
- Context: `BD.Data.ProyectoContext` 
- Models represent equipment management entities: `Area`, `Empleado`, `Equipo`, `Reporte`, `BitacoraEquipo`, `Causa`, `Electronico`, `Herramientum`, `Imagen`, `Marca`, `Mobiliario`, `Usuario`, `Vehiculo`
- **WARNING**: Connection string is hardcoded in both `ProyectoContext.cs:47` and `Program.cs:27` - should be moved to configuration

### Services  
- **MinIO Service**: Handles image/file uploads with bucket management via `IMinioService` (note: interface name has typo)
- **BCrypt Service**: Password hashing and verification via `IBCryptService`
- **Custom Services**: `IUsuarioService`, `IEmpleadoService`, `IAreaService` for business logic

### Frontend
- Blazor Server with MudBlazor UI framework  
- Interactive server-side rendering
- Cookie-based authentication with role-based authorization (Admin/User policies)
- Custom API endpoints for authentication (`/api/auth/login`, `/api/auth/logout`)
- DTOs and AutoMapper pattern for data transfer

## Common Development Commands

### Building and Running
```bash
# Build entire solution
dotnet build Proyecto/Proyecto.sln

# Run main application
cd Proyecto
dotnet run

# Restore packages for all projects
dotnet restore Proyecto/Proyecto.sln
```

### Database Operations
```bash
# Entity Framework commands (run from BD project directory)
cd BD
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### Project Structure Navigation
- Main entry point: `Proyecto/Program.cs`
- Blazor components: `Proyecto/Components/Pages/`
- Layouts: `Proyecto/Components/Layout/`
- Data models: `BD/Models/`
- Business services: `Proyecto/Services/`
- DTOs: `Proyecto/DTOS/`
- AutoMapper configuration: `Proyecto/Mapper/Map.cs`
- External services: Individual project folders (`Bucket/`, `Bycript/`)

## Key Configuration Notes

- All projects target .NET 9.0
- MySQL database connection hardcoded in both `ProyectoContext.cs:47` and `Program.cs:27` (needs refactoring)
- Database name: `proyecto_QA`
- Cookie-based authentication with 7-day expiration
- Role-based authorization: "AdminOnly" and "UserOnly" policies
- MudBlazor UI components with custom CSS styling
- Uses Riok.Mapperly for object mapping

## Development Environment

- Requires .NET 9 SDK
- MySQL database server (configured for localhost with `proyecto_QA` database)
- MinIO server for file storage functionality (optional - Bucket service handles image uploads)
- The `Minio/data/` directory suggests local MinIO development setup

## Important Notes

- **Security**: Database credentials are hardcoded and should be moved to configuration
- **Naming**: Some interface/class names contain typos (`IMinioSerivice`, `Herramientum`)
- **Authentication**: Uses cookie-based auth with role-based policies (Admin/User)
- **Architecture**: Clean separation with DTOs, services, and data layer
- **Database**: Entity Framework with MySQL, includes audit fields (`FechaCommit`) on key entities
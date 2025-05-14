# Store Management API

A .NET Core Web API for managing store data, including customers, products, and purchases.

## Technologies Used

- ASP.NET Core 9
- Entity Framework Core 9
- MediatR (CQRS implementation)
- FluentValidation
- SQL Server

## Project Structure

The solution follows Clean Architecture principles:

- **Domain Layer**: Core business entities and domain logic
- **Application Layer**: Use cases, business logic, and CQRS handlers
- **Infrastructure Layer**: External concerns (database, migrations, seeding)
- **API Layer**: Web API endpoints and middleware
- **Integration Tests**: End-to-end testing of the application

## Features

### API Versioning
- Default version: 1.0
- URL-based versioning
- Version reporting in responses

### Error Handling
- Global exception handling middleware
- Structured error responses
- Support for:
  - Validation exceptions
  - Not found exceptions
  - Invalid operation exceptions
  - Generic error handling

### Request Validation
- FluentValidation for query/command validation
- Validation behavior in MediatR pipeline
- Specific validation rules for each query

## API Endpoints

### Customers (v1)

1. **Get Birthday Customers**
   - Endpoint: `GET /api/v1/customers/birthday`
   - Query parameters:
     - `startDate`: Start of date range (YYYY-MM-DD)
     - `endDate`: End of date range (YYYY-MM-DD)
   - Returns customers whose birthday falls within the date range

2. **Get Recent Buyers**
   - Endpoint: `GET /api/v1/customers/recent-buyers`
   - Query parameters:
     - `days`: Number of days to look back
   - Returns customers who made purchases in the last N days

3. **Get Popular Categories**
   - Endpoint: `GET /api/v1/customers/{customerId}/popular-categories`
   - Returns product categories purchased by the customer with purchase counts

## Setup

1. Update the connection string in `appsettings.json` if needed
2. Run Entity Framework migrations:
   ```
   dotnet ef migrations add InitialCreate --project src/StoreManagement.Infrastructure --startup-project src/StoreManagement.Api
   dotnet ef database update --project src/StoreManagement.Infrastructure --startup-project src/StoreManagement.Api
   ```
3. Run the application:
   ```
   dotnet run --project src/StoreManagement.Api
   ```

## Architecture

The solution implements CQRS (Command Query Responsibility Segregation) pattern using MediatR:

- **Queries**: Handle data retrieval operations
- **Commands**: Handle data modification operations (not implemented in this version)

### Database
- Entity Framework Core with SQL Server
- Clean separation through `IApplicationDbContext`
- Proper entity configurations
- Database seeding for development

### Testing
- Integration tests using NUnit
- In-memory SQL Server database for testing
- FluentAssertions for readable assertions

## Development Guidelines

1. **Error Handling**
   - Use custom exception types for different error scenarios
   - Let the global middleware handle exception translation
   - Return appropriate HTTP status codes

2. **Validation**
   - Add validation rules in respective query/command validators
   - Use FluentValidation's built-in rules where possible
   - Add custom validation rules when needed

3. **Database**
   - Use migrations for schema changes
   - Configure entities in separate configuration classes
   - Use appropriate delete behaviors for relationships 
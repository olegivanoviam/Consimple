# Store Management API

A .NET Core Web API for managing store data, including customers, products, and purchases.

## Technologies Used

- ASP.NET Core 9
- Entity Framework Core 9
- MediatR (CQRS implementation)
- AutoMapper
- FluentValidation
- SQL Server

## Project Structure

The solution follows Clean Architecture principles:

- **Domain Layer**: Core business entities
- **Application Layer**: Use cases and business logic
- **Infrastructure Layer**: External concerns (database, etc.)
- **API Layer**: Web API endpoints

## API Endpoints

### Customers

1. **Get Birthday Customers**
   - Endpoint: `GET /api/customers/birthday?date={date}`
   - Returns customers whose birthday is on the given date

2. **Get Recent Buyers**
   - Endpoint: `GET /api/customers/recent-buyers?days={days}`
   - Returns customers who made purchases in the last N days

3. **Get Popular Categories**
   - Endpoint: `GET /api/customers/{customerId}/popular-categories`
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

Entity Framework Core is used with SQL Server for data persistence, with a clean separation of concerns through repository interfaces. 
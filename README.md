# SimpleWallet API

A wallet management API built with ASP.NET Core 10.0, PostgreSQL, and Clean Architecture patterns.

## Features

- **JWT Authentication**: Secure Bearer token-based authentication
- **Role-Based Authorization**: Admin-only operations like wallet deletion
- **Wallet Management**: Create, retrieve, and delete wallets
- **Transaction Operations**: Deposit, withdraw, and transfer funds
- **Global Error Handling**: Consistent error responses via middleware
- **Docker Support**: Full containerization with PostgreSQL
- **Swagger Documentation**: Interactive API documentation at `/swagger`
- **Clean Architecture**: Separation of concerns across Domain, Application, Infrastructure, and API layers
- **Comprehensive Testing**: Unit tests for domain logic, integration tests for API endpoints

## Project Structure

```
src/
â”œâ”€â”€ SimpleWallet.Api/              # API layer (controllers, DTOs, middleware)
â”œâ”€â”€ SimpleWallet.Application/      # Application layer (services, interfaces)
â”œâ”€â”€ SimpleWallet.Domain/           # Domain layer (entities, exceptions, validation)
â”œâ”€â”€ SimpleWallet.Infrastructure/   # Infrastructure layer (repositories, database)
â””â”€â”€ SimpleWallet.Tests/            # Test projects
    â”œâ”€â”€ Wallet.UnitTests/         # Domain logic tests
    â””â”€â”€ Wallet.IntegrationTests/  # API integration tests
```

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- Docker & Docker Compose (for containerized setup)
- PostgreSQL 16 (if running locally without Docker)

### Running with Docker (Recommended)

1. **Start all services:**

   ```bash
   docker compose up -d
   ```

   This will start:
   - PostgreSQL 16 database on `localhost:5432`
   - API on `http://localhost:8080`

2. **Access Swagger UI:**

   ```
   http://localhost:8080/swagger
   ```

3. **Stop services:**
   ```bash
   docker compose down
   ```

### Running Locally

1. **Start only the database:**

   ```bash
   docker compose up -d db
   ```

2. **Run the API:**

   ```bash
   cd src/SimpleWallet.Api
   dotnet run
   ```

   The API will be available at `https://localhost:5071`

3. **Access Swagger UI:**
   ```
   https://localhost:5071/swagger
   ```

## Database

### Automatic Setup

The database schema is automatically created on startup via EF Core's `EnsureCreated()` method.

### Seed Data

The following data is automatically seeded on first run:

**Users:**

- User 1: ID `00000000-0000-0000-0000-000000000001`
- User 2: ID `00000000-0000-0000-0000-000000000002`

**Wallets:**

- Wallet 1: Balance = 1000, Owner = User 1
- Wallet 2: Balance = 500, Owner = User 2

**Transactions:**

- Sample transactions showing deposits and transfers between the wallets

## Authentication

### Login

The API includes two pre-configured users for development/testing:

**Admin User:**

- Username: `admin`
- Password: `admin123`
- Role: Admin (can delete wallets)

**Regular User:**

- Username: `user`
- Password: `user123`
- Role: User

### Obtaining a Token

```bash
curl -X POST http://localhost:8080/api/Authentication/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}'
```

Response:

```json
{
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Using the Token

Include the token in the Authorization header:

```
Authorization: Bearer {token}
```

## API Endpoints

### Authentication

- `POST /api/Authentication/login` - Obtain JWT token

### Wallets

- `GET /api/Wallet/{id}` - Get wallet by ID
- `GET /api/Wallet/user/{userId}` - Get all wallets for a user
- `POST /api/Wallet` - Create a new wallet
- `DELETE /api/Wallet/{id}` - Delete wallet (Admin only) [Requires JWT]

### Transactions

- `POST /api/Transaction/deposit` - Deposit funds to a wallet
- `POST /api/Transaction/withdraw` - Withdraw funds from a wallet
- `POST /api/Transaction/transfer` - Transfer funds between wallets

## Testing

### Run All Tests

```bash
dotnet test
```

### Run Unit Tests

```bash
dotnet test src/SimpleWallet.Tests/Wallet.UnitTests/Wallet.UnitTests.csproj
```

### Run Integration Tests

```bash
# Requires API running on http://localhost:8080
dotnet test src/SimpleWallet.Tests/Wallet.IntegrationTests/Wallet.IntegrationTests.csproj
```

### Test Coverage

**Unit Tests (3):**

- Wallet domain logic: insufficient funds validation, deposits, transfers

**Integration Tests (3):**

- Authentication flow with token validation
- Authorization enforcement on protected endpoints
- API endpoint functionality

## Configuration

Key settings in `appsettings.json`:

```json
{
	"ConnectionStrings": {
		"PostgresConnection": "Host=localhost;Port=5432;Database=simplewallet;Username=postgres;Password=postgres"
	},
	"Jwt": {
		"Issuer": "SimpleWalletAPI",
		"Audience": "SimpleWalletClient",
		"Key": "your-secret-key-must-be-at-least-32-characters-long!!",
		"ExpirationMinutes": 60
	}
}
```

## Postman Collection

A pre-configured Postman collection is available: `wallet.postman_collection.json`

**To import:**

1. Open Postman
2. Click "Import" â†’ "File"
3. Select `wallet.postman_collection.json`
4. Set the `baseUrl` variable to `http://localhost:8080`
5. Use the "Login" endpoint to get a token and populate the `token` variable

## Error Handling

The API returns consistent error responses:

- `400 Bad Request` - Validation errors, invalid input, insufficient funds
- `401 Unauthorized` - Missing or invalid JWT token
- `403 Forbidden` - Insufficient permissions (e.g., non-admin trying to delete)
- `404 Not Found` - Resource does not exist
- `500 Internal Server Error` - Unexpected server error

All errors include a JSON response with status, title, and detail fields.

## Development Notes

### Architecture Layers

1. **Domain Layer**: Business entities, validation, and domain-specific exceptions
2. **Application Layer**: Services that orchestrate business logic
3. **Infrastructure Layer**: Data access repositories and database context
4. **API Layer**: Controllers, DTOs, middleware, and HTTP concerns

### Middleware Pipeline

1. Exception handling (catches and maps domain exceptions to HTTP status codes)
2. Database initialization and seeding
3. Authentication & Authorization
4. Controller routing

## Building for Production

```bash
dotnet publish -c Release -o ./publish
```


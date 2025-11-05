# Secret Santa / Amigo Oculto

A modern Secret Santa (Amigo Oculto) web application built with .NET Aspire, enabling users to organize gift exchanges by automatically pairing participants and sending email notifications.

## Project Overview

This application simplifies Secret Santa gift exchange organization by:
- Providing a web interface to manage participants (friends) with their names and emails
- Randomly generating gift-giver/receiver pairs using a fair lottery algorithm
- Automatically emailing each participant with their assigned gift recipient
- Supporting both English (Secret Santa) and Portuguese (Amigo Oculto) languages

## Architecture

The project follows a distributed application architecture using .NET Aspire, consisting of seven modules:

### 1. **SecretSanta.AppHost**
The Aspire orchestration host that coordinates all services in the distributed application.

**Key Responsibilities:**
- Service discovery and configuration
- Managing service dependencies and startup order
- Local development orchestration

**Implementation Details:**
- Built on .NET 9.0 with Aspire hosting
- Configures the API service and web frontend
- Ensures the web frontend waits for the API service to be ready
- Database and Redis components are currently commented out (prepared for future use)

### 2. **SecretSanta.Web**
A Blazor Server web application providing the user interface for managing Secret Santa events.

**Key Responsibilities:**
- Interactive UI for adding/managing participants
- Dynamic form with adjustable number of friends
- Form validation and submission
- Display of generated pairs (for testing/verification)
- Communication with the API service

**Implementation Details:**
- Built with Blazor Server (Interactive Server render mode)
- Uses Bootstrap Blazor for UI components
- Implements form validation with DataAnnotations
- HTTP client configured for service discovery
- Responsive bilingual interface (English/Portuguese)

**Pages:**
- `Home.razor`: Main page with friend management and lottery submission

### 3. **SecretSanta.ApiService**
RESTful API service that handles the core lottery logic and email distribution.

**Key Responsibilities:**
- Receive participant lists from the web frontend
- Execute lottery algorithm to generate random pairs
- Send personalized emails to each participant
- Provide Swagger documentation for API endpoints

**Implementation Details:**
- Built on ASP.NET Core Web API (.NET 8.0)
- Uses AutoMapper for DTO mapping
- Implements dependency injection for services
- PostgreSQL database integration (via Entity Framework Core)
- Swagger/OpenAPI documentation enabled

**Endpoints:**
- `POST /api/Lottery`: Accepts friend list and triggers the lottery and email distribution

**Interesting Implementation Choice - Lottery Algorithm:**
The pairing algorithm (`DrawPairs` method) uses a circular arrangement:
1. Randomizes the participant order using `OrderBy(_ => rnd.Next())`
2. Creates pairs where each person gives to the next person in the shuffled list
3. The last person gives to the first person (completing the circle)
4. This ensures everyone gives and receives exactly one gift with no orphaned participants

### 4. **SecretSanta.MigrationsService**
Database migration service that initializes and updates the database schema.

**Key Responsibilities:**
- Create database if it doesn't exist
- Apply Entity Framework migrations
- Ensure database is ready before other services start

**Implementation Details:**
- Hosted background service that runs on startup
- Uses execution strategies for resilience
- Applies migrations within transactions for atomicity
- Automatically stops after completing migrations
- Includes OpenTelemetry tracing for observability

### 5. **SecretSanta.Models**
Shared data models and Entity Framework DbContext.

**Key Responsibilities:**
- Define data entities (e.g., `DrawEntry`)
- Provide database context for Entity Framework Core
- Ensure consistent data models across services

**Implementation Details:**
- Contains `SecretSantaDBContext` for database operations
- `DrawEntry` model stores historical lottery results with giver/receiver pairs

### 6. **SecretSanta.ServiceDefaults**
Common service configuration and extensions used across all services.

**Key Responsibilities:**
- Configure OpenTelemetry (metrics, tracing, logging)
- Set up health checks
- Configure service discovery
- Apply resilience patterns (retries, circuit breakers, timeouts)

**Implementation Details:**
- Adds standard instrumentation for ASP.NET Core and HTTP clients
- Configures OTLP exporter for telemetry
- Implements default health check endpoints (`/health`, `/alive`)
- Applies standard resilience handlers to HTTP clients

**Interesting Implementation Choice:**
This module encapsulates all cross-cutting concerns in reusable extensions, promoting consistency and reducing duplication across services.

### 7. **SecretSanta.Tests**
Test project for integration and end-to-end testing.

**Key Responsibilities:**
- Verify application functionality
- Test Aspire distributed application behavior
- Ensure service communication works correctly

**Implementation Details:**
- Uses xUnit for testing framework
- Leverages Aspire.Hosting.Testing for distributed application tests
- Tests web frontend availability and basic functionality

## Interesting Implementation Choices

### 1. Email Templating System
The email service uses a flexible templating approach:
- HTML templates are stored as external files (e.g., `SecretSantaEmailTemplate.html`)
- Placeholders (`{{GiverName}}`, `{{ReceiverName}}`, `{{SentDate}}`) are replaced at runtime
- Template path is configurable via `appsettings.json`
- Allows easy customization without code changes
- Supports internationalization by using different template files

**Example Template:**
```html
<h1>ÚLTIMA HORA: {{SentDate}} - 🎅 Amigo Oculto 🎁</h1>
<p>Ora boas {{GiverName}},</p>
<p>Foste escolhido como o amigo oculto de <strong>{{ReceiverName}}</strong>!</p>
```

### 2. .NET Aspire Architecture
The project leverages .NET Aspire's capabilities:
- **Service Discovery**: Services locate each other using logical names (e.g., "apiservice")
- **Observability**: Built-in OpenTelemetry for distributed tracing and metrics
- **Local Development**: Simplified multi-service debugging and orchestration
- **Cloud-Ready**: Easy transition from local development to production deployment

### 3. Fair Lottery Algorithm
The circular pairing ensures mathematical fairness:
- Every participant gives to exactly one person
- Every participant receives from exactly one person
- No possibility of self-assignment
- No orphaned participants in the exchange
- Random shuffling provides unpredictability

### 4. Async Email Sending
The email service uses fire-and-forget pattern:
- Emails are sent asynchronously using `SendMailAsync`
- Non-blocking to avoid request timeouts
- Console logging tracks email sending progress
- Errors are caught and logged without blocking the entire process

## Prerequisites

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [.NET Aspire workload](https://learn.microsoft.com/dotnet/aspire/fundamentals/setup-tooling)
- PostgreSQL database (optional, currently commented out in AppHost)
- SMTP server credentials for email functionality

### Installing .NET Aspire Workload

```bash
dotnet workload update
dotnet workload install aspire
```

## Development Guide

### 1. Clone the Repository

```bash
git clone <repository-url>
cd SecretSanta
```

### 2. Configure Email Settings

Create or update `SecretSanta.ApiService/appsettings.json` with your SMTP configuration:

```json
{
  "MailSettings": {
    "Host": "smtp.example.com",
    "DefaultCredentials": false,
    "Port": 587,
    "Name": "Secret Santa",
    "EmailId": "noreply@example.com",
    "UserName": "noreply@example.com",
    "Password": "your_password_here",
    "UseSSL": true,
    "DefaultSubject": "🎅 Your Secret Santa Assignment"
  },
  "template_path": "/absolute/path/to/SecretSanta/SecretSanta.ApiService/Services/Email/EmailTemplate",
  "secret_santa_template": "SecretSantaEmailTemplate.html"
}
```

**Important Notes:**
- Use absolute paths for `template_path`
- Example Windows path: `"C:\\Users\\YourName\\SecretSanta\\SecretSanta.ApiService\\Services\\Email\\EmailTemplate"`
- Example Linux/Mac path: `"/home/yourname/SecretSanta/SecretSanta.ApiService/Services/Email/EmailTemplate"`
- On Windows, use double backslashes (`\\`) or forward slashes (`/`)
- On Linux/Mac, use forward slashes (`/`)
- The template file is located at: `SecretSanta.ApiService/Services/Email/EmailTemplate/SecretSantaEmailTemplate.html`

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Build the Solution

```bash
dotnet build
```

### 5. Run the Application

#### Option A: Using Aspire AppHost (Recommended)

```bash
dotnet run --project SecretSanta.AppHost
```

This will start all services and open the Aspire Dashboard, where you can:
- Monitor all running services
- View logs and traces
- Access the web frontend
- Check service health

#### Option B: Running Services Individually

**Terminal 1 - API Service:**
```bash
dotnet run --project SecretSanta.ApiService
```

**Terminal 2 - Web Frontend:**
```bash
dotnet run --project SecretSanta.Web
```

### 6. Access the Application

- **Web Frontend**: Automatically opened by Aspire Dashboard, or check console output for URL (typically `https://localhost:7xxx`)
- **API Swagger**: `https://localhost:<api-port>/swagger`
- **Aspire Dashboard**: `http://localhost:15xxx` (port shown in console)

### 7. Using the Application

1. Open the web interface
2. Set the number of participants using the `+` and `-` buttons
3. Enter each participant's name and email address
4. Click "Roll / Sortear" to generate pairs
5. Each participant will receive an email with their assigned gift recipient
6. The pairs are also displayed on the page for verification (typically only the organizer should see this)

## Building for Production

```bash
dotnet publish -c Release
```

The published files will be in the `bin/Release/net8.0/publish` (or `net9.0/publish`) directory for each project.

## Testing

Run the test suite:

```bash
dotnet test
```

**Note**: Integration tests require the Aspire hosting infrastructure and may need additional setup for the test environment.

## Project Structure

```
SecretSanta/
├── SecretSanta.ApiService/          # REST API service
│   ├── Controllers/                  # API endpoints
│   ├── DTOs/                        # Data transfer objects
│   ├── Services/                    # Business logic (email service)
│   │   └── Email/
│   │       └── EmailTemplate/       # Email templates
│   └── Repository/                  # Data access layer
├── SecretSanta.Web/                 # Blazor web frontend
│   ├── Components/                  # Blazor components
│   │   └── Pages/                   # Razor pages
│   ├── Models/                      # View models
│   └── Services/                    # API client services
├── SecretSanta.AppHost/             # Aspire orchestration
├── SecretSanta.MigrationsService/   # Database migrations
│   └── Migrations/                  # EF Core migrations
├── SecretSanta.Models/              # Shared data models
│   └── Models/                      # Entity models
├── SecretSanta.ServiceDefaults/     # Shared configuration
└── SecretSanta.Tests/               # Test project
```

## Configuration Options

### Email Template Customization

Edit `SecretSanta.ApiService/Services/Email/EmailTemplate/SecretSantaEmailTemplate.html` to customize the email appearance.

Available placeholders:
- `{{SentDate}}` - Current date
- `{{GiverName}}` - Name of the person giving the gift
- `{{ReceiverName}}` - Name of the assigned recipient
- `{{ReceiverEmail}}` - Email of the assigned recipient

### Database Configuration (Currently Disabled)

The project includes PostgreSQL support (currently commented out). To enable:

1. Uncomment database-related code in `SecretSanta.AppHost/Program.cs`
2. Configure connection string in `appsettings.json`
3. Run migrations using the MigrationsService

## Troubleshooting

### Email Not Sending

- Verify SMTP settings in `appsettings.json`
- Check firewall rules for SMTP port (usually 587 or 465)
- Enable "Less secure app access" if using Gmail (or use app-specific passwords)
- Check console logs for detailed error messages

### Template Not Found

- Ensure `template_path` in `appsettings.json` is an absolute path
- Verify the template file exists at the specified location
- Check file permissions

### Service Discovery Issues

- Ensure you're running through the AppHost
- Check Aspire Dashboard for service status
- Verify service names match configuration

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

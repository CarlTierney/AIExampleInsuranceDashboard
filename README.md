# Insurance Dashboard Solution

This repo contains some sample code built with AI agent mode using copilot.

- **DashboardApi**: ASP.NET Core Web API backend using Entity Framework Core 8 for SQL Server 
- **dashboard-frontend**: Next.js (TypeScript, Tailwind CSS) frontend for a configurable insurance dashboard.
- **Domain**: .NET EF Core domain model
- **DomainDataGenerator**: A .NET console application to generate data to use with the dasbhoard

## Features
- SimpleInsurance domain model: Customers, Policies, Claims, Agents, Payments, etc.
- Test data generation for backend
- Responsive dashboard UI with configurable layout and panels (charts, tables, etc.)

## Getting Started
using the prompt in initialprompt.txt with vscode and github copilot the initial projects and files were created using agent mode of copilot in additionl to leveraging the following screen shot

![Insurance-claim-analysis](https://github.com/user-attachments/assets/a7e3ef26-2da0-4a29-9135-a71dd79d6300)

### Backend (DashboardApi)
1. Update `appsettings.json` with your SQL Server connection string and OAuth settings. Or for local development use dotnet user-secrets
2. Run database migrations: `dotnet ef database update`
3. Start the API: `dotnet run --project DashboardApi`

### Frontend (dashboard-frontend)
1. Install dependencies: `npm install` (in `dashboard-frontend`)
2. Start the dev server: `npm run dev`

## Customization
- Dashboard layout and panels are driven by a JSON config file in the frontend.
- Extend the backend domain model and API as needed for new dashboard features.

---

For more details, see the code and comments in each project folder.


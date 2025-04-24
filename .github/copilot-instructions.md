<!-- Use this file to provide workspace-specific custom instructions to Copilot. For more details, visit https://code.visualstudio.com/docs/copilot/copilot-customization#_use-a-githubcopilotinstructionsmd-file -->

This workspace contains:
- An ASP.NET Core Web API backend (DashboardApi) using Entity Framework Core 8 for SQL Server and OAuth authentication.
- A Next.js (TypeScript, Tailwind CSS) frontend (dashboard-frontend) for a configurable insurance dashboard.

Copilot should:
- Generate C# code for EF Core 8 models, DbContext, and API controllers in DashboardApi.
- Generate TypeScript/React components for dashboard panels in dashboard-frontend, with layout/config driven by a JSON config file.
- Use best practices for secure API/OAuth integration between frontend and backend.
- Provide test data generation for the backend domain model.

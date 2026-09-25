# EPC Project & Vendor Management System

A personal **EPC (Engineering, Procurement, Construction) Project & Vendor Management portfolio and learning application**, built with C# and ASP.NET Core MVC.

**Current status: Phase 1 COMPLETE; Phase 2 IN PROGRESS.** The C#/.NET 8 warm-up foundation is complete. The MVC application skeleton exists, and the Department CRUD vertical slice is partially implemented through Index, Create, Details, Edit, and Deactivate. Service extraction and meaningful application tests remain pending.

> **Disclaimer:** This is a personal learning and portfolio project. It is **not affiliated with, built for, or used by any real company**. All business names, clients, vendors, and data are fictional.

## Implemented / Verified in Source

- **Completed — C#/.NET 8 foundation:** console exercises covering domain classes, encapsulation, LINQ, and async/await, plus three warm-up xUnit tests.
- **Completed — application skeleton:** `EpcVendorManagement.slnx`, a .NET 8 MVC web project, and a separate xUnit application test project referencing the web project.
- **Partially completed — Department vertical slice:** Index and navigation; Create and Edit forms with ViewModels, validation attributes, client validation scripts, server validation, duplicate-name checks, and Post/Redirect/Get; Details; POST Deactivate setting `IsActive` to false without deleting the record. State-changing actions have anti-forgery validation.
- **Completed — persistence foundation in source:** `Department`, `ApplicationDbContext`, SQL Server provider registration, async EF Core queries/saves, and the `InitialCreate` migration with a unique department-name index. Local migration application still needs verification.

The application test project currently contains one empty template test, not Department behavior coverage. See [CURRENT_STATUS.md](CURRENT_STATUS.md) for verification limits and the next task.

## Planned

The intended business scope is a fictional EPC company's projects, departments, employees, vendors, assignments, and business approvals. These features remain **planned**:

- ASP.NET Core Identity login, roles, and server-side authorization
- Employee, Vendor, and Project web modules (warm-up classes are learning exercises, not these modules)
- Employee/vendor project assignments and conflict checks
- Vendor registration approval workflow and status history
- Audit fields, dashboard, reporting, search, filtering, and pagination
- jQuery/Ajax partial page enhancements
- Docker/Compose packaging and optional Azure deployment

Intended fictional roles are Administrator, Project Manager, Procurement Officer, and General Employee. No demo accounts or deployed service are available here.

## Technology Stack

| Area | Current implementation / planned additions |
|---|---|
| Language and target | C#, .NET 8 (`net8.0` in all four projects) |
| Web | ASP.NET Core MVC, Razor views |
| Persistence | EF Core 8, SQL Server provider, code-first migration |
| Front end | Bootstrap, JavaScript, jQuery validation; Ajax enhancements planned |
| Testing | xUnit warm-up tests; application test-project foundation |
| Authentication | ASP.NET Core Identity planned |
| Deployment | Docker and Azure planned |

## Architecture Summary

The repository has one MVC web project and one application test project, alongside the separate warm-up projects. Department forms use ViewModels; the current controller accesses `ApplicationDbContext` directly. Extracting Department queries and rules into services is the next architectural step in Phase 2.

The intended flow is Controllers → Services → EF Core → SQL Server. [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) and [docs/DECISIONS.md](docs/DECISIONS.md) explain the direction; some supporting documentation still describes an earlier or proposed state. The implementation summary above reflects the inspected branch.

## Repository Layout

```text
EpcVendorManagement.slnx          Web + application test solution
src/EpcVendorManagement.Web/      MVC application and Department module
src/Warmup/                       C# console learning exercises
tests/EpcVendorManagement.Tests/  Application test foundation (placeholder)
tests/Warmup.Tests/               Three warm-up tests; outside the solution
ROADMAP.md                        Phased development and learning plan
CURRENT_STATUS.md                 Current progress and verification limits
AGENTS.md / CLAUDE.md              Coding-agent guidance
docs/                             Supporting project documentation
```

## Documentation Index

| File | Purpose |
|---|---|
| [ROADMAP.md](ROADMAP.md) | Phased plan with learning and interview objectives |
| [CURRENT_STATUS.md](CURRENT_STATUS.md) | What is done, in progress, and next |
| [docs/SETUP.md](docs/SETUP.md) | Windows environment setup, step by step |
| [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) | Architecture, request flow, design decisions |
| [docs/DATABASE.md](docs/DATABASE.md) | Data model, relationships, SQL practice |
| [docs/TESTING.md](docs/TESTING.md) | Testing strategy and checklists |
| [docs/SECURITY.md](docs/SECURITY.md) | Security approach and checklist |
| [docs/INTERVIEW_GUIDE.md](docs/INTERVIEW_GUIDE.md) | Interview Q&A tied to the project |
| [docs/DECISIONS.md](docs/DECISIONS.md) | Architecture decision records |
| [docs/PORTFOLIO_NOTES.md](docs/PORTFOLIO_NOTES.md) | Resume/LinkedIn/portfolio material |
| [docs/API_AND_WORKFLOWS.md](docs/API_AND_WORKFLOWS.md) | Workflows, endpoints, status transitions |
| [docs/UI_UX.md](docs/UI_UX.md) | Screens, journeys, UI states |

## Local Setup

Use a .NET SDK that supports the checked-in `.slnx` solution (the inspected environment uses SDK 10.0.301), with .NET 8 runtime support, SQL Server, and the EF Core 8 CLI tool. All projects target `net8.0`; SDK version and target framework are different. With an older SDK, restore/build the individual `.csproj` files instead of the `.slnx` file.

From a fresh clone:

```powershell
git clone https://github.com/Saurabhzambare/epc-vendor-management.git
cd epc-vendor-management
git switch feature/solution-skeleton
dotnet restore EpcVendorManagement.slnx
dotnet build EpcVendorManagement.slnx
```

Configure `ConnectionStrings:DefaultConnection` using user secrets for `src/EpcVendorManagement.Web`, or the `ConnectionStrings__DefaultConnection` environment variable. Use your own local SQL Server instance and database; do not commit connection strings or credentials. The web project already has a `UserSecretsId`.

With the connection configured and `dotnet-ef` 8.x installed, apply the existing migration and run locally:

```powershell
dotnet ef database update --project src/EpcVendorManagement.Web --startup-project src/EpcVendorManagement.Web
dotnet run --project src/EpcVendorManagement.Web --launch-profile https
```

Open the URL printed by the app and navigate to `/Departments`. The migration command changes your configured database. These setup steps were not verified end to end during this documentation cleanup.

[docs/SETUP.md](docs/SETUP.md) has additional Windows environment guidance, but its “nothing to run yet” wording is stale; use the existing project paths above. Docker setup is planned, not available.

## Running Tests

```powershell
dotnet test EpcVendorManagement.slnx
dotnet test tests/Warmup.Tests/Warmup.Tests.csproj
```

The warm-up suite contains three substantive tests. The application suite contains only an empty template test, so a passing application test run would not establish Department correctness.

**Verification note:** The current implementation is present in source on `feature/solution-skeleton`. Runtime/database verification details and current testing limitations are documented in [CURRENT_STATUS.md](CURRENT_STATUS.md).

## Learning and Interview Focus

Implemented source provides examples of C# classes, LINQ, async/await, MVC routing and model binding, dependency injection, form ViewModels, validation, EF Core migrations, and anti-forgery handling. Service-layer testing, Identity/roles, approval rules, Ajax, and deployment remain learning objectives in [ROADMAP.md](ROADMAP.md).

## Honest Limitations

- Fictional learning application with no production users or deployment claimed.
- Department functionality is implemented in source but Phase 2 completion criteria are not yet met; service extraction, meaningful tests, and runtime verification remain.
- Authentication and role restrictions are not implemented.
- Screenshots and demo credentials can be added after the relevant features are verified.

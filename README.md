# EPC Project & Vendor Management System

An enterprise-style **EPC (Engineering, Procurement, Construction) project and vendor management application** inspired by common engineering, procurement, and approval workflows.

> **Disclaimer:** This is a personal learning and portfolio project. It is **not affiliated with, built for, or used by any real company**. All business names, clients, vendors, and data are fictional.

## Project Summary

Large engineering and construction companies run internal software to track projects, the employees and vendors assigned to them, and the purchase/approval workflows that connect them. This repository is designed to grow into that class of full-stack ASP.NET Core MVC application.

The implemented code currently provides a tested .NET 8 C# foundation: vendor, employee, and project domain exercises; LINQ filtering, grouping, and joins; asynchronous loading; and xUnit tests. The MVC application, persistence, Identity, and business workflows remain approved design and roadmap work rather than implemented features.

It exists for two connected purposes:

1. **Interview preparation** — practical revision of C#, ASP.NET Core MVC, Entity Framework Core, SQL Server, Bootstrap, jQuery, and Ajax for a Full Stack Developer role.
2. **Portfolio** — a polished, honestly-documented enterprise-style application demonstrating professional development workflow.

## Business Problem

An EPC company needs to:

- Track projects, budgets, milestones, and statuses
- Manage departments and employees
- Register and evaluate vendors
- Assign employees and vendors to projects without conflicts
- Route business requests (e.g., vendor registration, purchase requests) through an approval workflow
- Keep an audit history of who changed what and when
- Report on projects, vendors, and pending approvals

## Intended Users (Fictional Roles)

- Administrator
- Project Manager
- Procurement Officer
- General Employee

(Role model may be refined — see `docs/DECISIONS.md`.)

## Main Features (Planned)

- Authentication and role-based authorization (ASP.NET Core Identity)
- Department, Employee, Vendor, and Project management (CRUD)
- Project assignments with duplicate/validity checks
- Vendor registration request with an approval workflow (Draft → Submitted → Under Review → Approved/Rejected)
- Dashboard with pending approvals and active projects
- Search, filtering, sorting, and pagination
- Audit fields and status history
- Ajax-driven partial page updates (jQuery)
- Reports (projects by status, vendors by category, pending requests)

## Approved Target Technology Stack

| Layer | Technology |
|---|---|
| Language | C# (.NET 8) |
| Web framework | ASP.NET Core MVC |
| Data access | Entity Framework Core |
| Database | SQL Server |
| Auth | ASP.NET Core Identity (cookies, roles, claims) |
| Front end | Razor Views, HTML5, CSS3, Bootstrap, JavaScript, jQuery, Ajax |
| Testing | xUnit |
| Tooling | Visual Studio, .NET CLI, SSMS, Git, GitHub, GitHub Desktop, Docker (later), Azure fundamentals (later) |

Current verified code evidence uses C#, .NET 8, LINQ, async/await, and xUnit. The other technologies in this table describe the approved application direction and must not be presented as implemented until the corresponding source exists.

## Architecture Summary

The approved architecture is a single ASP.NET Core MVC web project with clear internal layering (Controllers → Services → EF Core → SQL Server), ViewModels for screens/forms, and a separate xUnit test project. This is documented design direction; the current repository has not yet implemented that MVC structure. Full reasoning: `docs/ARCHITECTURE.md` and `docs/DECISIONS.md`.

## Repository Layout

```
README.md            Project overview (this file)
AGENTS.md            Rules for AI coding agents
CLAUDE.md            Rules for Claude Code
ROADMAP.md           Phased development plan
CURRENT_STATUS.md    Live project status — read this first
docs/                All other project documentation
src/                 Current .NET 8 warm-up source; future MVC application source
tests/               Current xUnit warm-up tests; future application tests
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

See [docs/SETUP.md](docs/SETUP.md). Short version (once the solution exists in Phase 2):

```
git clone <repo-url>
cd <repo>
dotnet restore
dotnet ef database update
dotnet run --project src/EpcVendorManagement.Web
```

## Docker Setup

Planned for a later phase (see ROADMAP.md, Phase 8). Not yet available.

## Running the Current Code and Tests

```bash
dotnet run --project src/Warmup
dotnet test tests/Warmup.Tests
```

The current tests cover the warm-up vendor model and asynchronous vendor loading. Application-level MVC, persistence, authorization, and workflow tests will be added only when those features are implemented.

## Demo Credentials

Development-only seeded accounts will be documented here once Identity is implemented (Phase 3). Never real credentials.

## Screenshots

*To be added as features are completed.*

## Current Status

**Phase 1 is partially complete.** The repository contains tested .NET 8 domain-class, LINQ, async/await, and xUnit warm-up work. The roadmap's `IApprovable` interface exercise remains outstanding, and Phase 2 has not started. The ASP.NET Core MVC solution and business workflows are not implemented yet. See [CURRENT_STATUS.md](CURRENT_STATUS.md).

## Interview Concepts Demonstrated

Current code demonstrates OOP, encapsulation, nullable reference types, LINQ, SQL-oriented join/grouping concepts, async/await, and xUnit testing. Planned application work will add MVC request lifecycle, dependency injection, EF Core relationships and migrations, Identity/roles, validation, jQuery/Ajax partial updates, and web-security controls.

## Honest Limitations

- Learning project: fictional data, no production users, no real deployments claimed
- Features are only marked complete after they are implemented **and tested**
- Scope is intentionally limited compared to real enterprise EPC software

## Lessons Learned

*Maintained as the project progresses.*

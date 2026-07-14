# EPC Project & Vendor Management System

An enterprise-style **EPC (Engineering, Procurement, Construction) project and vendor management application** inspired by common engineering, procurement, and approval workflows.

> **Disclaimer:** This is a personal learning and portfolio project. It is **not affiliated with, built for, or used by any real company**. All business names, clients, vendors, and data are fictional.

## Project Summary

Large engineering and construction companies run internal software to track projects, the employees and vendors assigned to them, and the purchase/approval workflows that connect them. This application simulates that class of software as a full-stack ASP.NET Core MVC application.

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

## Technology Stack

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

## Architecture Summary

Single ASP.NET Core MVC web project with clear internal layering (Controllers → Services → EF Core → SQL Server), ViewModels for screens/forms, and a separate xUnit test project. The structure is deliberately simple and can evolve into multiple projects if the codebase justifies it. Full reasoning: `docs/ARCHITECTURE.md` and `docs/DECISIONS.md`.

## Repository Layout

```
README.md            Project overview (this file)
AGENTS.md            Rules for AI coding agents
CLAUDE.md            Rules for Claude Code
ROADMAP.md           Phased development plan
CURRENT_STATUS.md    Live project status — read this first
docs/                All other project documentation
src/                 Application source (created in Phase 2)
tests/               Test projects (created in Phase 2)
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

## Running Tests

Planned from Phase 2 onward: `dotnet test`

## Demo Credentials

Development-only seeded accounts will be documented here once Identity is implemented (Phase 3). Never real credentials.

## Screenshots

*To be added as features are completed.*

## Current Status

**Phase 0/1 — planning, environment setup, and interview fast-track.** No application code exists yet. See [CURRENT_STATUS.md](CURRENT_STATUS.md).

## Interview Concepts Demonstrated

Will grow with the project. Target list: OOP, LINQ, async/await, MVC request lifecycle, dependency injection, EF Core relationships and migrations, SQL joins, Identity/roles, validation (client + server), jQuery/Ajax partial updates, anti-forgery/XSS/SQL-injection defenses, xUnit testing, Git workflow.

## Honest Limitations

- Learning project: fictional data, no production users, no real deployments claimed
- Features are only marked complete after they are implemented **and tested**
- Scope is intentionally limited compared to real enterprise EPC software

## Lessons Learned

*Maintained as the project progresses.*

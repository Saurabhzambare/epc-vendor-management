# CURRENT_STATUS

> Read this file first in every session. Update it after every meaningful work session.

**Last updated:** 2026-07-14

## Current Phase

**Phase 1 — C# Interview Fast-Track (console warm-up)** — Phase 0 completed 2026-07-14 (see ROADMAP.md)

## Current Objective

Build the warm-up console project step by step: model `Vendor`/`Project`/`Employee`, an `IApprovable` interface, LINQ drills over in-memory lists, first xUnit tests. Saurabh types the code locally with guided explanation.

**Interview timeline: expected within the week** — Phases 1–2 are the priority; everything else serves them.

## Completed

- **Phase 1, step 1 (2026-07-14, verified by Saurabh):** docs PR merged to `main`; `feature/csharp-warmup` branch created; `src/Warmup` console project created (`dotnet new console` → "created successfully", `dotnet run` → `Hello, World!`); top-level statements explained. Visual Studio "ASP.NET and web development" workload confirmed installed — Phase 2 fully unblocked.

- Repository assessed: previously a Git-tutorial "Hello-World" repo with a single README and **no application code** — nothing to preserve except history.
- Project purpose, roadmap, and architecture direction defined.
- Documentation foundation created: README, ROADMAP, CURRENT_STATUS, AGENTS, CLAUDE at root; ARCHITECTURE, SETUP, DATABASE, TESTING, SECURITY, INTERVIEW_GUIDE, DECISIONS, PORTFOLIO_NOTES, API_AND_WORKFLOWS, UI_UX under `docs/`.
- Initial architecture decision recorded (DECISIONS.md #001: single web project + test project).
- .NET-appropriate `.gitignore` added.
- **Phase 0 environment verification — COMPLETE (2026-07-14):**
  - ✅ .NET 8 SDK `8.0.422` (x64)
  - ✅ Git `2.55.0.windows.2`
  - ✅ Windows 11 (build 10.0.28120), x64
  - ✅ SQL Server 2025 Developer, **default instance** `MSSQLSERVER`, service Running → connection string uses `Server=localhost`
  - ✅ SSMS 22 (22.7.2) connects to `localhost` via Windows Authentication (verified in Object Explorer)
  - ✅ Visual Studio Community 2026 (18.7.3) installed
  - ✅ GitHub Desktop installed, repo cloned (fetch of the docs branch pending)
  - Note: `SQL Server Browser`/`Agent` services Stopped — normal; not needed for a default instance / this project

## In Progress

- Nothing — awaiting environment verification results from Saurabh.

## Known Issues / Blockers

- No blockers. Repo renamed to `epc-vendor-management` (done); VS workload confirmed; docs merged to `main` via PR #1.

## Tests

- Passing: none exist yet (no code).
- Failing: none.
- Untested areas: everything — no application code exists. All feature claims in docs are **planned**, not built.

## Important Commands

```
dotnet --version        # verify SDK (expect 8.x)
git status              # check working tree before any change
```

(Real build/run/test commands arrive with Phase 2.)

## Recent Decisions

- DECISIONS.md #001 — Start with a single ASP.NET Core MVC project + one test project; split into layers only when justified.
- DECISIONS.md #002 — First workflow will be the Vendor Registration Request.
- Roadmap ordering rationale is in ROADMAP.md's preamble.

## Git State

- Branch: `claude/epc-setup-roadmap-cx782w`
- This commit: documentation foundation (no application code).
- PR status: not yet opened.

## Recommended Next Task

**Phase 1, step 2 (Saurabh, locally, on `feature/csharp-warmup`):** write the `Vendor` class (`src/Warmup/Vendor.cs`) with properties, a constructor, an enum category, and a nullable contact field; create a `List<Vendor>` in `Program.cs` and print active vendors with string interpolation. Concepts: properties, constructors, nullable reference types (INTERVIEW_GUIDE entries added).

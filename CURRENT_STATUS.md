# CURRENT_STATUS

> Read this file first in every session. Update it after every meaningful work session.

**Last updated:** 2026-07-14

## Current Phase

**Phase 1 — COMPLETE (2026-07-14). Phase 2 — Solution Skeleton + Department CRUD — starting.** (see ROADMAP.md)

## Current Objective

Phase 2, step 1: create the `EpcVendorManagement` solution — MVC web project + xUnit test project, both explicitly targeting `net8.0` via `dotnet new -f net8.0` (per DECISIONS.md #005) — run the template app, and tour the generated files. Then the Department vertical slice: entity → DbContext → migration → CRUD screens → service extraction → tests.

**Interview timeline: expected within the week** — Phase 2 is the machine-test rehearsal; highest-value work.

**Phase 1 closure (2026-07-14, verified):** retarget to `net8.0` confirmed in both csproj files, tests re-passed 3/3 on net8.0, `fix: retarget warm-up projects to net8.0` pushed, `feature/csharp-warmup` merged to `main` (PR #2).

## Completed

- **Phase 1, step 1 (2026-07-14, verified by Saurabh):** docs PR merged to `main`; `feature/csharp-warmup` branch created; `src/Warmup` console project created (`dotnet new console` → "created successfully", `dotnet run` → `Hello, World!`); top-level statements explained. Visual Studio "ASP.NET and web development" workload confirmed installed — Phase 2 fully unblocked.
- **Phase 1, step 2 (2026-07-14, verified by Saurabh):** `Vendor` class written with enum category, getter-only `VendorCode`, nullable `ContactEmail`, constructor; used from `Program.cs` with a `List<Vendor>`, collection initializer, and string interpolation — correct filtered output confirmed. CS8618 nullable warning deliberately triggered and understood; encapsulation reasoning for the getter-only property articulated correctly. Concepts covered: properties, constructors, enums, nullable reference types, `var`, string interpolation.
- **Phase 1, step 3 (2026-07-14, verified by Saurabh):** LINQ `Where`/`OrderBy`/`Select`/`ToList`/`FirstOrDefault` working in `Program.cs` with correct output; deferred-execution experiment run with a **correct written prediction** (vendor added after query definition appeared in results); "when does LINQ execute" answered correctly. Concepts covered: lambdas, LINQ method syntax, projection, deferred execution, `First` vs `FirstOrDefault`, ternary operator, `is null`.
- **Phase 1, step 4 (2026-07-14, verified by Saurabh):** `Employee` and `Project` classes added; `GroupBy` (vendors per category) and `Join` (active projects with manager) produce correct output; both SQL twins hand-written correctly (GROUP BY with WHERE; INNER JOIN with alias + ON); orphan-project experiment answered fully (INNER drops it, LEFT JOIN keeps it with NULL manager). Concepts covered: `GroupBy`/`IGrouping`, aggregates, `Join`, FK-by-convention, object initializers, WHERE vs HAVING.
- **Phase 1, step 5 (2026-07-14, verified by Saurabh — PHASE 1 DEFINITION OF DONE MET):** `IsActive` encapsulated behind `Deactivate()` (CS0272 compile error experienced and fixed — encapsulation enforced by compiler); async `VendorRegistry.LoadVendorsAsync` awaited from top-level statements (visible latency observed); `tests/Warmup.Tests` xUnit project created and referenced; **3/3 tests passing** (`Test summary: total: 3, failed: 0, succeeded: 3`). Concepts covered: async/await, `Task<T>`, top-level await, `[Fact]`, Arrange-Act-Assert, async tests.
- **Finding from step 5 build output:** machine has the **.NET 10 SDK** (10.0.301, via VS 2026); templates created Warmup projects targeting `net10.0` instead of the project-standard `net8.0`. Decision + fix recorded as DECISIONS.md #005; retarget pending (see Current Objective).

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

**Retarget Warmup + Warmup.Tests to `net8.0`, re-run tests, commit, and merge `feature/csharp-warmup` to `main` via PR.** Then Phase 2 kickoff: create the `EpcVendorManagement` solution (net8.0) and begin the Department CRUD vertical slice.

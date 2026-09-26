# CURRENT_STATUS

> Read this file first in every session. Update it after every meaningful work session.

**Last updated:** 2026-09-26

## Current Phase

**Phase 1 — PARTIALLY COMPLETE.** The verified warm-up covers domain classes, LINQ, async/await, and xUnit; the roadmap's `IApprovable` interface exercise remains outstanding. Phase 2 has not started. (see ROADMAP.md)

## Current Objective

Complete the remaining Phase 1 `IApprovable` interface exercise and verify it before starting Phase 2. Phase 2 will create the `EpcVendorManagement` solution and begin the Department vertical slice in the order defined by the roadmap.

**Interview timeline: expected within the week** — Phase 2 is the machine-test rehearsal; highest-value work.

**Verified warm-up checkpoint (2026-07-14):** retarget to `net8.0` confirmed in both csproj files, tests re-passed 3/3 on net8.0, `fix: retarget warm-up projects to net8.0` pushed, and `feature/csharp-warmup` merged to `main` (PR #2). This checkpoint did not include the roadmap's interface exercise.

## Completed

- **Phase 1, step 1 (2026-07-14, verified by Saurabh):** docs PR merged to `main`; `feature/csharp-warmup` branch created; `src/Warmup` console project created (`dotnet new console` → "created successfully", `dotnet run` → `Hello, World!`); top-level statements explained. Visual Studio "ASP.NET and web development" workload confirmed installed — Phase 2 fully unblocked.
- **Phase 1, step 2 (2026-07-14, verified by Saurabh):** `Vendor` class written with enum category, getter-only `VendorCode`, nullable `ContactEmail`, constructor; used from `Program.cs` with a `List<Vendor>`, collection initializer, and string interpolation — correct filtered output confirmed. CS8618 nullable warning deliberately triggered and understood; encapsulation reasoning for the getter-only property articulated correctly. Concepts covered: properties, constructors, enums, nullable reference types, `var`, string interpolation.
- **Phase 1, step 3 (2026-07-14, verified by Saurabh):** LINQ `Where`/`OrderBy`/`Select`/`ToList`/`FirstOrDefault` working in `Program.cs` with correct output; deferred-execution experiment run with a **correct written prediction** (vendor added after query definition appeared in results); "when does LINQ execute" answered correctly. Concepts covered: lambdas, LINQ method syntax, projection, deferred execution, `First` vs `FirstOrDefault`, ternary operator, `is null`.
- **Phase 1, step 4 (2026-07-14, verified by Saurabh):** `Employee` and `Project` classes added; `GroupBy` (vendors per category) and `Join` (active projects with manager) produce correct output; both SQL twins hand-written correctly (GROUP BY with WHERE; INNER JOIN with alias + ON); orphan-project experiment answered fully (INNER drops it, LEFT JOIN keeps it with NULL manager). Concepts covered: `GroupBy`/`IGrouping`, aggregates, `Join`, FK-by-convention, object initializers, WHERE vs HAVING.
- **Phase 1, step 5 checkpoint (2026-07-14, verified by Saurabh):** `IsActive` encapsulated behind `Deactivate()` (CS0272 compile error experienced and fixed — encapsulation enforced by compiler); async `VendorRegistry.LoadVendorsAsync` awaited from top-level statements (visible latency observed); `tests/Warmup.Tests` xUnit project created and referenced; **3/3 tests passing** (`Test summary: total: 3, failed: 0, succeeded: 3`). Concepts covered: async/await, `Task<T>`, top-level await, `[Fact]`, Arrange-Act-Assert, async tests. The roadmap's interface exercise remains outstanding.
- **Finding from step 5 build output:** machine has the **.NET 10 SDK** (10.0.301, via VS 2026); templates initially created Warmup projects targeting `net10.0`. Decision + completed retarget to `net8.0` are recorded in DECISIONS.md #005.

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

- No implementation work is currently in progress. The remaining Phase 1 interface exercise is the next implementation task.

## Known Issues / Blockers

- Phase 1 cannot be marked complete until the roadmap's `IApprovable` interface exercise is implemented and verified. This is a scope item, not an environment blocker.

## Tests

- Passing: 3 xUnit warm-up tests covering vendor defaults, deactivation, and asynchronous vendor loading.
- Failing: none.
- Untested/unimplemented areas: the ASP.NET Core MVC application, persistence, Identity, and business workflows. Those capabilities remain **planned**, not built.

## Important Commands

```
dotnet --version        # verify SDK (expect 8.x)
dotnet run --project src/Warmup
dotnet test tests/Warmup.Tests
git status              # check working tree before any change
```

(Real build/run/test commands arrive with Phase 2.)

## Recent Decisions

- DECISIONS.md #001 — Start with a single ASP.NET Core MVC project + one test project; split into layers only when justified.
- DECISIONS.md #002 — First workflow will be the Vendor Registration Request.
- Roadmap ordering rationale is in ROADMAP.md's preamble.

## Git State

- Public default branch: `main`.
- Phase 1 warm-up work was merged through PR #2.

## Recommended Next Task

Complete and verify the remaining Phase 1 `IApprovable` interface exercise. After Phase 1 meets its roadmap definition, begin the Phase 2 MVC solution and Department CRUD vertical slice.

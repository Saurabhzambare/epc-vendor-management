# DECISIONS — Architecture Decision Records

Append-only. Never rewrite past entries; supersede them with a new entry.

---

## 001 — Single web project + test project (not multi-project clean architecture)

- **Date:** 2026-07-13
- **Context:** Repo is empty; owner is relearning C# with an interview possibly days away; brief suggests possible `Web/Core/Application/Infrastructure` split but requires justification either way.
- **Options considered:** (a) 4-project clean architecture; (b) single MVC project + test project with disciplined internal folders; (c) 2 projects (Web + Application).
- **Selected:** (b) — `src/EpcVendorManagement.Web` + `tests/EpcVendorManagement.Tests`.
- **Reason:** Maximizes learning of interview-relevant material (MVC, EF, SQL) per hour; matches machine-test conditions; separation of concerns still achieved via Services/interfaces/DI; extraction to libraries later is mechanical because logic is already behind interfaces.
- **Consequences:** Layering is enforced by convention, not compiler; AGENTS.md/CLAUDE.md rules and review carry that weight.
- **Risks:** Web project grows unwieldy → revisit trigger: when Services/ becomes hard to navigate or a second front end appears.
- **Revisitable:** Yes (explicitly expected around Phase 7+ if size justifies).

## 002 — First workflow: Vendor Registration Request

- **Date:** 2026-07-13
- **Context:** Brief lists four possible request types (vendor registration, purchase, service, project-resource); one must be first.
- **Options considered:** vendor registration vs. purchase request (the other strong candidate).
- **Selected:** Vendor registration request.
- **Reason:** Cleanest lifecycle demo with the fewest dependencies — it touches only Vendor on approval, whereas a purchase request wants projects, budgets, quantities, and possibly multi-level approval. Same interview value (state machine, roles, audit) at lower complexity.
- **Consequences:** Purchase/service requests move to backlog; the workflow service is written for this one workflow, not as a generic engine.
- **Risks:** Temptation to generalize prematurely — resisted by rule in ROADMAP Phase 5.
- **Revisitable:** Backlog items can add a second workflow after Phase 7.

## 003 — Four roles instead of six

- **Date:** 2026-07-13
- **Context:** Brief suggests up to six roles (adding Engineer, Finance Reviewer) but allows simplification.
- **Options considered:** six roles; four roles (Administrator, Project Manager, Procurement Officer, General Employee).
- **Selected:** Four roles.
- **Reason:** Every role must appear in the authorization matrix, seed data, tests, and manual checklists — six roles roughly doubles that surface without teaching any new concept. Engineer ≈ General Employee for permission purposes; Finance Reviewer adds a second approval stage best deferred with purchase requests.
- **Consequences:** Simpler, fully-testable role matrix.
- **Risks:** None significant; adding a role later is a seed + attribute change.
- **Revisitable:** Yes, alongside a second workflow type.

## 004 — Cookie-based Identity, not JWT

- **Date:** 2026-07-13
- **Context:** Owner's prior project used Django + JWT (SPA pattern); this app is server-rendered MVC.
- **Options considered:** Identity + cookies; JWT bearer tokens.
- **Selected:** ASP.NET Core Identity with cookie authentication.
- **Reason:** Cookies are the idiomatic, secure default for same-origin server-rendered apps (HttpOnly cookie + anti-forgery beats hand-rolled token storage in the browser); it is also what the JD's "ASP.NET MVC" implies. JWT remains an interview talking point, not an implementation.
- **Consequences:** Anti-forgery tokens required on POSTs including Ajax; documented in SECURITY.md.
- **Risks:** None for this architecture.
- **Revisitable:** Only if a separate API/SPA client is ever added.

## 005 — Target net8.0 even though the .NET 10 SDK is installed

- **Date:** 2026-07-14
- **Context:** Phase 1 build output revealed the machine carries the .NET 10 SDK (installed alongside Visual Studio 2026), so `dotnet new` created the Warmup projects targeting `net10.0`. The project brief and the job description specify .NET 8 (with .NET 6 as interview knowledge).
- **Options considered:** (a) retarget all projects to `net8.0` via `<TargetFramework>`; (b) stay on `net10.0`; (c) pin the SDK itself with a `global.json`.
- **Selected:** (a) — set `<TargetFramework>net8.0</TargetFramework>` in every project; no `global.json` for now.
- **Reason:** Interview credibility and JD alignment: the portfolio claims .NET 8 experience, so the code must actually target it. A newer SDK building an older target is normal, supported practice (SDK version ≠ target framework). A `global.json` pin adds friction without benefit while the 8.0.4xx SDK's presence isn't something we rely on.
- **Consequences:** New projects created by `dotnet new` must have their TargetFramework checked/edited — added as a standing rule; Phase 2 solution will be created targeting net8.0 explicitly.
- **Risks:** Forgetting the edit on a future project — mitigated by this record and CURRENT_STATUS notes.
- **Revisitable:** Yes — retarget upward deliberately when .NET 10 becomes the interview-relevant LTS story.

## 006 — Service tests run against SQLite in-memory, not the EF InMemory provider or DbContext mocks

- **Date:** 2026-07-16
- **Context:** Extracting `DepartmentService` (Phase 2, step 6) requires a test strategy for code whose main dependency is `ApplicationDbContext`. TESTING.md deferred this choice until now.
- **Options considered:** (a) mock `DbContext`/`DbSet`; (b) EF Core InMemory provider; (c) SQLite in-memory database; (d) real SQL Server (LocalDB) per test run.
- **Selected:** (c) SQLite in-memory via `Microsoft.EntityFrameworkCore.Sqlite`, one open connection per test.
- **Reason:** Mocking DbSet is brittle and tests the mock, not the query. The InMemory provider isn't relational — it ignores unique indexes and relational behavior, so our duplicate-name rules would pass tests while lying. SQLite is a real relational engine: LINQ translates, constraints (including the unique Name index) actually enforce, and it runs in milliseconds with no infrastructure. LocalDB is the higher-fidelity option but slower and Windows-tied; it remains the fallback for Phase 7 integration tests.
- **Consequences:** Tests create the schema with `EnsureCreated()` (not migrations); minor SQL Server/SQLite dialect differences are accepted and, where they ever matter, covered later by Phase 7 integration tests against real SQL Server.
- **Risks:** A query using a SQL-Server-only feature could pass/fail differently — mitigated by keeping provider-specific SQL out of services.
- **Revisitable:** Yes, at Phase 7 when the integration-test database strategy is implemented.

---

*Template for new entries: Title · Date · Context · Options considered · Selected approach · Reason · Consequences · Risks · Revisitable?*

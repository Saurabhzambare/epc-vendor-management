# ROADMAP — EPC Project & Vendor Management System

This roadmap was produced by analyzing the project requirements, the interview timeline (an interview call may come **within days**), the job description (C#, .NET 6/8, ASP.NET MVC, SQL Server, Bootstrap, jQuery, Ajax, Azure), Saurabh's background (strong Django/React, rusty C#/ASP.NET), and the current repository state (**empty — no application code exists yet**).

## How the plan was derived

**Two competing pressures:**

1. **Interview urgency** — the highest-value topics for an onsite interview + machine test are: C# fundamentals/OOP, LINQ, building a CRUD module in ASP.NET Core MVC with EF Core + SQL Server, validation, SQL joins, and jQuery/Ajax. A machine test almost always looks like "build a small CRUD form with validation and a related table."
2. **Portfolio depth** — approval workflows, role-based authorization, audit history, testing, Docker, and Azure make the project impressive, but they are worthless if the foundation is shaky.

**Therefore the plan front-loads exactly the skills a machine test requires (Phases 0–2), and defers everything that doesn't help in the first week (Docker, Azure, reports) to later phases.** Every early phase doubles as machine-test rehearsal.

**Key dependencies that fixed the ordering:**

- Nothing can be built before the environment works → Phase 0 first.
- CRUD requires the solution skeleton, EF Core, and SQL Server → Phase 2 before all features.
- Most screens need "who is logged in and what role do they have" → Identity (Phase 3) before domain features that depend on roles.
- The approval workflow needs Vendors and Users to exist → Phase 5 after Phases 3–4.
- Ajax/search/dashboard need data and screens to enhance → Phase 6 after Phase 4–5.
- Docker/Azure need a locally stable app → Phases 8–9 last.

**Risks identified:** interview may arrive before Phase 2 completes (mitigation: Phases 0–1 are interview-first); SQL Server install problems on the laptop (mitigation: containerized SQL Server as fallback documented in SETUP.md); scope creep across 9 entity types (mitigation: each phase has a strict definition of done); rusty C# slowing everything (mitigation: Phase 1 is a dedicated warm-up, not skipped).

**Complexity scale:** Small = a focused session or two · Medium = several sessions · Large = a week+ of sessions. No calendar promises.

---

## Phase 0 — Environment Verification & Repository Hygiene

- **Purpose:** Make the laptop capable of .NET development and the repo professional.
- **Why here:** Everything else is blocked without it; also the cheapest phase.
- **Business objective:** None (infrastructure).
- **Technical objective:** Verified installs of .NET 8 SDK, Visual Studio (ASP.NET workload), SQL Server + SSMS, Git, GitHub Desktop. Repo gets `.gitignore` and the docs foundation (this commit).
- **Learning objective:** Understand what each tool is for; basic Git/GitHub Desktop workflow.
- **Technologies:** .NET CLI, Visual Studio installer, SQL Server, Git.
- **Concepts to revise:** What the SDK vs. runtime is; what a solution vs. project is.
- **Tasks:** Run the verification checklist in `docs/SETUP.md`; install anything missing; confirm `dotnet --version` starts with 8; confirm SQL Server connection in SSMS.
- **Prerequisites / dependencies:** None.
- **Files affected:** Documentation only; later `.gitignore`.
- **Testing:** Each verification command's output checked against expected results in SETUP.md.
- **Documentation:** SETUP.md troubleshooting updated with any real errors hit.
- **Interview topics:** "What is the .NET SDK vs. runtime?", "Difference between .NET Framework and .NET Core/.NET 8?"
- **Practical exercise:** Clone the repo with GitHub Desktop, make a trivial branch, commit, push, open and close a PR — the full loop once.
- **Deliverables:** All tools verified; results recorded in CURRENT_STATUS.md.
- **Definition of done:** Every checklist command produces the expected output on the laptop.
- **Risks:** SQL Server/WSL install issues. **Common mistake:** installing Visual Studio without the "ASP.NET and web development" workload.
- **Complexity:** Small. **Next:** Phase 1.

## Phase 1 — C# Interview Fast-Track (console warm-up)

- **Purpose:** Rebuild C# fluency fast, using Django/Python knowledge as the bridge, before touching web code.
- **Why here:** The interview may arrive before the web app exists. C#/OOP/LINQ questions are near-certain; a console project is the fastest way to drill them. Skipping this and going straight to MVC means fighting syntax and framework at the same time.
- **Business objective:** None directly; the warm-up models EPC domain objects (Project, Vendor, Employee) so the work transfers.
- **Technical objective:** A small `warmup` console project exercising classes, interfaces, inheritance, polymorphism, collections, generics, exceptions, LINQ, async/await.
- **Learning objective:** Speak fluently about every OOP pillar and common LINQ methods with project-flavored examples.
- **Technologies:** C#, .NET CLI, xUnit (first tests).
- **Concepts to revise:** value vs. reference types, properties, constructors, `var`, nullable reference types, `List<T>`/`Dictionary<K,V>`, `Where/Select/OrderBy/GroupBy/First/FirstOrDefault`, `Task`, `async`/`await`.
- **Tasks:** Model `Vendor`, `Project`, `Employee` as classes; an `IApprovable` interface; LINQ queries over in-memory lists mirroring the SQL practice questions; a few xUnit tests.
- **Prerequisites:** Phase 0.
- **Files affected:** `src/Warmup/` (throwaway or kept as `samples/`), `tests/`.
- **Testing:** xUnit tests for the small classes — first Arrange/Act/Assert practice.
- **Documentation:** INTERVIEW_GUIDE.md filled with C#/OOP/LINQ Q&A as each concept is exercised.
- **Interview topics:** All four OOP pillars, interface vs. abstract class, `IEnumerable` vs. `IQueryable`, deferred execution, exception handling, async/await.
- **Practical exercises:** Timed: "model these 3 entities and write 5 LINQ queries in 30 minutes."
- **Deliverables:** Working console project + tests + a filled C# section of INTERVIEW_GUIDE.md.
- **Definition of done:** Can write a class with constructor, properties, interface implementation, and 5 common LINQ queries from memory.
- **Risks:** Spending too long polishing throwaway code. **Common mistake:** reading about LINQ instead of typing it.
- **Complexity:** Small–Medium. **Next:** Phase 2.

## Phase 2 — Solution Skeleton + First CRUD Module (Departments)

- **Purpose:** Create the real solution and complete one full vertical slice: Razor form → controller → service → EF Core → SQL Server and back.
- **Why here:** This IS the machine test. One entity (Department — the simplest, no dependencies) done end-to-end teaches routing, model binding, validation, migrations, and Razor in the most reusable way. Every later module copies this pattern.
- **Business objective:** Manage departments (create, edit, view, list, activate/deactivate).
- **Technical objective:** `EpcVendorManagement.Web` + `EpcVendorManagement.Tests` projects; EF Core `DbContext`; first migration; Department CRUD with server- and client-side validation; soft-delete via `IsActive`; Bootstrap layout.
- **Learning objective:** Trace an HTTP request through the entire stack and explain every file that participates.
- **Technologies:** ASP.NET Core MVC, EF Core, SQL Server, data annotations, Bootstrap, xUnit.
- **Concepts to revise:** MVC pattern, routing, `IActionResult`, model binding, `ModelState`, DI container, `DbContext`/`DbSet`, migrations, tag helpers, layout/partials.
- **Tasks (in order):** create solution/projects → wire DbContext + connection string via user secrets → Department entity + migration → list page → create form (POST/validation) → edit → details → deactivate → extract service layer → tests.
- **Prerequisites:** Phases 0–1.
- **Files affected:** `src/EpcVendorManagement.Web/` (Program.cs, Data/, Models/, Services/, Controllers/, Views/), `tests/`.
- **Testing:** Unit tests on DepartmentService rules (e.g., no duplicate names); manual checklist for each screen.
- **Documentation:** ARCHITECTURE.md request-flow section confirmed against real code; DATABASE.md first table; TESTING.md first checklist.
- **Interview topics:** MVC request lifecycle, DI lifetimes (scoped/transient/singleton), code-first migrations, validation pipeline, `Program.cs` middleware order.
- **Practical exercise:** Rebuild Department CRUD from scratch, timed at 60–90 minutes — the core machine-test rehearsal. Repeat until comfortable.
- **Deliverables:** Running app with working Department module and passing tests.
- **Definition of done:** All CRUD screens work against SQL Server; validation blocks bad input on client and server; at least 3 meaningful service tests pass; the timed rebuild exercise completed once.
- **Risks:** Connection-string problems; migration confusion. **Common mistakes:** putting logic in the controller "temporarily"; exposing entities directly to views instead of ViewModels for forms.
- **Complexity:** Medium. **Next:** Phase 3.

## Phase 3 — Authentication & Role-Based Authorization (Identity)

- **Purpose:** Add login/logout, roles, and protected pages with ASP.NET Core Identity.
- **Why here:** Nearly every later feature needs "current user + role." Adding Identity after many modules exist forces rework. Auth is also a guaranteed interview area.
- **Business objective:** Only authorized staff can access the system; admins manage users; role-based navigation.
- **Technical objective:** Identity with cookie auth; seeded roles (Administrator, Project Manager, Procurement Officer, Employee) and dev-only demo users; `[Authorize]`/role checks; login/logout/profile pages; audit fields base class (`CreatedBy/CreatedAt/ModifiedBy/ModifiedAt`).
- **Learning objective:** Authentication vs. authorization; cookies vs. JWT (bridge from the Django/JWT project); claims and roles.
- **Technologies:** ASP.NET Core Identity, EF Core, cookies, claims.
- **Concepts:** password hashing, claims principal, roles vs. policies, anti-forgery, `[Authorize]` vs. `[AllowAnonymous]`.
- **Tasks:** add Identity → Identity migration → seed roles/users → login/logout UI → protect Department module → role-based nav menu → audit-field saving via DbContext override.
- **Prerequisites:** Phase 2.
- **Testing:** Authorization tests (anonymous redirected; wrong role gets 403); manual login matrix per role.
- **Documentation:** SECURITY.md filled in with the real implementation; demo credentials in README.
- **Interview topics:** AuthN vs. AuthZ, how cookie auth works, where passwords are stored and how they're hashed, CSRF and anti-forgery tokens.
- **Practical exercise:** Add a new role and a page only it can see, unaided.
- **Deliverables:** Working login with roles; protected pages; seeded demo users.
- **Definition of done:** Role matrix verified manually + authorization tests pass.
- **Risks:** Fighting Identity scaffolding UI. **Common mistake:** confusing 401 vs. 403; checking roles in views but not on the server action.
- **Complexity:** Medium. **Next:** Phase 4.

## Phase 4 — Core Domain: Employees, Vendors, Projects, Assignments

- **Purpose:** Build the main business entities and their relationships.
- **Why here:** Depends on the CRUD pattern (Phase 2) and roles (Phase 3); required by the workflow (Phase 5).
- **Business objective:** Maintain employees (linked to departments), vendors (with categories/status), projects (manager, dates, budget, status, milestones), and assign employees/vendors to projects without duplicates.
- **Technical objective:** One-to-many (Department→Employees, Manager→Projects), many-to-many via join entities (ProjectEmployee, ProjectVendor), enums for statuses, seed data, business-rule validation in services (e.g., duplicate assignment blocked, end date ≥ start date).
- **Learning objective:** EF Core relationship mapping and the SQL it produces; eager loading with `Include`; avoiding N+1.
- **Technologies:** EF Core relationships/Fluent API where needed, LINQ, Bootstrap tables/forms, select lists.
- **Tasks (order):** Employees → Vendors → Projects → Milestones → assignments (both) → dropdown population via ViewModels → seed data.
- **Prerequisites:** Phases 2–3.
- **Testing:** Service tests for assignment rules and date/budget validation; manual checklists per module.
- **Documentation:** DATABASE.md ER diagram + relationship docs; API_AND_WORKFLOWS.md CRUD flows.
- **Interview topics:** 1-to-many vs. many-to-many in EF Core, navigation properties, `Include`/`ThenInclude`, N+1 problem, SQL joins equivalent to the LINQ used.
- **Practical exercises:** Write the raw SQL for 5 of the app's LINQ queries; timed "add a related entity with a dropdown" drill.
- **Deliverables:** Four modules + assignments working with realistic seed data.
- **Definition of done:** All relationships enforce integrity (FKs + service rules); duplicate assignment provably blocked by a test.
- **Risks:** Scope creep (milestone documents, vendor documents can wait). **Common mistake:** loading entire tables then filtering in memory instead of in the query.
- **Complexity:** Large. **Next:** Phase 5.

## Phase 5 — Approval Workflow (Vendor Registration Request)

- **Purpose:** The signature portfolio feature: a stateful request/approval workflow.
- **Why here:** Needs users/roles (3) and vendors (4). Chosen first workflow type: **vendor registration request** — it has a clear lifecycle, only touches one downstream entity, and shows off authorization + state transitions.
- **Business objective:** An employee submits a vendor registration request; a Procurement Officer reviews; approval creates/activates the vendor; every transition is recorded with comments and timestamps.
- **Technical objective:** `VendorRequest` entity + `RequestStatus` enum (Draft, Submitted, Under Review, Approved, Rejected, Returned for Changes, Cancelled); a workflow service that owns the legal-transition map; status-history table; role-restricted transition actions.
- **Learning objective:** State machines in services, transactional updates, authorization at the action level, designing for auditability.
- **Technologies:** EF Core transactions, enums, service-layer design, xUnit.
- **Tasks:** entity + history table → workflow service with transition validation → submit/review/approve/reject/return UI → comments + rejection reason → history timeline view.
- **Prerequisites:** Phases 3–4.
- **Testing:** This is the most unit-testable code in the app — every legal and illegal transition gets a test; authorization tests per transition.
- **Documentation:** API_AND_WORKFLOWS.md status diagram (Mermaid); DECISIONS.md entry for the workflow design.
- **Interview topics:** where business rules live and why, enum mapping in EF Core, transactions, policy vs. role authorization.
- **Practical exercise:** Add a new status ("On Hold") end-to-end, unaided.
- **Deliverables:** Complete working workflow with full history.
- **Definition of done:** Transition matrix fully covered by passing tests; illegal transitions impossible via UI **and** via forged POST.
- **Risks:** Over-engineering into a generic workflow engine — resist; hardcode this one workflow well. **Common mistake:** enforcing transitions only in the UI.
- **Complexity:** Large. **Next:** Phase 6.

## Phase 6 — jQuery/Ajax, Search, Filtering, Pagination, Dashboard

- **Purpose:** Deliberate practice of the job description's front-end stack, applied to real screens.
- **Why here:** Enhancement work needs existing screens and data (Phases 4–5). Kept as its own phase so jQuery/Ajax get focused attention rather than incidental use.
- **Business objective:** Fast, searchable lists; a dashboard showing active projects, pending approvals, vendors by status, recent activity.
- **Technical objective:** Ajax status updates (approve/reject without full reload), dependent dropdowns (Department→Employee), keyword search + filters + sorting + pagination on the big lists, loading/error states, confirmation dialogs, partial views returned from Ajax endpoints.
- **Learning objective:** jQuery selectors/events, `$.ajax` GET/POST with anti-forgery tokens, JSON vs. partial-HTML responses, progressive enhancement, when plain `fetch` would do.
- **Technologies:** jQuery, Ajax, JSON, partial views, Bootstrap components, JS debugging via browser dev tools.
- **Tasks:** search/filter/paginate vendors list → dependent dropdown on assignment form → Ajax approval actions with feedback states → dashboard cards + recent activity.
- **Prerequisites:** Phases 4–5.
- **Testing:** Manual checklists (happy path, server error, slow network via dev-tools throttling); integration tests for the Ajax endpoints.
- **Documentation:** UI_UX.md states and journeys; API_AND_WORKFLOWS.md Ajax endpoints; INTERVIEW_GUIDE.md JS/jQuery/Ajax sections.
- **Interview topics:** what Ajax is, `$(document).ready`, event delegation, how to send anti-forgery tokens with Ajax POSTs, JSON serialization.
- **Practical exercise:** Timed: add search + pagination to a list in 45 minutes; build a dependent dropdown in 30.
- **Deliverables:** Enhanced lists, dashboard, Ajax workflow actions.
- **Definition of done:** All checklist states (loading/success/error/empty) demonstrably handled.
- **Risks:** jQuery-spaghetti — keep JS per-page and small. **Common mistake:** forgetting server-side validation because client-side exists.
- **Complexity:** Medium. **Next:** Phase 7.

## Phase 7 — Hardening: Testing Depth, Security Review, Performance, Reports

- **Purpose:** Turn a working app into a defensible one.
- **Why here:** Needs the app to exist; deliberately before Docker/Azure so we containerize/deploy something solid.
- **Business objective:** Reliable reports (projects by status, vendors by category, pending requests, date-range approvals) with CSV export; trustworthy audit trail.
- **Technical objective:** Integration tests for critical flows; security pass against `docs/SECURITY.md` checklist (XSS, CSRF, overposting, open redirects, error handling); EF Core query logging reviewed for N+1/missing indexes; global error handling + structured logging polish; reports + CSV export.
- **Learning objective:** Reading EF Core SQL logs, using the debugger fluently (breakpoints, watch, call stack), writing integration tests with a test database, measuring before optimizing.
- **Prerequisites:** Phases 2–6.
- **Testing:** This phase IS testing. Regression checklist finalized in TESTING.md.
- **Documentation:** SECURITY.md production checklist executed and dated; TESTING.md tested/untested areas honest; DATABASE.md indexes documented.
- **Interview topics:** N+1 queries, eager loading vs. projection, indexes, debugging methodology, XSS/CSRF/SQL-injection defenses in ASP.NET Core.
- **Practical exercises:** Machine-test drills: "fix this broken LINQ query", "find why this page is slow", "prevent duplicate records under concurrency".
- **Deliverables:** Green test suite, dated security checklist, reports with CSV export.
- **Definition of done:** All critical flows covered by integration tests; security checklist has no open high items.
- **Risks:** Endless polishing. **Common mistake:** claiming performance wins without measurement — forbidden by project rules.
- **Complexity:** Medium–Large. **Next:** Phase 8.

## Phase 8 — Docker & Docker Compose

- **Purpose:** Containerize the app + SQL Server for reproducible dev.
- **Why here:** App is locally stable (rule: no Docker before local mastery, unless SQL Server install fails earlier — then a SQL container is pulled forward into Phase 0/2 as documented in SETUP.md).
- **Technical objective:** Dockerfile (multi-stage), docker-compose with app + SQL Server + volume, `.env.example`, connection strings via environment variables.
- **Learning objective:** image vs. container, ports, volumes, networks, logs, lifecycle — bridging from the Django/Docker project.
- **Testing:** Full manual regression against the containerized stack.
- **Documentation:** SETUP.md Docker section; README Docker instructions.
- **Interview topics:** what Docker solves, Dockerfile vs. compose, how the app finds the DB container.
- **Deliverables:** `docker compose up` yields a working app from a clean checkout.
- **Definition of done:** A fresh clone + compose + seed = usable app, verified.
- **Risks:** WSL2/virtualization issues on the laptop. **Common mistake:** baking secrets into images.
- **Complexity:** Medium. **Next:** Phase 9.

## Phase 9 — Azure Fundamentals & Portfolio Polish

- **Purpose:** Deployment fundamentals + final portfolio packaging.
- **Why here:** Last, per the rule that deployment waits for local stability; Azure knowledge is needed for interviews even if deployment stays optional.
- **Technical objective:** Understand (and optionally execute) App Service + Azure SQL deployment; GitHub Actions CI (build + test) once stable; screenshots, demo script, final README/PORTFOLIO_NOTES/resume bullets.
- **Learning objective:** App Service, Azure SQL, connection strings/app settings in Azure, deployment slots at a conceptual level, CI basics.
- **Testing:** Smoke test on any deployed instance; CI must run the test suite.
- **Documentation:** PORTFOLIO_NOTES.md finalized; README screenshots; honest deployment status (deployed vs. deployment-ready).
- **Interview topics:** Azure services in the JD, what CI/CD is, environment configuration in the cloud.
- **Deliverables:** CI badge, portfolio materials, optionally a live demo URL.
- **Definition of done:** Portfolio claims match verified reality, item by item.
- **Risks:** Azure costs — free tier or conceptual-only is acceptable and stated honestly.
- **Complexity:** Medium. **Next:** maintenance/enhancements backlog.

---

## Continuous threads (every phase)

- **Interview review:** every new concept gets an INTERVIEW_GUIDE.md entry the same session.
- **Testing:** no feature is "done" untested; TESTING.md updated as coverage changes.
- **Documentation:** CURRENT_STATUS.md after every session; DECISIONS.md for every significant choice.
- **Git:** feature branches, conventional commit messages, small reviewable diffs.

## Deferred / backlog (deliberately not scheduled)

Document uploads, vendor performance scoring, email notifications, ViewComponents beyond need, stored procedures (only if a genuine use appears), policy-based authorization beyond need, advanced caching, purchase/service request types beyond the first workflow.

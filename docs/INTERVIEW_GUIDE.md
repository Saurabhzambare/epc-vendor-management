# INTERVIEW_GUIDE

Living document: every important concept used in the project gets an entry **in the same session it is learned**, using the standard format below. Entries reference real project files once they exist.

**Standard entry format:** Interview concept → Simple meaning → Technical meaning → Where we used it → Likely interview question → Strong answer → Possible follow-ups → Common mistake → Practice task.

## Priority Topics for the Upcoming Interview (revise in this order)

1. **C# & OOP** — four pillars, interface vs. abstract class, value vs. reference types, properties, generics, collections, exception handling, async/await *(drilled in Phase 1)*
2. **LINQ** — Where/Select/OrderBy/GroupBy/First vs. FirstOrDefault, deferred execution, IEnumerable vs. IQueryable *(Phase 1)*
3. **ASP.NET Core MVC** — request lifecycle, routing, controllers/actions, model binding, validation, ViewModels, DI lifetimes, middleware order *(Phase 2)*
4. **EF Core + SQL Server** — DbContext/DbSet, migrations, relationships, Include, N+1, and hand-written SQL joins *(Phases 2/4)*
5. **Identity & security** — authN vs. authZ, cookies, roles/claims, CSRF/XSS/SQL injection *(Phase 3)*
6. **jQuery/Ajax/Bootstrap** — selectors, events, $.ajax, JSON, partial updates *(Phase 6, JD-critical)*
7. **.NET 6 vs .NET 8** — LTS versions; both current-style minimal hosting; .NET 8 adds performance, C# 12 features; expect "which version have you used and what changed" *(discussed in Phase 2)*

## Seed Q&A (starting set — grows continuously)

### Interview concept: The four pillars of OOP

**Simple meaning:** Encapsulation = keep data and rules together and hide internals. Inheritance = a class reuses another. Polymorphism = one call, different behaviors by type. Abstraction = expose *what*, hide *how*.

**Technical meaning:** Encapsulation via access modifiers and properties; inheritance via base/derived classes; polymorphism via virtual/override and interface dispatch; abstraction via abstract classes and interfaces.

**Where we used it:** Planned — audit-fields base class (inheritance), `IApprovable`/service interfaces (abstraction/polymorphism), private setters on workflow status (encapsulation). File references added in Phases 1–5.

**Likely question:** "Explain polymorphism with a real example."

**Strong answer:** "In my EPC project the controllers depend on service interfaces like `IVendorRequestService`. At runtime DI injects a concrete implementation; in tests I substitute a fake. The calling code is identical — that's polymorphism through interface dispatch, and it's what makes the business logic testable."

**Follow-ups:** interface vs. abstract class? method overloading vs. overriding? — Overloading: same name, different signatures, resolved at compile time. Overriding: derived class replaces a `virtual` member, resolved at runtime.

**Common mistake:** Reciting definitions with no example; confusing overloading with overriding.

**Practice task:** Model `Vendor` and `Employee` sharing an abstract `Party` base with one overridden method; call it polymorphically from a list.

### Interview concept: IEnumerable vs. IQueryable

**Simple meaning:** `IEnumerable` filters in memory after fetching; `IQueryable` builds up a query that the database executes.

**Technical meaning:** `IQueryable` composes an expression tree translated to SQL by the EF Core provider at enumeration (deferred execution). `IEnumerable` extension methods run as LINQ-to-Objects delegates in memory.

**Where we used it:** Planned — list/search services in Phases 2/6 compose `IQueryable` filters so paging happens in SQL (`OFFSET/FETCH`), not in memory.

**Likely question:** "Your vendors page is slow. It calls `GetAll().ToList()` then filters. Why is that a problem?"

**Strong answer:** "`ToList()` materializes the entire table into memory before filtering. Keeping the filter on `IQueryable` lets EF translate it to a SQL `WHERE`, so the database returns only the needed rows — with pagination that's the difference between fetching 20 rows and 20,000."

**Follow-ups:** What is deferred execution? When does the query actually run? (On enumeration — `ToList`, `foreach`, `First…`.)

**Common mistake:** Calling `.ToList()` early "to be safe."

**Practice task:** Write one search method twice — filter-before vs. filter-after `ToList()` — and inspect the generated SQL in the logs.

### Interview concept: Authentication vs. Authorization

**Simple meaning:** Authentication = who are you. Authorization = what may you do.

**Technical meaning:** Identity's authentication middleware builds a `ClaimsPrincipal` from the auth cookie; authorization middleware/filters evaluate `[Authorize]` requirements (roles/policies) against it. Unauthenticated → 401/redirect to login; unauthorized → 403.

**Where we used it:** Planned — Phase 3: cookie login, role-restricted controllers.

**Likely question:** "How does the server know the user is logged in on the second request?"

**Strong answer:** "At login, Identity writes an encrypted, signed cookie containing the user's claims. The browser sends it on every request; the authentication middleware decrypts it and reconstructs the user principal — no server-side session lookup is required for the basic case."

**Follow-ups:** Cookie vs. JWT — when each? Where do roles live? (As claims in the principal.)

**Common mistake:** Treating a hidden menu link as security; the action itself must be attributed.

**Practice task:** After Phase 3, demonstrate the 401-vs-403 difference with two curl/browser experiments.

## Question Banks (populated as phases complete)

- C# / OOP · LINQ · ASP.NET Core / MVC · EF Core · SQL Server · JavaScript / jQuery / Ajax · Bootstrap · Security · Performance · Docker · Azure · Git · Debugging scenarios

## Machine-Test Exercise Log

*Timed drills, attempts, and results recorded here (drill specs live in the roadmap phases). Model solutions only after an attempt.*

## Questions I Struggled With / Topics Requiring Revision

*Honest log, maintained continuously — this drives revision priorities.*

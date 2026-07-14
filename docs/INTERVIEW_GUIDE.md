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

### Interview concept: Top-level statements

**Simple meaning:** Since C# 9, a console app's entry file can be plain statements — the compiler writes the wrapping `Program` class and `Main` method for you.

**Technical meaning:** The compiler synthesizes `internal class Program { private static void Main(string[] args) { ... } }` around the top-level code; only one file per project may use them. Older projects use the explicit form.

**Where we used it:** `src/Warmup/Program.cs` (Phase 1, 2026-07-14).

**Likely question:** "Where is the Main method in your console app?"

**Strong answer:** "It's generated — the file uses top-level statements, so the compiler wraps my code in a synthesized `Program.Main`. In .NET Framework or older codebases I'd write `static void Main(string[] args)` explicitly; both compile to the same entry point."

**Follow-ups:** Can two files have top-level statements? (No — compile error.) How do you read command-line args? (`args` is available implicitly.)

**Common mistake:** Thinking the program "has no Main" or that top-level statements are a scripting mode — it's still a normal compiled program.

**Practice task:** Convert Warmup's `Program.cs` to an explicit `Main` and back; confirm both run identically.

### Interview concept: Properties (auto-implemented)

**Simple meaning:** A property looks like a field from outside but is really a pair of get/set methods — the class keeps control over its data.

**Technical meaning:** `public string Name { get; set; }` compiles to a hidden backing field plus `get_Name`/`set_Name` accessors. Variants: `{ get; }` (settable only in constructor — immutable after construction), `{ get; private set; }` (class controls writes), `{ get; init; }` (settable at object initialization).

**Where we used it:** `Vendor` class in `src/Warmup` (Phase 1); later every EF Core entity maps properties to columns.

**Django comparison:** Roughly where Django model fields (`models.CharField(...)`) sit, but Django fields are ORM column descriptors; C# properties are a plain language feature that EF Core *chooses* to map to columns in Phase 4.

**Likely question:** "Why use a property instead of a public field?"

**Strong answer:** "A property keeps the class in control: I can add validation or make the setter private later without breaking callers, data binding and EF Core work against properties, and `{ get; }`-only properties give me immutability. A public field commits me to raw access forever."

**Follow-ups:** What does the compiler generate? Difference between `{ get; }` and `{ get; init; }`?

**Common mistake:** Calling a property "a variable"; not knowing accessors are methods under the hood (which is what makes encapsulation real).

**Practice task:** Give `Vendor.IsActive` a private setter and expose `Deactivate()` — encapsulating the state change.

### Interview concept: Nullable reference types (`string?`)

**Simple meaning:** By default the compiler treats reference types as never-null; adding `?` declares "this may legitimately be null," and the compiler warns wherever you forget to check.

**Technical meaning:** A compile-time annotation + flow analysis feature (`<Nullable>enable</Nullable>` in the .csproj, on by default in .NET 6+ templates). No runtime difference — it's static analysis against `NullReferenceException`, C#'s answer to the "billion-dollar mistake."

**Where we used it:** `Vendor.ContactEmail` is `string?` (optional); `Name` is non-nullable and must be set in the constructor.

**Django comparison:** Like `null=True` on a model field, but enforced by the compiler across *all* code, not just the database schema — closer in spirit to Python type hints with `Optional[str]` checked by mypy, except it's built into the language.

**Likely question:** "What does `string?` mean and what problem does it solve?"

**Strong answer:** "It marks a reference that may be null. With nullable reference types enabled, everything else is assumed non-null, and the compiler warns on unguarded dereferences of nullable ones — so whole categories of `NullReferenceException` are caught at compile time instead of in production."

**Follow-ups:** Runtime behavior difference? (None — warnings only.) What is the null-conditional operator `?.` / null-coalescing `??`?

**Common mistake:** Confusing `string?` (annotation on a reference type) with `int?` (`Nullable<int>`, a genuinely different value-type wrapper).

**Practice task:** Remove a required constructor assignment and read the compiler warning; fix it three ways (constructor, default value, make it nullable) and explain when each is right.

## Question Banks (populated as phases complete)

- C# / OOP · LINQ · ASP.NET Core / MVC · EF Core · SQL Server · JavaScript / jQuery / Ajax · Bootstrap · Security · Performance · Docker · Azure · Git · Debugging scenarios

## Machine-Test Exercise Log

*Timed drills, attempts, and results recorded here (drill specs live in the roadmap phases). Model solutions only after an attempt.*

## Questions I Struggled With / Topics Requiring Revision

*Honest log, maintained continuously — this drives revision priorities.*

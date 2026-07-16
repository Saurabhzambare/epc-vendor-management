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

### Interview concept: Lambda expressions

**Simple meaning:** A tiny unnamed function written inline: `v => v.IsActive` reads "given a vendor v, return whether it's active."

**Technical meaning:** `(parameters) => expression-or-block`, compiled to a delegate (e.g. `Func<Vendor, bool>`) — or to an expression tree when the receiver is `IQueryable`, which is how EF Core later translates the same syntax to SQL.

**Where we used it:** every LINQ call in `src/Warmup/Program.cs` (Phase 1, step 3).

**Django comparison:** Python's `lambda v: v.is_active`, but statically typed; the QuerySet analogy is that `filter(is_active=True)` and `Where(v => v.IsActive)` play the same role.

**Likely question:** "What is `v => v.IsActive` actually?"

**Strong answer:** "A lambda — an inline anonymous function the compiler turns into a `Func<Vendor, bool>` delegate that `Where` calls per element. Against an `IQueryable` the same lambda becomes an expression tree instead, which is what lets EF Core translate it into a SQL WHERE clause."

**Follow-ups:** Delegate vs. expression tree? Can lambdas capture variables? (Yes — closures.)

**Common mistake:** Calling it "an arrow function that's just syntax" without knowing delegates/expression trees sit underneath — the follow-up question exists to catch that.

**Practice task:** Write the same filter three ways: lambda, separate named method, and (for contrast) a `foreach`/`if` loop.

### Interview concept: LINQ core methods & deferred execution

**Simple meaning:** LINQ lets you query any collection like a database: filter (`Where`), sort (`OrderBy`), reshape (`Select`). The query doesn't run when you write it — it runs when you loop over it.

**Technical meaning:** Extension methods on `IEnumerable<T>` composing lazily; enumeration (`foreach`, `ToList()`, `Count()`, `First...`) triggers execution. Each enumeration re-executes the query against the *current* data.

**Where we used it:** `src/Warmup/Program.cs` — active-vendor query + the deferred-execution experiment (adding a vendor after defining the query, before enumerating it).

**Django comparison:** QuerySets are also lazy (`.filter()` builds, iteration executes) — the concept transfers directly; the difference is LINQ works over in-memory objects too, not only the ORM.

**Likely question:** "When does a LINQ query actually execute?"

**Strong answer:** "On enumeration, not definition. In my warm-up I defined a query over the vendor list, added a vendor afterwards, and the new vendor appeared in the results — proof the query ran at the `foreach`, against current data. `ToList()` is how you force immediate execution and snapshot the results."

**Follow-ups:** What happens if you enumerate twice? (Runs twice — with EF Core, two database hits.) Method syntax vs. query syntax? (Same thing; method syntax is what most teams use.)

**Common mistake:** Calling `.ToList()` immediately everywhere "to be safe," which with EF Core fetches whole tables before filtering.

**Practice task:** Predict, then verify, the output of enumerating one query before and after mutating the source list.

### Interview concept: First vs. FirstOrDefault

**Simple meaning:** Both grab the first match; `First` throws an exception if nothing matches, `FirstOrDefault` quietly returns null (or the type's default).

**Technical meaning:** `First(predicate)` throws `InvalidOperationException` on no match; `FirstOrDefault(predicate)` returns `default(T)` — null for reference types — so its result should be typed nullable (`Vendor?`) and checked. `Single`/`SingleOrDefault` additionally throw if *more than one* matches.

**Where we used it:** vendor lookup by code in `src/Warmup/Program.cs`, with a null check — this is where nullable reference types and LINQ meet.

**Likely question:** "Difference between First and FirstOrDefault, and when do you use each?"

**Strong answer:** "`First` throws if there's no match — right when absence is a bug and I want to fail loudly. `FirstOrDefault` returns null — right when absence is a normal case I'll handle, like a user searching for a vendor code that may not exist. The compiler then reminds me to null-check because the result is `Vendor?`."

**Follow-ups:** What does `Single` add? What's `default(T)` for an `int`? (0 — which is why `FirstOrDefault` on value types can be a trap.)

**Common mistake:** Using `FirstOrDefault` everywhere and forgetting the null check — trading a clear exception for a `NullReferenceException` two lines later.

**Practice task:** Look up a nonexistent vendor code with both methods; observe the exception vs. the null, and handle the null path properly.

### Interview concept: GroupBy and aggregates

**Simple meaning:** `GroupBy` sorts items into buckets by a key (vendors by category); aggregates (`Count`, `Sum`, `Max`) then summarize each bucket.

**Technical meaning:** `GroupBy(v => v.Category)` yields `IEnumerable<IGrouping<VendorCategory, Vendor>>` — each `IGrouping` has a `.Key` and is itself enumerable. SQL equivalent: `SELECT Category, COUNT(*) FROM Vendors GROUP BY Category`.

**Where we used it:** active-vendors-per-category in `src/Warmup/Program.cs` (Phase 1, step 4); reappears in the Phase 6 dashboard and the DATABASE.md SQL practice set (Q2).

**Django comparison:** `Vendor.objects.values('category').annotate(n=Count('id'))` — same concept; LINQ's version also works on plain in-memory lists.

**Likely question:** "How would you count vendors per category — in LINQ and in SQL?"

**Strong answer:** "LINQ: `vendors.GroupBy(v => v.Category)` then `group.Count()` per group — each group carries its `Key` and its members. SQL: `SELECT Category, COUNT(*) FROM Vendors GROUP BY Category`. And if I only want big categories, SQL filters groups with `HAVING`, not `WHERE` — `WHERE` runs before grouping, `HAVING` after."

**Follow-ups:** WHERE vs. HAVING? What type does GroupBy return? Can you group by two keys? (Yes — anonymous type key.)

**Common mistake:** Not knowing `IGrouping` has both a `Key` and the elements; in SQL, putting an aggregate condition in `WHERE`.

**Practice task:** Group vendors by category *and* active flag using an anonymous-type key; write the matching SQL.

### Interview concept: LINQ Join vs. SQL INNER JOIN

**Simple meaning:** A join matches rows from two collections/tables where keys line up — projects matched to the employee who manages them.

**Technical meaning:** `outer.Join(inner, o => o.Key, i => i.Key, (o, i) => result)` — equi-join over key selectors, like SQL `INNER JOIN ... ON`. Non-matches drop out (a project whose ManagerId matches no employee simply disappears — same as SQL). In EF Core, navigation properties + `Include` usually replace explicit `Join`, but interviews test the SQL form.

**Where we used it:** projects-with-managers in `src/Warmup/Program.cs` (Phase 1, step 4) = DATABASE.md practice Q1.

**Django comparison:** you rarely write joins in Django — `project.manager.name` walks a FK and the ORM emits the JOIN. EF Core navigation properties do the same in Phase 4; the warm-up writes the join by hand precisely because interviews and machine tests ask for raw SQL joins.

**Likely question:** "Write a query returning each active project with its manager's name."

**Strong answer:** "SQL: `SELECT p.Name, e.Name FROM Projects p INNER JOIN Employees e ON p.ManagerId = e.Id WHERE p.Status = 'Active'`. LINQ mirrors it with `Join` on the same keys — or, with EF Core entities, I'd expose a `Manager` navigation property and just project `p.Manager.Name`, letting EF generate that same JOIN."

**Follow-ups:** INNER vs. LEFT join — what happens to a project with no matching manager? (INNER drops it; LEFT keeps it with NULLs — LINQ equivalent is `GroupJoin`/`DefaultIfEmpty`.)

**Common mistake:** Only knowing joins through the ORM and freezing when asked for the SQL; mixing up which side LEFT preserves.

**Practice task:** Add a project whose ManagerId matches nobody; show it vanishes from the join, then write the SQL LEFT JOIN that would keep it.

### Interview concept: async/await and Task

**Simple meaning:** `await` says "this will take a while (network, database, disk) — don't sit idle, come back when it's done." The thread is freed to do other work in the meantime.

**Technical meaning:** `Task`/`Task<T>` represents an in-flight operation (like a JS Promise). `async` enables `await` in a method; at each `await`, the compiler rewrites the rest of the method into a continuation that resumes on completion — no thread is blocked while the I/O is pending. Async is about *not wasting threads during I/O*, not about parallelism — no extra thread is created by `await`.

**Where we used it:** `VendorRegistry.LoadVendorsAsync` in `src/Warmup` (Phase 1, step 5), awaited from top-level statements and from an async xUnit test. From Phase 2 on, every EF Core call (`ToListAsync`, `SaveChangesAsync`) uses it.

**Django comparison:** Python's `async def`/`await` (asyncio) is a direct cognate. Classic Django views are sync-per-worker; in ASP.NET Core async is idiomatic and the default — a blocked thread is a wasted request slot under load.

**Likely question:** "Why make a controller action async? Does it make the request faster?"

**Strong answer:** "No — the individual request takes the same time. It makes the *server* scale better: while the database call is pending, `await` returns the thread to the pool to serve other requests instead of blocking. Under load that's the difference between handling and queueing requests. That's why EF Core calls in my controllers are `await`ed `ToListAsync`/`SaveChangesAsync`."

**Follow-ups:** `Task` vs `Task<T>` vs `void`? (Never `async void` except event handlers — exceptions become uncatchable.) What does the compiler generate? (A state machine.) Difference from multithreading?

**Common mistake:** Believing `async` = faster or = multithreaded; calling `.Result`/`.Wait()` on a Task (blocks, can deadlock — defeats the point).

**Practice task:** Time two awaited `Task.Delay(500)` calls run sequentially vs. with `Task.WhenAll`; explain the ~500 ms difference.

### Interview concept: Unit testing with xUnit (Arrange–Act–Assert)

**Simple meaning:** A unit test is a small program that proves one behavior of your code: set up the situation (Arrange), do the thing (Act), check the result (Assert).

**Technical meaning:** xUnit discovers `[Fact]` methods (and `[Theory]` + `[InlineData]` for parameterized cases) and runs them in isolation; `Assert.True/Equal/Null/Throws` verify outcomes. Async tests are `async Task` methods. Tests live in a separate project referencing the production code.

**Where we used it:** `tests/Warmup.Tests` (Phase 1, step 5) — default-state, behavior (`Deactivate`), and async tests for `Vendor`/`VendorRegistry`. The real suite grows from Phase 2 (`DepartmentService`).

**Django comparison:** `TestCase` methods with `self.assertEqual` — same discipline; xUnit favors plain classes + attributes over inheritance, and there's no implicit test database (integration tests set that up explicitly — Phase 7).

**Likely question:** "What makes a good unit test?"

**Strong answer:** "It tests one behavior through the public surface, is isolated — no shared state, no real network or database — fails for exactly one reason, and its name states the scenario and expectation, like `Deactivate_SetsIsActiveFalse`. I structure each test Arrange-Act-Assert so the story is readable at a glance."

**Follow-ups:** `[Fact]` vs `[Theory]`? Unit vs integration test? What do you mock and why?

**Common mistake:** Testing private internals instead of observable behavior; multiple unrelated asserts in one test; tests that pass without asserting anything meaningful.

**Practice task:** Convert the `Deactivate` test into a `[Theory]` covering an already-inactive vendor too.

### Interview concept: Middleware pipeline and why order matters

**Simple meaning:** Every request walks through a fixed line of checkpoints (middleware) in the order they're registered — and the response walks back out through the same line in reverse. A checkpoint can pass the request along, change it, or stop it right there.

**Technical meaning:** `Program.cs` composes delegates via `app.Use...`; each middleware receives the request and a `next` delegate. Order defines semantics: `UseAuthentication` must precede `UseAuthorization` (you can't check permissions for an unidentified user); the exception handler is registered first so its try/catch wraps *everything after it*; `UseStaticFiles` sits early to short-circuit CSS/JS requests before routing/auth work is wasted on them.

**Where we used it:** `src/EpcVendorManagement.Web/Program.cs` (Phase 2); order verified against the template.

**Django comparison:** the `MIDDLEWARE` list — same onion model (request top-down, response bottom-up), same order-sensitivity (`AuthenticationMiddleware` before permission checks). The concept transfers 1:1; ASP.NET Core just expresses it as code instead of a settings list.

**Likely question:** "Why must UseAuthentication come before UseAuthorization?"

**Strong answer:** "Middleware runs in registration order. Authentication reads the cookie and builds the user principal; authorization then evaluates that principal against the endpoint's requirements. Reversed, authorization would evaluate an anonymous user and reject everyone. Same logic puts the exception handler first — it can only catch exceptions from middleware registered after it."

**Follow-ups:** What happens if middleware doesn't call `next`? (Short-circuit — static files does this on a hit.) Where do controllers fit? (The endpoint execution at the end of the pipeline.) Difference between `Use`, `Run`, `Map`?

**Common mistake:** Treating the order as boilerplate to copy; not knowing responses traverse the pipeline in reverse.

**Practice task:** Move `UseStaticFiles` after `UseAuthorization` in a throwaway branch and reason about (then observe) what changes for a CSS request.

### Interview concept: DbContext, DbSet, and code-first migrations

**Simple meaning:** `DbContext` is your session with the database; each `DbSet<T>` property is a table you can query with LINQ. Migrations are versioned scripts EF generates from your entity classes so the database schema follows your code.

**Technical meaning:** `DbContext` combines unit-of-work (change tracking + `SaveChangesAsync` as one transaction) and repository-style access (`DbSet<T>`). `dotnet ef migrations add X` diffs the current model against the last snapshot and emits `Up()`/`Down()`; `dotnet ef database update` applies pending migrations, recorded in the `__EFMigrationsHistory` table.

**Where we used it:** `Data/ApplicationDbContext.cs`, `InitialCreate` migration creating `Departments` (Phase 2).

**Django comparison:** `makemigrations`/`migrate` — nearly identical mental model, including the history table (`django_migrations`). Difference worth naming: EF Core migrations are C# classes you can read and edit *before* applying, and there's no automatic per-app grouping.

**Likely question:** "How do you change the database schema in EF Core?"

**Strong answer:** "Code-first: I change the entity or DbContext configuration, run `dotnet ef migrations add DescriptiveName`, review the generated Up/Down methods, then `dotnet ef database update`. EF tracks applied migrations in `__EFMigrationsHistory`, so each environment applies only what it's missing. Applied migrations are never edited — schema mistakes get a new migration."

**Follow-ups:** Why never edit an applied migration? (Other environments already ran the old version — the snapshot and history diverge.) What's in the ModelSnapshot? How do you roll back? (`database update PreviousMigrationName`.)

**Common mistake:** Editing or deleting applied migrations; treating generated migrations as unreviewable magic.

**Practice task:** Add a column to Department via a second migration; read the generated `Up()` before applying it.

### Interview concept: Controllers, actions, and convention-based routing

**Simple meaning:** A controller is the class that answers a group of related URLs; each public method (action) answers one. ASP.NET Core finds the right method from the URL by naming convention: `/Departments` → `DepartmentsController.Index()`.

**Technical meaning:** The default route template `{controller=Home}/{action=Index}/{id?}` tokenizes the path, appends the `Controller` suffix, and dispatches. Actions return `IActionResult` — an abstraction over "what kind of response": `View(model)`, `RedirectToAction(...)`, `NotFound()`, `Json(...)`. Dependencies (like `ApplicationDbContext`) arrive via constructor injection from the DI container, scoped per request.

**Where we used it:** `Controllers/DepartmentsController.cs` (Phase 2, step 3) — constructor-injected DbContext, async `Index` action returning a typed view.

**Django comparison:** a controller ≈ a group of related Django views; `IActionResult` ≈ returning `HttpResponse`/`render()`/`redirect()`. Big difference: Django routes are *explicit* in `urls.py`; MVC's are *conventional* — no per-action registration, which is why naming matters.

**Likely question:** "Walk me through what happens when a request hits `/Departments`."

**Strong answer:** "The middleware pipeline runs; routing matches the default template, resolving controller=Departments, action=Index. The DI container constructs `DepartmentsController`, injecting a request-scoped `ApplicationDbContext`. `Index` awaits an EF Core query — `ToListAsync` releases the thread while SQL Server works — then returns `View(departments)`, and Razor renders the strongly-typed view inside the shared layout back through the pipeline."

**Follow-ups:** Why return `IActionResult` instead of the view type? (One action can return different results — view, redirect, 404.) What DI lifetime does DbContext use and why? (Scoped — one per request, matching a unit of work.)

**Common mistake:** Hunting for a urls.py-style registration and concluding routing is magic; putting queries/business logic permanently in the controller (ours moves to a service in a later step — deliberately staged).

**Practice task:** Add a `Details(int id)` action and work out its URL without running the app, then verify.

### Interview concept: Model binding and validation (ModelState)

**Simple meaning:** When a form posts, MVC automatically fills your C# object's properties from the form fields by matching names — that's model binding. It then checks the object against its validation attributes; `ModelState.IsValid` tells you if anything failed.

**Technical meaning:** Binders map request data (form fields, route values, query string) onto action parameters by name. Data annotations (`[Required]`, `[StringLength]`) are evaluated during binding into the `ModelState` dictionary — per-field errors included. Client-side, jQuery Unobtrusive Validation reads the same attributes (rendered as `data-val-*`) for instant feedback; the server check remains authoritative because clients can bypass JS.

**Where we used it:** `DepartmentsController.Create(DepartmentFormViewModel model)` + `_ValidationScriptsPartial` (Phase 2, step 4). Custom rule added via `ModelState.AddModelError` for duplicate names.

**Django comparison:** Django Forms — `form.is_valid()` ≈ `ModelState.IsValid`, field validators ≈ data annotations, `form.errors` ≈ `ModelState`. Near 1:1 mental model.

**Likely question:** "How does validation work in ASP.NET Core MVC — client and server?"

**Strong answer:** "One set of attributes on the ViewModel drives both: the tag helpers emit data-val attributes that jQuery unobtrusive validation enforces in the browser for UX, and model binding re-evaluates the same rules on the server into ModelState. I always check `ModelState.IsValid` and redisplay the form with errors when it fails — client validation is a convenience, never a security boundary. Business rules the attributes can't express, like duplicate names, I add with `ModelState.AddModelError`, and the database constraint backs the whole thing."

**Follow-ups:** What if JS is disabled? Where do error messages render? (`asp-validation-for` spans / validation summary.) How do you validate across two fields?

**Common mistake:** Trusting client validation alone; forgetting to return the same view with the model so the user's input survives a failed post.

**Practice task:** Submit the form with dev-tools-disabled JS and confirm the server still rejects bad input.

### Interview concept: ViewModels, overposting, and Post-Redirect-Get

**Simple meaning:** The form binds to a small class holding only what the form should touch (ViewModel), never the database entity — so nobody can smuggle extra fields in. After a successful POST, redirect instead of rendering, so refresh doesn't resubmit.

**Technical meaning:** Binding a POST directly to an entity lets an attacker add form fields for any entity property (`IsActive=false`, `Id=7`) — **overposting/mass assignment**. A ViewModel is an allowlist by construction. **PRG:** successful POSTs end in a 303-style redirect to a GET; one-shot feedback crosses the redirect via `TempData` (cookie/session-backed, survives exactly one request).

**Where we used it:** `DepartmentFormViewModel` (no Id, no IsActive) + `RedirectToAction(nameof(Index))` + `TempData["Success"]` (Phase 2, step 4).

**Django comparison:** overposting ≈ the risk behind careless `ModelForm` with `fields="__all__"`; PRG ≈ Django's standard redirect-after-valid-form; `TempData` ≈ the messages framework.

**Likely question:** "Why not bind the form straight to your EF entity?"

**Strong answer:** "Overposting: model binding fills any matching property, so a crafted request could set fields the form never showed — IsActive, or a foreign key. The ViewModel is an explicit allowlist of what users may submit, and it also carries the form's validation attributes without polluting the entity. In the action I map ViewModel → entity myself, so every settable field is a deliberate decision."

**Follow-ups:** What does refresh-after-POST do without PRG? (Browser re-submits — duplicate records.) TempData vs ViewBag lifetime?

**Common mistake:** `[Bind]` attribute band-aids instead of a real ViewModel; returning `View()` after a successful POST.

**Practice task:** Add a hidden `IsActive` field to the rendered form via dev tools, submit, and confirm the ViewModel ignores it.

## Question Banks (populated as phases complete)

- C# / OOP · LINQ · ASP.NET Core / MVC · EF Core · SQL Server · JavaScript / jQuery / Ajax · Bootstrap · Security · Performance · Docker · Azure · Git · Debugging scenarios

## Machine-Test Exercise Log

*Timed drills, attempts, and results recorded here (drill specs live in the roadmap phases). Model solutions only after an attempt.*

## Questions I Struggled With / Topics Requiring Revision

*Honest log, maintained continuously — this drives revision priorities.*

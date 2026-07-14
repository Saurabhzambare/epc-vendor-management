# PORTFOLIO_NOTES

**Honesty rule:** every claim below must be backed by verified, completed work. Items marked *(when built)* may not be used until the corresponding phase is done and tested. Never claim real users, a real company, deployment, or measured improvements that didn't happen.

## One-Line Description

Enterprise-style EPC project and vendor management system built with ASP.NET Core MVC, EF Core, and SQL Server. *(usable once Phase 2 is complete; feature claims grow per phase)*

## Short Description

An enterprise-style web application simulating how an engineering/procurement/construction company manages projects, employees, vendors, and approval workflows — built with ASP.NET Core MVC, Entity Framework Core, SQL Server, Bootstrap, jQuery, and Ajax, with role-based access control and xUnit tests. All companies and data are fictional.

## Detailed Description

*(Assembled from completed phases; drafted fully after Phase 5.)*

## Technologies Demonstrated (checked off only when true)

- [ ] C# / .NET 8 fundamentals and OOP (Phase 1)
- [ ] ASP.NET Core MVC CRUD with validation (Phase 2)
- [ ] EF Core code-first migrations against SQL Server (Phase 2)
- [ ] ASP.NET Core Identity, roles, cookie auth (Phase 3)
- [ ] Relational modeling: 1-many & many-many with join entities (Phase 4)
- [ ] Stateful approval workflow with audit history (Phase 5)
- [ ] jQuery/Ajax partial updates, search/filter/pagination, dashboard (Phase 6)
- [ ] xUnit unit + integration testing (Phases 2–7)
- [ ] Security hardening: CSRF/XSS/overposting/SQL-injection defenses (Phases 3–7)
- [ ] Docker & Docker Compose (Phase 8)
- [ ] CI with GitHub Actions; Azure deployment fundamentals (Phase 9)

## Resume Bullet Ideas *(activate only when the phase is done)*

- Built an enterprise-style EPC project and vendor management system using ASP.NET Core MVC, Entity Framework Core, and SQL Server, with role-based authorization via ASP.NET Core Identity. *(after Phase 3)*
- Designed a relational schema with one-to-many and many-to-many relationships, enforced with database constraints and EF Core code-first migrations. *(after Phase 4)*
- Implemented a multi-status approval workflow with validated state transitions, per-role permissions, and a full audit history, covered by xUnit tests. *(after Phase 5)*
- Developed interactive UI features with Bootstrap, jQuery, and Ajax, including partial page updates, dependent dropdowns, server-side search, filtering, and pagination. *(after Phase 6)*
- Containerized the application and SQL Server with Docker Compose for reproducible development environments. *(after Phase 8)*

## LinkedIn Project Description

*(Drafted after Phase 5, from verified bullets above.)*

## GitHub Repository Description

`Enterprise-style EPC project & vendor management system — ASP.NET Core MVC, EF Core, SQL Server, Identity, Bootstrap/jQuery/Ajax, xUnit. Fictional data; portfolio project.`

**Suggested topics:** `aspnet-core` `csharp` `dotnet` `entity-framework-core` `sql-server` `mvc` `bootstrap` `jquery` `portfolio-project`

**Recommendation:** rename the repository from `Hello-World` to `epc-vendor-management` (GitHub → Settings → Rename; old URLs redirect).

## Screenshots to Capture (as features complete)

Login page · dashboard · project list with search/filters/pagination · project details with assignments · vendor request approval screen with status history timeline · validation errors on a form · role-based navigation differences (admin vs. employee).

## Demo-Video / Interview Demonstration Plan *(after Phase 6)*

1. Log in as Employee → submit a vendor registration request
2. Log in as Procurement Officer → review, comment, approve via Ajax (show no full reload)
3. Show the vendor now active + the status history trail
4. Show role enforcement: employee cannot open the approval action (403)
5. Open the code: controller → service → transition map → test that forbids the illegal transition
6. Show `dotnet test` passing

## Honest Limitations (keep current)

Fictional data; no production users; single-instance design; no real procurement/ERP integration; deployment status stated exactly as it is.

## Future Improvements

Second workflow type (purchase request, multi-stage approval) · document uploads · email notifications · reporting exports beyond CSV · vendor performance scoring.

## ATS Keywords

C#, .NET 8, .NET Core, ASP.NET Core, ASP.NET MVC, Entity Framework Core, SQL Server, T-SQL, LINQ, Razor, Bootstrap, jQuery, Ajax, JavaScript, HTML5, CSS3, ASP.NET Core Identity, role-based access control, REST, xUnit, unit testing, integration testing, Git, GitHub, Docker, Docker Compose, Azure App Service, Azure SQL, CI/CD, GitHub Actions, dependency injection, MVC architecture, code-first migrations, responsive design, application security, OWASP, debugging, performance optimization.

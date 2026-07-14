# SECURITY

**Status:** Planned approach. Implementation begins Phase 3 (Identity); each section below gets updated from "planned" to "implemented + verified" with dates as the work lands. Security topics are also interview material — each maps to entries in INTERVIEW_GUIDE.md.

## Authentication (planned — Phase 3)

- **ASP.NET Core Identity** with **cookie-based** authentication (server-rendered MVC app; contrast with JWT used in the owner's Django SPA project — cookies suit same-origin server-rendered apps, JWTs suit token-bearing API clients).
- Passwords hashed by Identity (PBKDF2 with per-user salt) — never stored or logged in plain text.
- Cookie flags: HttpOnly, Secure, SameSite — verified when implemented.
- Lockout on repeated failures: Identity defaults, reviewed in Phase 7.

## Authorization (planned — Phase 3+)

- Roles: Administrator, Project Manager, Procurement Officer, General Employee.
- `[Authorize]` at controller level; role restrictions per action. **Server-side checks are the security boundary; hidden menu items are convenience only.**
- Rule of interest for Phase 5: a user must not approve their own request — enforced in the workflow service (and possibly a policy), with a test proving it.
- 401 (not authenticated) vs 403 (authenticated, not permitted) behavior documented and tested.

## Input Validation

- ViewModels with data annotations; `ModelState.IsValid` checked on every POST.
- Client-side validation (jQuery unobtrusive) for UX; **server-side validation is authoritative** — client checks can be bypassed with dev tools.
- Overposting/mass assignment: POST actions bind to ViewModels, never to entities, so extra fields (e.g. `IsApproved=true`) can't be smuggled in — the equivalent risk to Django's `fields = '__all__'` misuse.

## Anti-Forgery (CSRF)

- Razor form tag helpers emit anti-forgery tokens automatically; `[ValidateAntiForgeryToken]` (or global auto-validation filter — decided in Phase 3) on state-changing actions.
- Ajax POSTs must send the token explicitly — documented with the first Ajax feature in Phase 6.

## SQL Injection

- All data access via EF Core LINQ → parameterized SQL. No string-concatenated SQL anywhere. If raw SQL is ever justified, only `FromSqlInterpolated`/parameters — recorded in DECISIONS.md.

## XSS

- Razor HTML-encodes output by default. `@Html.Raw` is banned unless a DECISIONS.md entry justifies a specific, sanitized use.
- User-supplied text (comments, rejection reasons) rendered as text, never as markup.

## Open Redirects

- Any post-login `returnUrl` handling uses `Url.IsLocalUrl` before redirecting.

## Secrets & Configuration

- Development: **user secrets** for connection strings; nothing sensitive in `appsettings.json` committed to Git.
- Docker: environment variables + `.env` (with committed `.env.example` only).
- Production/Azure: App Service configuration / Key Vault (conceptual until Phase 9).
- `.gitignore` excludes local settings; any accidentally committed secret is rotated, not just deleted.

## Error Handling & Information Exposure

- Detailed errors/developer exception page in Development only; generic error page otherwise.
- Logs may contain exception details; they must never contain passwords, tokens, or full connection strings.

## File Uploads

Not in scope for early phases. If vendor documents are added (backlog), this section gains: extension/content-type allowlist, size limits, storage outside webroot, no execution of uploaded content.

## Dependency Security

NuGet packages kept current; `dotnet list package --vulnerable` run during Phase 7 hardening and before any release/deployment.

## Production Checklist (executed and dated in Phase 7/9)

- [ ] HTTPS enforced, HSTS enabled
- [ ] Cookie flags verified (HttpOnly/Secure/SameSite)
- [ ] All state-changing actions require auth + anti-forgery
- [ ] Role matrix verified against every controller action
- [ ] No secrets in repo history
- [ ] Error pages leak nothing; logging reviewed for sensitive data
- [ ] `dotnet list package --vulnerable` clean
- [ ] Overposting review: every POST binds a ViewModel

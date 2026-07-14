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

---

*Template for new entries: Title · Date · Context · Options considered · Selected approach · Reason · Consequences · Risks · Revisitable?*

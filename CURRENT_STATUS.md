# CURRENT_STATUS

> Read this file first in every session. Update it after every meaningful work session.

**Last updated:** 2026-07-14

## Current Phase

**Phase 0 — Environment Verification & Repository Hygiene** (see ROADMAP.md)

## Current Objective

1. Install SQL Server Developer edition + SSMS (engine confirmed missing — see Blockers).
2. Confirm Visual Studio 2022 with the "ASP.NET and web development" workload, and GitHub Desktop.
3. Begin Phase 1 (C# warm-up) immediately — it needs no database, so it runs in parallel with the installs.

**Interview timeline: expected within the week** — Phases 1–2 are the priority; everything else serves them.

## Completed

- Repository assessed: previously a Git-tutorial "Hello-World" repo with a single README and **no application code** — nothing to preserve except history.
- Project purpose, roadmap, and architecture direction defined.
- Documentation foundation created: README, ROADMAP, CURRENT_STATUS, AGENTS, CLAUDE at root; ARCHITECTURE, SETUP, DATABASE, TESTING, SECURITY, INTERVIEW_GUIDE, DECISIONS, PORTFOLIO_NOTES, API_AND_WORKFLOWS, UI_UX under `docs/`.
- Initial architecture decision recorded (DECISIONS.md #001: single web project + test project).
- .NET-appropriate `.gitignore` added.
- **Environment verification (2026-07-14, partial):**
  - ✅ .NET 8 SDK `8.0.422` (x64) — verified via `dotnet --version` / `--info`
  - ✅ Git `2.55.0.windows.2`
  - ✅ Windows 11 (build 10.0.28120), x64
  - ❌ SQL Server database engine **not installed** — only `SQLWriter` (VSS backup helper) found; no `MSSQLSERVER`/`SQLEXPRESS` service
  - ❓ Not yet verified: Visual Studio 2022 + ASP.NET workload, SSMS, GitHub Desktop

## In Progress

- Nothing — awaiting environment verification results from Saurabh.

## Known Issues / Blockers

- **Blocker for Phase 2 only:** SQL Server database engine is not installed (the `SQL Server VSS Writer` service found is a backup helper, not the engine). Fix: install SQL Server 2022 Developer edition + SSMS per `docs/SETUP.md` §4–5. Phase 1 is NOT blocked — it needs no database.
- Visual Studio / SSMS / GitHub Desktop presence still unverified.
- Repository rename to `epc-vendor-management` agreed; to be done by Saurabh on GitHub (Settings → General → Rename) — cosmetic, not blocking.

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

**Start the SQL Server Developer + SSMS install (it can download in the background), and while it runs, begin the Phase 1 C# warm-up** — with an interview expected within the week, C#/OOP/LINQ fluency is the highest-value work and needs no database.

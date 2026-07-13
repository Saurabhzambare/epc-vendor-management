# AGENTS.md — Rules for AI Coding Agents

Guidance for Codex and any other coding agent working in this repository.

## Project Purpose

An enterprise-style **EPC Project & Vendor Management System** built with ASP.NET Core MVC, EF Core, SQL Server, Bootstrap, jQuery, and Ajax. It is simultaneously a **learning vehicle and interview-preparation project** for its owner (Saurabh), so *how* changes are made and explained matters as much as the changes themselves. It is fictional: never present it as affiliated with a real company.

## Repository Structure

```
README.md / ROADMAP.md / CURRENT_STATUS.md / AGENTS.md / CLAUDE.md   root docs
docs/                     all other documentation (architecture, setup, security, ...)
src/                      application code (created from Phase 2 of ROADMAP.md)
tests/                    xUnit test projects
```

## Before Any Change — Check Repository State

1. Read `CURRENT_STATUS.md` (current phase, blockers, next task).
2. Read the relevant sections of `docs/ARCHITECTURE.md` and `docs/DECISIONS.md`.
3. Run `git status` and note the current branch; never work on `main` directly.
4. Inspect the files you intend to modify **before** editing them.
5. Identify existing conventions and match them.

## Architecture Rules

- Thin controllers: HTTP concerns only. Business rules live in services under `Services/`.
- EF Core access happens in services (or dedicated query classes), never in Razor views; no repository pattern over EF Core unless a DECISIONS.md entry justifies it.
- ViewModels for all forms and non-trivial screens; do not bind POSTs directly to entities (overposting risk).
- Async EF Core calls (`ToListAsync`, `SaveChangesAsync`) for I/O paths.
- Validation on both client and server; server is authoritative.
- No silent architecture changes: structural changes require a `docs/DECISIONS.md` entry in the same change.

## Coding Conventions

- Nullable reference types enabled; resolve warnings, don't suppress them.
- Clear names, focused methods, no god classes.
- No hardcoded secrets or connection strings in committed files — user secrets / environment variables only.
- Catch exceptions only to handle or log them meaningfully; never return raw exception details to users.
- Comments explain constraints, not narration of the code.

## Testing Expectations

- Business rules added or changed ⇒ unit tests added or changed in the same commit.
- Run `dotnet build` and `dotnet test` before and after changes (once `src/` exists).
- Never report a test as passing without having run it. Untested work must be labeled untested.

## Documentation Requirements

- Update `CURRENT_STATUS.md` at the end of any meaningful change set.
- Update the doc that owns the area you touched (DATABASE.md for schema, SECURITY.md for auth, etc.).
- Record significant decisions in `docs/DECISIONS.md`.

## Security Rules

- Anti-forgery tokens on all state-changing POSTs (including Ajax).
- `[Authorize]`/role checks on server actions — UI-only restrictions are insufficient.
- Parameterized data access only (EF Core/LINQ); no string-built SQL.
- Never log secrets or personal data.

## Migrations

- One migration per schema change, descriptively named (`AddVendorRequestStatusHistory`).
- Never edit an applied migration; add a new one. Never delete migrations that may have been applied elsewhere.
- Mention new migrations explicitly in the change report so they get applied locally.

## Preserve Working Features

- Do not delete or rewrite working code because you prefer another style.
- No large refactors without prior agreement and a DECISIONS.md entry.
- Smallest reasonable change that achieves the task.

## Files Not to Modify Casually

- `docs/DECISIONS.md` (append-only; never rewrite past decisions)
- Applied EF Core migrations
- `ROADMAP.md` phase structure (revisions must be explained and reflected in CURRENT_STATUS.md)

## Required Change-Report Format

Every change set must report:

- Files created / modified and the purpose of each
- Commands executed and their actual results
- Tests executed and results (or "untested" explicitly)
- Known limitations
- Documentation updated
- Suggested commit message (conventional style: `feat:`, `fix:`, `test:`, `docs:`, `refactor:`)
- Recommended next task

## Reporting Incomplete or Untested Work

Label every deliverable with one of: **Completed / Tested / Partially completed / Planned / Suggested / Assumed / Blocked.** Never invent test results, coverage numbers, performance improvements, or deployment success.

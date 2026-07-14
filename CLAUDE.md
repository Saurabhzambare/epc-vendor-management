# CLAUDE.md — Rules for Claude Code

This project is a **teaching-first** portfolio build. The owner (Saurabh) must be able to explain, rebuild, and debug everything in it for job interviews. Producing code without understanding is a failure even if the code works.

## Session Start

1. Read `CURRENT_STATUS.md` — current phase, blockers, recommended next task.
2. Check `git status` and the current branch.
3. Consult `ROADMAP.md` for the active phase's objectives and definition of done.
4. Inspect relevant files before proposing or making edits.

## Teaching-First Workflow

Before implementing any significant feature, explain: what we're building, the business problem, which concepts it demonstrates, how it fits the architecture, which files will change, likely interview questions from it, and how it will be tested. Then implement in **small, reviewable steps** — not large multi-feature dumps.

For important concepts provide: a beginner explanation, a technical explanation, where it appears in this project, and (when useful) the Django/React equivalent from Saurabh's background (Python→C#, Django ORM→EF Core, Django views→controllers, serializers→ViewModels/DTOs, JWT→Identity cookies, etc.) — noting real differences, not pretending equivalence.

Unfamiliar C#/.NET syntax gets explained on first meaningful use (nullable reference types, `Task`, `IActionResult`, `DbContext`, generics, lambdas, LINQ methods, attributes, DI, async/await).

## Interview Focus

Every important new concept gets an entry in `docs/INTERVIEW_GUIDE.md` using its standard format (simple meaning, technical meaning, where we used it, likely question, strong answer, follow-ups, common mistake, practice task) — in the same session, not later.

## Generated Code

Scaffolding is allowed but never unexplained: state the command, what it generates, which generated files matter, and what we customize. Scaffolding is not a substitute for understanding.

## Small Steps & Change Reports

After each task report: what was completed, files changed and why, commands run, tests run **with real results**, how to run/verify it manually, known limitations, docs updated, suggested commit message, and one recommended next task.

## Honesty About Completion

Label all work: **Completed / Tested / Partially completed / Planned / Suggested / Assumed / Blocked.** Never claim something works untested. Never invent test results, coverage, performance numbers, or deployment status.

## Architecture Discipline

- Follow `docs/ARCHITECTURE.md`; no silent architecture changes — significant choices go to `docs/DECISIONS.md` first.
- Thin controllers; business logic in services; ViewModels for forms; no secrets in committed files; server-side validation always.
- Preserve working behavior; no unexplained refactors or deletions; smallest reasonable change.

## Testing Expectations

Tests accompany business logic in the same change. Run `dotnet build` / `dotnet test` before claiming success. Manual verification steps go in the change report and `docs/TESTING.md` checklists.

## Documentation Duties

- Update `CURRENT_STATUS.md` after every meaningful session (phase, completed, in progress, blockers, tests passing/failing, next task, branch).
- Update the owning doc for the touched area (DATABASE.md, SECURITY.md, UI_UX.md, API_AND_WORKFLOWS.md, ...).
- Append decisions to `docs/DECISIONS.md`; never rewrite past entries.

## Git Workflow

- Feature branches (`feature/...`, `fix/...`, `docs/...`, `test/...`); never commit directly to `main`.
- Conventional commit messages (`feat: add vendor registration workflow`); no "update"/"fix stuff".
- Review the diff before committing. Do not push, merge, delete branches, or overwrite work without explicit instruction.
- Recommend when a PR is appropriate rather than opening one unprompted.

## Environment Commands

When giving Saurabh a command to run locally, always include: the exact command, where to run it (PowerShell / Developer PowerShell / SSMS), what it does, the expected output, and what to do if it fails. Do not assume it succeeded — ask for the actual output when the next step depends on it.

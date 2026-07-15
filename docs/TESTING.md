# TESTING

**Status:** Strategy defined; no tests exist yet (no code exists). First tests arrive in Phase 1 (warm-up) and Phase 2 (Department service).

## Strategy

Testing accompanies development — a feature is not "done" until its tests pass and its manual checklist is walked. Three levels:

1. **Unit tests (xUnit)** — business rules in services, in isolation: workflow transitions, duplicate-assignment rules, validation logic. Fast, no database.
2. **Integration tests** — controller→service→EF Core against a test database, for critical flows: auth redirects, CRUD round-trips, Ajax endpoints, workflow actions. Introduced seriously in Phase 7.
3. **Manual checklists** — per-screen scripts in this file for what automation doesn't cover (visual states, browser behavior).

Rule: **no meaningless tests written to inflate counts.** Every test asserts a behavior someone could break.

## Conventions

- Test project: `tests/EpcVendorManagement.Tests`
- Naming: `MethodName_Scenario_ExpectedResult` — e.g. `SubmitRequest_FromDraft_SetsStatusSubmitted`, `AssignEmployee_AlreadyAssigned_ReturnsError`
- Structure: **Arrange, Act, Assert** — commented explicitly in early tests while the pattern is being learned.

## Mocking Strategy

- Services are tested against interfaces; dependencies faked/mocked (Moq or hand-written fakes — decided when first needed, recorded in DECISIONS.md).
- EF Core: prefer the **SQLite in-memory or SQL Server LocalDB test database** over mocking DbContext (mocking DbSet is brittle and teaches nothing) — final choice made in Phase 2 and recorded.

## Test Database Strategy

Integration tests get their own database (created/dropped per run) so they never touch development data. Details documented when implemented.

## What to Test per Feature (template)

- Expected behavior (happy path)
- Invalid input (validation errors surface correctly)
- Boundary cases (dates equal, empty lists, max lengths)
- Authorization (anonymous + wrong role rejected)
- Workflow states (legal and illegal transitions)

## How to Run

```
dotnet test
```

Expected: `Passed! - Failed: 0`. (Available from Phase 2.)

## Currently Tested Areas

- **Warmup (Phase 1):** `Vendor` default active state, `Deactivate()` behavior, and async `VendorRegistry.LoadVendorsAsync` — 3/3 xUnit tests passing in `tests/Warmup.Tests` (verified 2026-07-14 by Saurabh: `total: 3, failed: 0, succeeded: 3`).

## Currently Untested Areas

- Warmup LINQ queries in `Program.cs` (verified manually against expected output only).
- Everything in the main application — it does not exist yet (Phase 2+).

## Manual Regression Checklist

*Built up per module from Phase 2 onward. Final consolidated checklist owned by Phase 7.*

### Template (per screen)

- [ ] Loads for authorized role; blocked for others
- [ ] Empty state renders sensibly
- [ ] Create with valid data succeeds and appears in list
- [ ] Create with invalid data shows field errors, keeps input
- [ ] Edit persists; concurrency/duplicate rules enforced
- [ ] Deactivate hides from default lists, preserves history
- [ ] Search/filter/pagination return correct slices
- [ ] Ajax actions show loading, success, and error states

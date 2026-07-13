# UI_UX

**Status:** Planned design; updated with real screenshots and refinements as screens are built (Phase 2 onward).

## Design Direction

Clean internal-tool aesthetic: Bootstrap 5 components, no heavy custom theming. Clarity, consistency, and honest UI states matter more than visual flair — this mirrors real enterprise line-of-business software and keeps the focus on the JD-relevant skills (Bootstrap, jQuery, Ajax, responsive layout).

## Main Screens (planned)

| Screen | Phase | Notes |
|---|---|---|
| Login | 3 | Identity; validation feedback; return-url handling |
| Dashboard | 6 | cards: active projects, pending approvals, active vendors; recent activity list |
| Departments list/form | 2 | the pattern-setting CRUD module |
| Employees list/form/details | 4 | department dropdown; status badge |
| Vendors list/form/details | 4 | category filter; approval-status badge |
| Projects list/form/details | 4 | manager, dates, budget, status; tabs/sections for milestones + assignments |
| Assignment forms | 4 | dependent dropdown (department → employee) |
| Vendor request form + review screen | 5 | status timeline, comments, role-dependent action buttons |
| Reports | 7 | tabular + CSV export |

## Navigation

Top navbar (Bootstrap): brand → Dashboard · Projects · Vendors · Employees · Departments · Requests · Reports · user menu (profile/logout). **Links are filtered by role for convenience; the server remains the enforcement point.** Active section highlighted; collapses to hamburger on mobile.

## Key User Journeys

1. **Submit a vendor request (Employee):** Dashboard → Requests → New → fill form (inline validation) → Save draft → Submit → sees "Submitted" badge + history entry.
2. **Approve a request (Procurement Officer):** Dashboard "Pending approvals" card → request details → review info + history → Approve with comment (Ajax; button shows spinner) → success toast, status badge updates without reload.
3. **Staff a project (Project Manager):** Project details → Assignments → pick department → employee dropdown repopulates (Ajax) → dates + responsibility → duplicate attempt shows a field error.

## Form Behavior

- Labels above inputs; required markers; Bootstrap validation styling.
- Client-side validation (jQuery unobtrusive) for instant feedback; server-side always re-validates.
- On server-side failure the form redisplays with values preserved and errors beside fields + summary on top.
- Destructive/irreversible actions (deactivate, reject, cancel) get a confirmation dialog.

## UI States (every dynamic screen must handle all four)

- **Loading:** spinner/disabled button during Ajax; no double-submit.
- **Success:** toast or inline alert; updated data visible without confusion.
- **Empty:** friendly message + primary action ("No vendors yet — Add vendor"), never a bare empty table.
- **Error:** human-readable message with retry guidance; never a raw exception; Ajax failures don't leave a dead spinner.

## Validation Feedback

Field-level messages appear next to the input; a summary appears for cross-field/business errors (e.g., "Employee already assigned to this project"). Errors are phrased as what to do, not what code failed.

## Accessibility Considerations

Semantic HTML (labels bound to inputs, buttons vs. links used correctly); keyboard-reachable forms and dialogs; sufficient color contrast on badges; status conveyed by text + color, not color alone; alt text on images.

## Responsive Behavior

Bootstrap grid throughout; tables get horizontal scroll or stacked layouts on narrow screens; navbar collapses; forms single-column on mobile. Verified with browser dev-tools device emulation per module checklist.

## Design Decisions Log

*Recorded here (or in DECISIONS.md if architectural) as real screens raise real questions.*

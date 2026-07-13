# SETUP — Windows Development Environment

Beginner-friendly setup and verification for this project. Assumes **Windows 10/11**. (macOS/Linux: use `dotnet` CLI + VS Code + SQL Server in Docker; ask for adapted steps if needed.)

For every command below: **run it, compare with the expected output, and if it differs, check the troubleshooting notes — do not assume success.**

## 0. System Requirements Check

- Windows 10 64-bit (version 1909+) or Windows 11
- 8 GB RAM minimum (16 GB recommended once SQL Server + Visual Studio + Docker coexist)
- ~30 GB free disk
- Virtualization enabled (needed later for Docker/WSL2): Task Manager → Performance → CPU → "Virtualization: Enabled"

## 1. .NET 8 SDK

**Check first:**

```
dotnet --version
```

- **Where:** PowerShell or Windows Terminal
- **What it does:** prints the installed .NET SDK version
- **Expected:** a version starting with `8.` (e.g. `8.0.4xx`)
- **If it fails:** `'dotnet' is not recognized` → SDK not installed. Download the **.NET 8 SDK** (not just the runtime) from https://dotnet.microsoft.com/download/dotnet/8.0, install, **open a new terminal**, retry.

Also useful: `dotnet --list-sdks` (shows all installed SDKs — multiple versions can coexist; that is normal and is how .NET 6 vs .NET 8 projects live on one machine).

## 2. Visual Studio 2022 (Community)

**Check:** Start menu → "Visual Studio 2022". If installed, open **Visual Studio Installer** → Modify → confirm the workload **"ASP.NET and web development"** is checked.

- **If missing:** download Community edition from https://visualstudio.microsoft.com/, and during install select the **ASP.NET and web development** workload. That workload is the single most common setup mistake — without it there are no web project templates.

## 3. Visual Studio Code (optional but useful)

For quick edits, Markdown, and JS. https://code.visualstudio.com/ with the **C# Dev Kit** extension if you want to use it for C# too.

## 4. SQL Server (Developer or Express edition)

**Check first (PowerShell):**

```
Get-Service | Where-Object {$_.DisplayName -like "*SQL Server*"}
```

- **Expected:** a service like `SQL Server (MSSQLSERVER)` or `SQL Server (SQLEXPRESS)` with Status `Running`.
- **If nothing appears:** install **SQL Server Developer edition** (free, full-featured) from https://www.microsoft.com/sql-server/sql-server-downloads → "Basic" install is fine. Note the instance name and connection string it prints at the end.
- **Fallback:** if the install fights you, SQL Server in Docker is a first-class alternative — say so in session and we will pull that part of Phase 8 forward.

## 5. SQL Server Management Studio (SSMS)

**Check:** Start menu → "SQL Server Management Studio".

- **If missing:** https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms
- **Verify the whole database stack:** open SSMS → Server name: `localhost` (default instance) or `localhost\SQLEXPRESS` (Express) → Authentication: Windows Authentication → Connect. Success = the database engine is reachable; this is the exact connectivity the app will use.

## 6. Git

```
git --version
```

- **Expected:** `git version 2.x`
- **If missing:** https://git-scm.com/download/win (defaults are fine). Then set identity:

```
git config --global user.name  "Saurabh Lalit Zambare"
git config --global user.email "zambaresaurabh10@gmail.com"
```

## 7. GitHub Desktop

**Check:** Start menu → "GitHub Desktop". **If missing:** https://desktop.github.com/, sign in with the GitHub account.

First-loop exercise (Phase 0): File → Clone repository → pick this repo → make a branch → edit a doc → commit → push → open PR on github.com → close it. That is the whole workflow once, end to end.

## 8. Docker Desktop (defer until Phase 8 unless SQL Server install failed)

**Check:** `docker --version` → expected `Docker version 2x.x`. Requires WSL2 (`wsl --status`) and virtualization enabled. Install from https://www.docker.com/products/docker-desktop/ when the roadmap reaches it.

## 9. Browser & API tools

Any Chromium browser or Firefox (we will use its DevTools heavily in Phase 6). API testing tools (Postman etc.) only if/when useful — MVC apps are mostly exercised through the browser.

---

## Project Setup (available from Phase 2 — nothing to run yet)

Once `src/` exists, the flow will be:

1. **Clone:** GitHub Desktop → Clone repository (or `git clone <url>`)
2. **Open:** double-click the `.sln`, or `code .`
3. **Restore:** `dotnet restore` — downloads NuGet packages. Expected: `Restored ... (in Xs)`
4. **Connection string:** stored via **user secrets**, never committed:
   ```
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=EpcVendorManagement;Trusted_Connection=True;TrustServerCertificate=True"
   ```
   (run inside `src/EpcVendorManagement.Web`; adjust `Server=` for named instances)
5. **Migrations:** `dotnet ef database update` — creates/updates the database. If `dotnet ef` is missing: `dotnet tool install --global dotnet-ef`
6. **Run:** `dotnet run --project src/EpcVendorManagement.Web` then open the printed `https://localhost:xxxx`
7. **Tests:** `dotnet test` — expected: `Passed! - Failed: 0`

## Troubleshooting (grows with real errors we actually hit)

| Symptom | Likely cause | Fix |
|---|---|---|
| `'dotnet' is not recognized` | SDK not installed / stale PATH | install SDK, open a **new** terminal |
| No web templates in Visual Studio | missing workload | VS Installer → Modify → ASP.NET and web development |
| SSMS cannot connect to `localhost` | wrong instance name or service stopped | try `localhost\SQLEXPRESS`; start the service in `services.msc` |
| `A network-related or instance-specific error` at runtime | bad connection string | match the server name that works in SSMS |
| `dotnet ef` not found | tool not installed | `dotnet tool install --global dotnet-ef` |
| Docker fails to start | WSL2/virtualization | `wsl --update`; enable virtualization in BIOS |

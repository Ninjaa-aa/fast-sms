# Societies Management System (SMM)

Desktop application for managing university societies: members, events, tasks, and admin workflows. Built as a **Windows Forms** client on **.NET** with **SQL Server** for persistence.

> Repository name on GitHub may differ; this codebase is the **Societies Management System** project.

---

## Features

### Student

- Browse societies and apply for membership
- View membership status (pending, approved, rejected)
- Browse events and register for events
- View issued ticket codes

### Society head

- Dashboard scoped to the society they head (requires **Active** society status)
- Manage members (memberships for their society)
- Manage events (create/update; some items may require admin approval)
- Manage tasks assigned to members
- Society-level reports

### Admin

- Dashboard with summary counts (users, active societies, pending events)
- Manage users (non-admin accounts)
- Manage societies (status, assignments)
- Approve or reject pending events
- Admin reports

### Authentication

- Login and self-registration (Student or Society Head; admin is not self-registered)
- Passwords stored as **BCrypt** hashes (`BCrypt.Net-Next`)

---

## Tech stack

| Layer | Technology |
|--------|------------|
| UI | Windows Forms |
| Runtime | .NET (`net10.0-windows`) |
| Database | SQL Server (LocalDB, Express, or full instance) |
| Data access | `Microsoft.Data.SqlClient`, parameterized SQL |
| Configuration | `DotNetEnv` (`.env` beside the executable / project) |

---

## Prerequisites

- **Windows** (WinForms targets Windows)
- **[.NET SDK](https://dotnet.microsoft.com/download)** matching **`net10.0-windows`** (as defined in `SMM-PROJ/SMM-PROJ.csproj`)
- **SQL Server** (e.g. Express / LocalDB) reachable from your machine
- **Visual Studio 2022+** (recommended) or another editor with .NET support — solution file: `SMM-PROJ.slnx`; project: `SMM-PROJ/SMM-PROJ.csproj`

---

## Repository layout

```text
SMM-PROJ/                          ← Git repository root
├── README.md
├── .gitignore
├── SMM-PROJ.slnx
└── SMM-PROJ/                      ← .NET project folder
    ├── Program.cs                 ← Entry; loads .env then shows LoginForm
    ├── SMM-PROJ.csproj
    ├── .env.example               ← Copy to .env and adjust
    ├── Config/
    │   └── EnvConfig.cs           ← Builds connection string from .env
    ├── DAL/                       ← Data access (DBHelper + table DALs)
    ├── Database/
    │   ├── schema.sql             ← Creates DB + tables
    │   └── seed.sql               ← Sample data (run after schema)
    ├── Helpers/
    │   ├── PasswordHasher.cs      ← BCrypt hash / verify
    │   └── Session.cs             ← Current user after login
    ├── Models/                    ← User, Society, Event, etc.
    ├── analysis/                  ← Tasks 2–8 metrics write-ups (Markdown)
    ├── assets/                    ← Cover logo + architecture diagram (PDF build)
    ├── generate_report.py         ← Builds SMM_Report.pdf (see below)
    └── forms/
        ├── Auth/                  ← Login, Register
        ├── Admin/                 ← Admin dashboard + tools
        ├── Society/               ← Society head dashboard + tools
        └── Student/               ← Student dashboard + tools
```

---

## Course report (PDF)

The **SE-4011** consolidated deliverable is generated as **`SMM-PROJ/SMM_Report.pdf`** from the same folder as the WinForms project.

### Prerequisites

- **Python 3** with the Windows launcher **`py`** available on `PATH`
- Packages: **reportlab**, **Pillow**

```powershell
cd "SMM-PROJ"
py -m pip install reportlab Pillow
```

### Generate the PDF

From the **`SMM-PROJ`** project directory (where `generate_report.py` lives):

```powershell
py generate_report.py
```

This writes **`SMM_Report.pdf`** next to the script. Content is assembled from the hardcoded tables and text in `generate_report.py` (aligned with the analysis under **`analysis/`**), ending with **Section 9 — Member Contributions** (edit the contribution rows in `make_member_contribution_table()` inside `generate_report.py` if your team’s split differs). Ensure **`assets/nuces_logo.png`** and **`assets/architecture.png`** exist for the cover and architecture pages.

### Metrics analysis (Markdown)

Task write-ups used for the course submission live under **`SMM-PROJ/analysis/`**, for example:

- `task2-cyclomatic-complexity.md` — cyclomatic complexity and test cases  
- `task3-structural-metric.md` — structural metrics and best-module justification  
- `task4-ck-metrics.md` — CK suite  
- `task5-fault-injection.md` — fault injection and Poisson reliability  
- `task6-klm-evaluation.md` — KLM usability  
- `task7-cocomo.md` — Basic COCOMO (semi-detached)  
- `task8-documentation-ratio.md` — documentation ratio  

---

## Database setup

1. Open **SQL Server Management Studio** (or `sqlcmd`) connected to your instance.
2. Run **`SMM-PROJ/Database/schema.sql`**  
   - Creates database **`SocietiesManagementSystem`** and all tables.
3. Run **`SMM-PROJ/Database/seed.sql`**  
   - Inserts demo users, societies, memberships, events, tasks, and announcements.

If the database already contains data, avoid re-running the full `seed.sql` without clearing conflicting rows (or adjust scripts), because inserts may violate unique constraints.

---

## Configuration (`.env`)

1. Copy **`SMM-PROJ/.env.example`** to **`SMM-PROJ/.env`**.
2. Set variables for your SQL Server instance.

**Option A — server + database name (typical for local Express):**

```env
DB_SERVER=localhost\SQLEXPRESS01
DB_NAME=SocietiesManagementSystem
DB_INTEGRATED_SECURITY=True
```

**Option B — full connection string:** set `CONNECTION_STRING` in `.env` (see comments in `.env.example`). If the string has no catalog, `DB_NAME` is appended as `Initial Catalog`.

The app loads `.env` at startup (`EnvConfig.Load()` in `Program.cs`). The `.csproj` copies `.env` to the output folder when present; **do not commit real `.env` files** (they are gitignored).

---

## Build and run

From the repository root:

```powershell
cd "SMM-PROJ"
dotnet build
dotnet run
```

Or open **`SMM-PROJ.slnx`** / **`SMM-PROJ/SMM-PROJ.csproj`** in Visual Studio and press **F5**.

Ensure SQL Server is running and `.env` points to the same database you created with `schema.sql`.

---

## Demo accounts (after `seed.sql`)

All seeded users share the same demo password: **`pass123`**.

| Role | Email |
|------|--------|
| Admin | `admin@fast.edu.pk` |
| Society head | `ali@fast.edu.pk`, `sara@fast.edu.pk` |
| Student | `ahmed@fast.edu.pk`, `fatima@fast.edu.pk`, `usman@fast.edu.pk` |

**Security:** These credentials are for local/demo use only. Change or remove them in any shared or production environment.

---

## Architecture notes

- **Session** holds `UserID`, `FullName`, `Email`, `Role`, and optional `SocietyID` for society heads.
- **DBHelper** centralizes connections using `EnvConfig.ConnectionString`.
- **DAL** classes contain SQL; use parameterized queries to limit injection risk.
- **Society head** features are disabled in the UI until the head’s society status is **Active** (see `SocietyDashboard`).

---

## Troubleshooting

| Issue | What to check |
|--------|----------------|
| Cannot connect to database | `DB_SERVER`, firewall, SQL Server service, TCP/protocol for remote instances |
| Login always fails | Email spelling; password exact (`pass123`); `PasswordHash` in `Users` matches BCrypt for that password; re-seed or update hash |
| Missing `.env` | File must exist as `SMM-PROJ/.env` and be copied to build output (or run from project directory per your setup) |

---

## License / course use

Add your team’s or institution’s license here if required for your submission.

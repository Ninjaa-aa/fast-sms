# FAST Societies Management System — Cursor Implementation Plan

> **Scope:** Functional Requirements + Task 1 only (C# WinForms desktop app + SQL Server database + ERD)

---

## Tech Stack

- **Frontend:** C# Windows Forms (.NET 6 or .NET Framework 4.8)
- **Database:** SQL Server (LocalDB or full SQL Server instance)
- **ORM/Data Access:** ADO.NET (plain SQL) or Entity Framework Core
- **IDE:** Visual Studio 2022

---

## Project Structure

```
SocietiesManagementSystem/
├── SocietiesMS.sln
├── SocietiesMS/
│   ├── Program.cs
│   ├── Forms/
│   │   ├── Auth/
│   │   │   ├── frmLogin.cs
│   │   │   └── frmRegister.cs
│   │   ├── Student/
│   │   │   ├── frmStudentDashboard.cs
│   │   │   ├── frmBrowseSocieties.cs
│   │   │   ├── frmMyMemberships.cs
│   │   │   ├── frmEvents.cs
│   │   │   └── frmMyTickets.cs
│   │   ├── Society/
│   │   │   ├── frmSocietyDashboard.cs
│   │   │   ├── frmManageMembers.cs
│   │   │   ├── frmManageEvents.cs
│   │   │   ├── frmAssignTasks.cs
│   │   │   └── frmSocietyReports.cs
│   │   └── Admin/
│   │       ├── frmAdminDashboard.cs
│   │       ├── frmManageStudents.cs
│   │       ├── frmManageSocieties.cs
│   │       ├── frmApproveEvents.cs
│   │       └── frmUniversityReports.cs
│   ├── Models/
│   │   ├── Student.cs
│   │   ├── Society.cs
│   │   ├── Event.cs
│   │   ├── Membership.cs
│   │   ├── Task.cs
│   │   └── Ticket.cs
│   ├── DAL/                          # Data Access Layer
│   │   ├── DatabaseHelper.cs         # Connection string + ExecuteQuery helpers
│   │   ├── StudentDAL.cs
│   │   ├── SocietyDAL.cs
│   │   ├── EventDAL.cs
│   │   ├── MembershipDAL.cs
│   │   └── AdminDAL.cs
│   └── Helpers/
│       ├── PasswordHasher.cs
│       └── SessionManager.cs         # Stores logged-in user info globally
└── Database/
    ├── schema.sql                    # Full DDL script
    └── seed.sql                      # Sample data for testing
```

---

## Phase 1 — Database Schema

### Task 1A: Design and create `schema.sql`

Create the following tables in SQL Server:

```sql
-- Users table (shared across all roles)
CREATE TABLE Users (
    UserID      INT PRIMARY KEY IDENTITY,
    FullName    NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role        NVARCHAR(20) NOT NULL CHECK (Role IN ('Student', 'SocietyHead', 'SocietyMember', 'Admin')),
    CreatedAt   DATETIME DEFAULT GETDATE()
);

-- Societies table
CREATE TABLE Societies (
    SocietyID   INT PRIMARY KEY IDENTITY,
    Name        NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    HeadUserID  INT FOREIGN KEY REFERENCES Users(UserID),
    Status      NVARCHAR(20) DEFAULT 'Active' CHECK (Status IN ('Active', 'Suspended', 'Pending')),
    CreatedAt   DATETIME DEFAULT GETDATE()
);

-- Memberships table (Student <-> Society)
CREATE TABLE Memberships (
    MembershipID INT PRIMARY KEY IDENTITY,
    UserID       INT FOREIGN KEY REFERENCES Users(UserID),
    SocietyID    INT FOREIGN KEY REFERENCES Societies(SocietyID),
    Status       NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
    AppliedAt    DATETIME DEFAULT GETDATE(),
    ApprovedAt   DATETIME,
    UNIQUE (UserID, SocietyID)
);

-- Events table
CREATE TABLE Events (
    EventID     INT PRIMARY KEY IDENTITY,
    SocietyID   INT FOREIGN KEY REFERENCES Societies(SocietyID),
    Title       NVARCHAR(150) NOT NULL,
    Description NVARCHAR(1000),
    EventDate   DATETIME NOT NULL,
    Venue       NVARCHAR(200),
    Status      NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Approved', 'Cancelled')),
    CreatedAt   DATETIME DEFAULT GETDATE()
);

-- Event Registrations (Student registers for event)
CREATE TABLE EventRegistrations (
    RegistrationID INT PRIMARY KEY IDENTITY,
    EventID        INT FOREIGN KEY REFERENCES Events(EventID),
    UserID         INT FOREIGN KEY REFERENCES Users(UserID),
    RegisteredAt   DATETIME DEFAULT GETDATE(),
    TicketCode     NVARCHAR(50) UNIQUE,
    UNIQUE (EventID, UserID)
);

-- Tasks table (Society assigns to members)
CREATE TABLE Tasks (
    TaskID      INT PRIMARY KEY IDENTITY,
    SocietyID   INT FOREIGN KEY REFERENCES Societies(SocietyID),
    AssignedTo  INT FOREIGN KEY REFERENCES Users(UserID),
    AssignedBy  INT FOREIGN KEY REFERENCES Users(UserID),
    Title       NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500),
    DueDate     DATETIME,
    Status      NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'InProgress', 'Completed')),
    CreatedAt   DATETIME DEFAULT GETDATE()
);
```

**Deliverable:** `Database/schema.sql` with all CREATE TABLE statements + FK constraints.

---

## Phase 2 — Project Setup

### Task 2A: Create the Visual Studio Solution

1. Create new **Windows Forms App (.NET 6)** solution named `SocietiesMS`
2. Add NuGet packages:
   - `Microsoft.Data.SqlClient` (for SQL Server connectivity)
   - `BCrypt.Net-Next` (for password hashing)
3. Set up `DatabaseHelper.cs` with connection string pointing to LocalDB:
   ```csharp
   private static string connectionString = 
       @"Server=(localdb)\MSSQLLocalDB;Database=SocietiesMS;Integrated Security=true;";
   ```
4. Run `schema.sql` against the LocalDB instance on app startup if DB doesn't exist (or document manual setup steps).

### Task 2B: SessionManager

```csharp
public static class SessionManager {
    public static int CurrentUserID { get; set; }
    public static string CurrentUserRole { get; set; }
    public static string CurrentUserName { get; set; }
}
```

---

## Phase 3 — Authentication Forms

### Task 3A: `frmLogin`

**UI Elements:** Email TextBox, Password TextBox (masked), Login Button, Register Link

**Logic:**
- On Login click → query `Users` table for matching email
- Verify password hash using BCrypt
- Set `SessionManager` fields
- Open role-based dashboard: `frmStudentDashboard`, `frmSocietyDashboard`, or `frmAdminDashboard`
- Show error label if credentials invalid

**FR Covered:** Student FR-1, (shared with all roles)

---

### Task 3B: `frmRegister`

**UI Elements:** FullName, Email, Password, Confirm Password, Role dropdown (Student only for self-registration), Register Button

**Logic:**
- Validate fields (non-empty, email format, passwords match)
- Hash password with BCrypt
- Insert into `Users` table
- Default role = `Student`
- Redirect to login on success

**FR Covered:** Student FR-1

---

## Phase 4 — Student Module

### Task 4A: `frmStudentDashboard`

- Nav panel with buttons: Browse Societies, My Memberships, Events, My Tickets
- Welcome label showing `SessionManager.CurrentUserName`

### Task 4B: `frmBrowseSocieties`

**UI:** DataGridView listing all Active societies (Name, Description, Head)

**Logic:**
- Load all societies with `Status = 'Active'` from DB
- "Apply for Membership" button → inserts row into `Memberships` with `Status = Pending`
- Disable button if already applied or already a member

**FR Covered:** Student FR-2, FR-3, FR-4

---

### Task 4C: `frmMyMemberships`

**UI:** DataGridView showing student's memberships with status (Pending / Approved / Rejected)

**FR Covered:** Student FR-7

---

### Task 4D: `frmEvents`

**UI:** DataGridView showing all Approved upcoming events (Title, Society, Date, Venue)

**Logic:**
- Filter by `EventDate >= GETDATE()` and `Status = 'Approved'`
- "Register" button → insert into `EventRegistrations` with a generated `TicketCode` (GUID substring)
- Disable if already registered

**FR Covered:** Student FR-5, FR-6

---

### Task 4E: `frmMyTickets`

**UI:** DataGridView of student's registrations (Event Title, Date, Venue, TicketCode)

**FR Covered:** Student FR-8

---

## Phase 5 — Society Module

### Task 5A: `frmSocietyDashboard`

- Nav panel: Manage Members, Manage Events, Assign Tasks, Reports
- Shows the society this user is head of

### Task 5B: `frmManageMembers`

**UI:** Two grids — Pending Requests + Current Members

**Logic:**
- Load pending `Memberships` for this society
- "Approve" → update `Status = Approved`, set `ApprovedAt`
- "Reject" → update `Status = Rejected`
- Show full member list in second grid

**FR Covered:** Society FR-2, FR-3

---

### Task 5C: `frmManageEvents`

**UI:** DataGridView of society's events + Add/Edit/Cancel buttons

**Logic:**
- Create Event: opens a dialog/sub-form to fill Title, Description, Date, Venue → insert with `Status = Pending` (admin must approve)
- Edit Event: update allowed only if `Status = Pending`
- Cancel Event: update `Status = Cancelled`

**FR Covered:** Society FR-4

---

### Task 5D: `frmAssignTasks`

**UI:** Member dropdown, Task Title, Description, Due Date inputs + DataGridView of existing tasks

**Logic:**
- Dropdown populated with approved members of this society
- Submit → insert into `Tasks`
- Grid shows all tasks with status

**FR Covered:** Society FR-5

---

### Task 5E: `frmSocietyReports`

**UI:** Two report views (switchable via radio buttons or tabs):
1. Members Report: list of all approved members
2. Events Report: list of all events with registration count

**Logic:** Queries with JOINs; export to DataGridView. (PrintDocument or simple DataGridView print for bonus)

**FR Covered:** Society FR-6

---

## Phase 6 — Admin Module

### Task 6A: `frmAdminDashboard`

- Nav: Manage Students, Manage Societies, Approve Events, Reports
- Summary stats (total students, societies, pending events) in label panel

### Task 6B: `frmManageStudents`

**UI:** DataGridView of all users with Role = Student

**Logic:**
- View all student accounts
- Delete account (soft delete or hard delete)

**FR Covered:** Admin FR-1

---

### Task 6C: `frmManageSocieties`

**UI:** DataGridView of all societies with Status column

**Logic:**
- Create new Society → insert record + assign head user
- Approve pending societies → update `Status = Active`
- Suspend → update `Status = Suspended`
- Delete → remove record (cascade or guard with FK check)

**FR Covered:** Admin FR-2, FR-3

---

### Task 6D: `frmApproveEvents`

**UI:** DataGridView of events with `Status = Pending`

**Logic:**
- Approve → `Status = Approved`
- Reject → `Status = Cancelled`

**FR Covered:** Admin FR-4

---

### Task 6E: `frmUniversityReports`

**UI:** Tab control with:
1. All Societies + member counts
2. All Events + registrations
3. Society activity summary

**FR Covered:** Admin FR-5

---

## Phase 7 — ERD (for Report Document)

Include an Entity-Relationship Diagram in the `.docx` report showing:

**Entities:**
- `Users` (UserID PK, FullName, Email, PasswordHash, Role)
- `Societies` (SocietyID PK, Name, HeadUserID FK)
- `Memberships` (MembershipID PK, UserID FK, SocietyID FK, Status)
- `Events` (EventID PK, SocietyID FK, Title, EventDate, Status)
- `EventRegistrations` (RegistrationID PK, EventID FK, UserID FK, TicketCode)
- `Tasks` (TaskID PK, SocietyID FK, AssignedTo FK, AssignedBy FK)

**Relationships:**
- Users `1—*` Memberships `*—1` Societies
- Societies `1—*` Events
- Events `1—*` EventRegistrations `*—1` Users
- Societies `1—*` Tasks `*—1` Users (assignee)
- Users `1—1` Societies (as head)

Draw this in draw.io, Lucidchart, or dbdiagram.io and paste as image into report.

---

## Implementation Order (Recommended)

| Step | What to build | Priority |
|------|--------------|----------|
| 1 | `schema.sql` + seed data | High |
| 2 | Project setup + `DatabaseHelper` + `SessionManager` | High |
| 3 | `frmLogin` + `frmRegister` | High |
| 4 | `frmStudentDashboard` + Browse Societies + Apply | High |
| 5 | `frmSocietyDashboard` + Manage Members (approve/reject) | High |
| 6 | `frmAdminDashboard` + Manage Societies + Approve Events | High |
| 7 | Events flow (create, approve, register, ticket) | Medium |
| 8 | Tasks module | Medium |
| 9 | Reports (Society + Admin) | Medium |
| 10 | ERD diagram for report | Low |

---

## Notes for Cursor

- Keep all DB calls in the `DAL/` layer. Forms should never have raw SQL.
- Use parameterized queries everywhere — no string concatenation in SQL.
- Use `MessageBox.Show()` for user-facing errors.
- Every form should call `this.Close()` and open the next form — do not open forms on top of each other infinitely.
- On logout, clear `SessionManager` and return to `frmLogin`.
- The `SocietyHead` role means the user is both a member of their own society AND its head. Handle this in queries.
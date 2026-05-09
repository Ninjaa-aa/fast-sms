# Societies Management System (SMM) — Cursor Implementation Requirements

## Project Overview

Build a **C# Windows Forms (.NET Framework 4.8) desktop application** connected to **SQL Server** for managing university student societies. The app has three user roles: Student, Society Head, and Admin.

---

## Tech Stack

- **Language**: C# (.NET Framework 4.8)
- **UI Framework**: Windows Forms (WinForms)
- **Database**: SQL Server (LocalDB or Express)
- **ORM**: Raw ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)
- **IDE Target**: Visual Studio 2022

---

## Database Schema (Already Created)

The following tables exist in the database `SocietiesManagementSystem`:

```sql
-- Users (UserID, FullName, Email, Password, Role, CreatedAt)
-- Role values: 'Student', 'SocietyHead', 'Admin'

-- Societies (SocietyID, Name, Description, HeadUserID, Status, CreatedAt)
-- Status values: 'Pending', 'Active', 'Suspended'

-- Memberships (MembershipID, UserID, SocietyID, Status, AppliedAt)
-- Status values: 'Pending', 'Approved', 'Rejected'

-- Events (EventID, SocietyID, Title, Description, EventDate, Venue, Status, CreatedAt)
-- Status values: 'Pending', 'Approved', 'Cancelled'

-- EventRegistrations (RegistrationID, EventID, UserID, RegisteredAt)

-- Tasks (TaskID, SocietyID, AssignedTo, Title, Description, DueDate, Status, CreatedAt)
-- Status values: 'Pending', 'InProgress', 'Completed'

-- Announcements (AnnouncementID, SocietyID, Title, Content, PostedAt)
```

---

## Project Structure

```
SMM-PROJ/
├── Database/
│   └── DBConnection.cs          -- Singleton DB connection helper
├── Models/
│   ├── User.cs
│   ├── Society.cs
│   ├── Event.cs
│   ├── Membership.cs
│   ├── Task.cs
│   └── Announcement.cs
├── Forms/
│   ├── LoginForm.cs             -- Entry point
│   ├── RegisterForm.cs
│   ├── Student/
│   │   ├── StudentDashboard.cs
│   │   ├── BrowseSocieties.cs
│   │   ├── MyMemberships.cs
│   │   ├── BrowseEvents.cs
│   │   └── MyTickets.cs
│   ├── Society/
│   │   ├── SocietyDashboard.cs
│   │   ├── ManageMembers.cs
│   │   ├── ManageEvents.cs
│   │   ├── ManageTasks.cs
│   │   └── SocietyReports.cs
│   └── Admin/
│       ├── AdminDashboard.cs
│       ├── ManageUsers.cs
│       ├── ManageSocieties.cs
│       ├── ApproveEvents.cs
│       └── AdminReports.cs
└── Program.cs
```

---

## Database Connection

**File: `Database/DBConnection.cs`**

```csharp
// Use this connection string — user will adjust server name
string connectionString = "Server=.\\SQLEXPRESS;Database=SocietiesManagementSystem;Integrated Security=True;";

// Provide static helper methods:
// - GetConnection() → returns open SqlConnection
// - ExecuteNonQuery(string query, SqlParameter[] params) → int rows affected
// - ExecuteReader(string query, SqlParameter[] params) → DataTable
// - ExecuteScalar(string query, SqlParameter[] params) → object
```

---

## Session Management

**Create a static `Session` class** to hold the currently logged-in user:

```csharp
public static class Session {
    public static int UserID { get; set; }
    public static string FullName { get; set; }
    public static string Email { get; set; }
    public static string Role { get; set; }  // 'Student', 'SocietyHead', 'Admin'
    public static int? SocietyID { get; set; } // for SocietyHead only

    public static void Clear() { /* reset all */ }
}
```

---

## Forms Implementation

### 1. LoginForm.cs
**Purpose**: Entry point of the application.

**UI Elements**:
- Title label: "Societies Management System"
- TextBox: Email
- TextBox: Password (PasswordChar = '*')
- Button: Login
- LinkLabel: "Don't have an account? Register"

**Logic**:
- Query Users table: `SELECT * FROM Users WHERE Email=@Email AND Password=@Password`
- On success: set Session values, then open correct dashboard based on Role:
  - 'Student' → StudentDashboard
  - 'SocietyHead' → SocietyDashboard
  - 'Admin' → AdminDashboard
- On failure: MessageBox.Show("Invalid email or password.")
- Close LoginForm after opening dashboard

---

### 2. RegisterForm.cs
**Purpose**: New student account registration.

**UI Elements**:
- TextBox: Full Name
- TextBox: Email
- TextBox: Password
- ComboBox: Role (only show 'Student' and 'SocietyHead' — Admin cannot self-register)
- Button: Register
- LinkLabel: Back to Login

**Logic**:
- Validate all fields not empty
- Check email not already taken: `SELECT COUNT(*) FROM Users WHERE Email=@Email`
- Insert: `INSERT INTO Users (FullName, Email, Password, Role) VALUES (@FullName, @Email, @Password, @Role)`
- On success: MessageBox "Registration successful!", go back to LoginForm

---

### 3. StudentDashboard.cs
**Purpose**: Main hub for students after login.

**UI Elements**:
- Welcome label: "Welcome, [FullName]"
- Tab control OR side panel with navigation buttons:
  - Browse Societies
  - My Memberships
  - Browse Events
  - My Tickets / Passes
- Logout button

**Logic**:
- Each nav button opens the corresponding child form (or loads a panel)
- Logout: Session.Clear(), show LoginForm, close dashboard

---

### 4. BrowseSocieties.cs
**Purpose**: Students can view all active societies and apply for membership.

**UI Elements**:
- DataGridView: shows all Active societies (SocietyID hidden, Name, Description, Status)
- Button: Apply for Membership (enabled when a row is selected)
- Label: status messages

**Logic**:
- Load: `SELECT SocietyID, Name, Description, Status FROM Societies WHERE Status='Active'`
- On Apply:
  - Check not already a member: `SELECT COUNT(*) FROM Memberships WHERE UserID=@UID AND SocietyID=@SID AND Status != 'Rejected'`
  - If not: `INSERT INTO Memberships (UserID, SocietyID, Status) VALUES (@UID, @SID, 'Pending')`
  - Show "Membership request sent!"

---

### 5. MyMemberships.cs
**Purpose**: Student views their membership statuses.

**UI Elements**:
- DataGridView: Society Name, Status (Pending/Approved/Rejected), Applied Date
- Label: "Your Memberships"

**Logic**:
- `SELECT s.Name, m.Status, m.AppliedAt FROM Memberships m JOIN Societies s ON m.SocietyID=s.SocietyID WHERE m.UserID=@UID`

---

### 6. BrowseEvents.cs
**Purpose**: Students view upcoming approved events and register.

**UI Elements**:
- DataGridView: Event Title, Society Name, Date, Venue, Status
- Button: Register for Event
- Filter: show only future events

**Logic**:
- Load: `SELECT e.EventID, e.Title, s.Name AS Society, e.EventDate, e.Venue FROM Events e JOIN Societies s ON e.SocietyID=s.SocietyID WHERE e.Status='Approved' AND e.EventDate >= GETDATE()`
- On Register:
  - Check not already registered
  - `INSERT INTO EventRegistrations (EventID, UserID) VALUES (@EID, @UID)`
  - Show "Registered successfully! Your ticket has been saved."

---

### 7. MyTickets.cs
**Purpose**: Student views their registered event passes.

**UI Elements**:
- DataGridView: Event Title, Society, Date, Venue, Registered At
- Label: "Your Event Passes"

**Logic**:
- `SELECT e.Title, s.Name AS Society, e.EventDate, e.Venue, er.RegisteredAt FROM EventRegistrations er JOIN Events e ON er.EventID=e.EventID JOIN Societies s ON e.SocietyID=s.SocietyID WHERE er.UserID=@UID`

---

### 8. SocietyDashboard.cs
**Purpose**: Main hub for Society Heads.

**UI Elements**:
- Welcome label: "Welcome, [FullName] — [Society Name]"
- Navigation buttons:
  - Manage Members
  - Manage Events
  - Manage Tasks
  - Reports
- Logout button

**Logic**:
- On load: fetch SocietyID for this head from Societies table
  - `SELECT SocietyID, Name FROM Societies WHERE HeadUserID=@UID AND Status='Active'`
  - Store in Session.SocietyID
- If no society found: show "Your society is pending admin approval."

---

### 9. ManageMembers.cs
**Purpose**: Society head approves/rejects membership requests.

**UI Elements**:
- DataGridView: Member Name, Email, Status, Applied Date
- Button: Approve
- Button: Reject
- Filter ComboBox: All / Pending / Approved / Rejected

**Logic**:
- Load: `SELECT m.MembershipID, u.FullName, u.Email, m.Status, m.AppliedAt FROM Memberships m JOIN Users u ON m.UserID=u.UserID WHERE m.SocietyID=@SID`
- Approve: `UPDATE Memberships SET Status='Approved' WHERE MembershipID=@MID`
- Reject: `UPDATE Memberships SET Status='Rejected' WHERE MembershipID=@MID`
- Refresh grid after each action

---

### 10. ManageEvents.cs
**Purpose**: Society head creates, updates, and cancels events.

**UI Elements**:
- DataGridView: Event Title, Date, Venue, Status
- Button: Create New Event → opens popup/panel with form fields
- Button: Cancel Event (sets status to 'Cancelled')
- Input fields (shown when creating): Title, Description, EventDate (DateTimePicker), Venue

**Logic**:
- Load: `SELECT * FROM Events WHERE SocietyID=@SID`
- Create: `INSERT INTO Events (SocietyID, Title, Description, EventDate, Venue, Status) VALUES (@SID, @Title, @Desc, @Date, @Venue, 'Pending')`
  - Note: new events go to 'Pending' — Admin must approve
- Cancel: `UPDATE Events SET Status='Cancelled' WHERE EventID=@EID`

---

### 11. ManageTasks.cs
**Purpose**: Assign tasks to approved society members.

**UI Elements**:
- DataGridView: Task Title, Assigned To, Due Date, Status
- Button: Assign New Task → popup form
- Form fields: Title, Description, Assigned Member (ComboBox of approved members), Due Date
- Button: Mark as Completed

**Logic**:
- Load members: `SELECT u.UserID, u.FullName FROM Memberships m JOIN Users u ON m.UserID=u.UserID WHERE m.SocietyID=@SID AND m.Status='Approved'`
- Load tasks: `SELECT t.TaskID, t.Title, u.FullName AS AssignedTo, t.DueDate, t.Status FROM Tasks t JOIN Users u ON t.AssignedTo=u.UserID WHERE t.SocietyID=@SID`
- Create task: `INSERT INTO Tasks (SocietyID, AssignedTo, Title, Description, DueDate, Status) VALUES (...)`
- Complete: `UPDATE Tasks SET Status='Completed' WHERE TaskID=@TID`

---

### 12. SocietyReports.cs
**Purpose**: Society generates member and event reports.

**UI Elements**:
- Two GroupBoxes: "Member Report" and "Event Report"
- DataGridView for each
- Button: Refresh / Generate Report
- Label showing counts: "Total Members: X", "Total Events: Y"

**Logic**:
- Members: `SELECT u.FullName, u.Email, m.Status, m.AppliedAt FROM Memberships m JOIN Users u ON m.UserID=u.UserID WHERE m.SocietyID=@SID AND m.Status='Approved'`
- Events: `SELECT Title, EventDate, Venue, Status FROM Events WHERE SocietyID=@SID`

---

### 13. AdminDashboard.cs
**Purpose**: Main hub for Admin.

**UI Elements**:
- Welcome label: "Admin Panel"
- Navigation buttons:
  - Manage Users
  - Manage Societies
  - Approve Events
  - University Reports
- Summary stats on dashboard: Total Users, Total Societies, Pending Events
- Logout button

**Logic**:
- Load stats on form load:
  - `SELECT COUNT(*) FROM Users`
  - `SELECT COUNT(*) FROM Societies WHERE Status='Active'`
  - `SELECT COUNT(*) FROM Events WHERE Status='Pending'`

---

### 14. ManageUsers.cs
**Purpose**: Admin manages all student accounts.

**UI Elements**:
- DataGridView: UserID, Full Name, Email, Role, Created Date
- Button: Delete User
- Search TextBox: filter by name or email
- Button: Search

**Logic**:
- Load: `SELECT UserID, FullName, Email, Role, CreatedAt FROM Users WHERE Role != 'Admin'`
- Search: `SELECT ... WHERE FullName LIKE @q OR Email LIKE @q`
- Delete: `DELETE FROM Users WHERE UserID=@UID`
  - First show confirmation: "Are you sure you want to delete this user?"

---

### 15. ManageSocieties.cs
**Purpose**: Admin creates, approves, suspends, or deletes societies.

**UI Elements**:
- DataGridView: Society Name, Head Name, Status, Created Date
- Buttons: Approve, Suspend, Delete
- Button: Create New Society → popup form (Name, Description, select Head from SocietyHead users)

**Logic**:
- Load: `SELECT s.SocietyID, s.Name, u.FullName AS Head, s.Status, s.CreatedAt FROM Societies s JOIN Users u ON s.HeadUserID=u.UserID`
- Approve: `UPDATE Societies SET Status='Active' WHERE SocietyID=@SID`
- Suspend: `UPDATE Societies SET Status='Suspended' WHERE SocietyID=@SID`
- Delete: `DELETE FROM Societies WHERE SocietyID=@SID`
- Create: `INSERT INTO Societies (Name, Description, HeadUserID, Status) VALUES (@Name, @Desc, @HeadID, 'Pending')`

---

### 16. ApproveEvents.cs
**Purpose**: Admin approves or rejects event requests from societies.

**UI Elements**:
- DataGridView: Event Title, Society Name, Date, Venue, Status
- Button: Approve
- Button: Reject (sets status to 'Cancelled')
- Filter: show Pending events by default

**Logic**:
- Load: `SELECT e.EventID, e.Title, s.Name AS Society, e.EventDate, e.Venue, e.Status FROM Events e JOIN Societies s ON e.SocietyID=s.SocietyID WHERE e.Status='Pending'`
- Approve: `UPDATE Events SET Status='Approved' WHERE EventID=@EID`
- Reject: `UPDATE Events SET Status='Cancelled' WHERE EventID=@EID`

---

### 17. AdminReports.cs
**Purpose**: University-wide reporting.

**UI Elements**:
- TabControl with tabs:
  - "All Members" — DataGridView with all memberships
  - "All Events" — DataGridView with all events
  - "Society Performance" — DataGridView: Society Name, Member Count, Event Count
- Button: Refresh each tab

**Logic**:
- All Members: `SELECT u.FullName, u.Email, s.Name AS Society, m.Status FROM Memberships m JOIN Users u ON m.UserID=u.UserID JOIN Societies s ON m.SocietyID=s.SocietyID`
- All Events: `SELECT e.Title, s.Name AS Society, e.EventDate, e.Venue, e.Status FROM Events e JOIN Societies s ON e.SocietyID=s.SocietyID`
- Performance: `SELECT s.Name, COUNT(DISTINCT m.UserID) AS Members, COUNT(DISTINCT e.EventID) AS Events FROM Societies s LEFT JOIN Memberships m ON s.SocietyID=m.SocietyID AND m.Status='Approved' LEFT JOIN Events e ON s.SocietyID=e.SocietyID AND e.Status='Approved' GROUP BY s.Name`

---

## General Implementation Rules

1. **Every form must have a Logout button** that calls `Session.Clear()` and returns to `LoginForm`.

2. **All DataGridViews** should:
   - Set `ReadOnly = true`
   - Set `SelectionMode = FullRowSelect`
   - Hide ID columns (set `Visible = false` on ID columns but keep them in the DataTable for use in queries)
   - Auto-resize columns: `AutoSizeColumnsMode = Fill`

3. **Error handling**: Wrap all DB calls in try-catch. Show `MessageBox.Show(ex.Message)` on error.

4. **Refresh after every write operation** (Insert/Update/Delete) — reload the DataGridView immediately.

5. **Input validation**: Check for empty fields before any DB insert/update. Show friendly error messages.

6. **Connection string location**: Define it once in `DBConnection.cs`. Do not hardcode it elsewhere.

7. **Form navigation**: Use `form.Show()` then `this.Hide()` (not `this.Close()`) when navigating between major screens, so the app doesn't exit. Only use `this.Close()` on sub-forms/popups.

8. **Popup forms** (for Create New Event, Assign Task, etc.): Use `ShowDialog()` so they are modal. Pass data back via public properties or constructor parameters.

9. **Comments**: Add XML doc comments (`/// <summary>`) to every method. Add inline comments for every SQL query explaining what it does. This is required for the Documentation Ratio metric.

10. **Naming conventions**:
    - Classes: PascalCase
    - Methods: PascalCase
    - Variables: camelCase
    - Constants: ALL_CAPS

---

## Sample Data to Insert After Setup

```sql
-- Sample Society Heads
INSERT INTO Users (FullName, Email, Password, Role) VALUES 
('Ali Hassan', 'ali@fast.edu.pk', 'pass123', 'SocietyHead'),
('Sara Khan', 'sara@fast.edu.pk', 'pass123', 'SocietyHead');

-- Sample Students
INSERT INTO Users (FullName, Email, Password, Role) VALUES 
('Ahmed Raza', 'ahmed@fast.edu.pk', 'pass123', 'Student'),
('Fatima Malik', 'fatima@fast.edu.pk', 'pass123', 'Student'),
('Usman Tariq', 'usman@fast.edu.pk', 'pass123', 'Student');

-- Sample Societies (HeadUserID 2 = Ali, 3 = Sara)
INSERT INTO Societies (Name, Description, HeadUserID, Status) VALUES 
('Gaming Society', 'For gaming enthusiasts', 2, 'Active'),
('Developers Club', 'For software developers', 3, 'Active'),
('Literary Society', 'For book lovers and writers', 2, 'Pending');

-- Sample Events
INSERT INTO Events (SocietyID, Title, Description, EventDate, Venue, Status) VALUES 
(1, 'Gaming Tournament 2025', 'Annual gaming competition', '2025-06-15', 'CS Lab 1', 'Approved'),
(2, 'Hackathon Spring', '24-hour coding competition', '2025-07-01', 'Auditorium', 'Pending');
```

---

## Cursor Prompt Tips

When using this file in Cursor, use these prompts:

- *"Implement DBConnection.cs based on the requirements.md"*
- *"Implement LoginForm.cs with role-based redirect as described"*
- *"Implement StudentDashboard.cs with navigation panel"*
- *"Implement ManageMembers.cs for the Society Head role"*

Implement one file at a time for best results.
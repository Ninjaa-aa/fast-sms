# Task 2 — Cyclomatic Complexity & Test Cases

> **Project:** Societies Management System (SMM-PROJ)
> **Scope:** All methods across all `.cs` files, **excluding** Designer.cs auto-generated files
> **Formula:** CC = 1 + (number of decision points: `if`, `else if`, `switch case`, `for`, `foreach`, `while`, `catch`, `&&`, `||`, `?:` ternary)

---

## Complete Cyclomatic Complexity Table

### 1. Config — `EnvConfig`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 1 | `Load()` | `if` (1) | **2** | **TC1:** CONNECTION_STRING env var is set → uses full string. **TC2:** CONNECTION_STRING is empty → builds from DB_SERVER / DB_NAME. |
| 2 | `EnsureDatabaseInConnectionString()` | `\|\|` (1), `if` (1), `?:` (1) | **4** | **TC1:** String contains "Initial Catalog=" → returned unchanged. **TC2:** String contains "Database=" → returned unchanged. **TC3:** String lacks catalog and ends with `;` → catalog appended with space separator. **TC4:** String lacks catalog and does NOT end with `;` → catalog appended with `;` separator. |

---

### 2. DAL — `DBHelper`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 3 | `GetConnection()` | 0 | **1** | **TC1:** Valid connection string → returns open SqlConnection. |
| 4 | `ExecuteNonQuery()` | 0 | **1** | **TC1:** Valid INSERT query with parameters → returns rows affected > 0. |
| 5 | `ExecuteReader()` | 0 | **1** | **TC1:** Valid SELECT query → returns populated DataTable. |
| 6 | `ExecuteScalar()` | 0 | **1** | **TC1:** Valid COUNT query → returns scalar value. |

---

### 3. DAL — `UserDAL`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 7 | `GetByEmail()` | `if` (1) | **2** | **TC1:** Email exists → returns User object. **TC2:** Email not found → returns null. |
| 8 | `EmailExists()` | 0 | **1** | **TC1:** Email "test@test.com" exists → returns true. |
| 9 | `Register()` | 0 | **1** | **TC1:** Valid User object → inserts and returns true. |
| 10 | `GetAll()` | 0 | **1** | **TC1:** DB has non-Admin users → returns DataTable with rows. |
| 11 | `Search()` | 0 | **1** | **TC1:** Search term "john" → returns matching rows. |
| 12 | `Delete()` | 0 | **1** | **TC1:** Valid userId → deletes row, returns true. |
| 13 | `GetCount()` | 0 | **1** | **TC1:** DB has users → returns integer count. |
| 14 | `GetSocietyHeads()` | 0 | **1** | **TC1:** SocietyHead users exist → returns DataTable. |
| 15 | `MapUser()` | 0 | **1** | **TC1:** Valid DataRow → returns correctly mapped User. |

---

### 4. DAL — `SocietyDAL`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 16 | `GetActive()` | 0 | **1** | **TC1:** Active societies exist → returns DataTable. |
| 17 | `GetAll()` | 0 | **1** | **TC1:** Societies exist → returns joined DataTable with head names. |
| 18 | `GetByHead()` | 0 | **1** | **TC1:** Head user ID valid → returns society DataTable. |
| 19 | `GetActiveCount()` | 0 | **1** | **TC1:** Active societies exist → returns count. |
| 20 | `Create()` | 0 | **1** | **TC1:** Valid name, desc, headId → inserts and returns true. |
| 21 | `Approve()` | 0 | **1** | **TC1:** Valid societyId → sets status to Active. |
| 22 | `Suspend()` | 0 | **1** | **TC1:** Valid societyId → sets status to Suspended. |
| 23 | `Delete()` | 0 | **1** | **TC1:** Valid societyId → deletes row. |
| 24 | `GetPerformanceSummary()` | 0 | **1** | **TC1:** Societies exist → returns aggregated member/event counts. |

---

### 5. DAL — `MembershipDAL`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 25 | `HasApplied()` | 0 | **1** | **TC1:** User has pending/approved membership → returns true. |
| 26 | `Apply()` | 0 | **1** | **TC1:** Valid userId, societyId → inserts pending row. |
| 27 | `GetByUser()` | 0 | **1** | **TC1:** User has memberships → returns joined DataTable. |
| 28 | `GetBySociety()` | 0 | **1** | **TC1:** Society has members → returns DataTable. |
| 29 | `GetBySocietyFiltered()` | 0 | **1** | **TC1:** Society has 'Approved' members → returns filtered rows. |
| 30 | `Approve()` | 0 | **1** | **TC1:** Valid membershipId → sets status to Approved. |
| 31 | `Reject()` | 0 | **1** | **TC1:** Valid membershipId → sets status to Rejected. |
| 32 | `GetApprovedMembers()` | 0 | **1** | **TC1:** Approved members exist → returns UserID + FullName table. |
| 33 | `GetAll()` | 0 | **1** | **TC1:** Memberships exist → returns full joined table. |

---

### 6. DAL — `EventDAL`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 34 | `GetUpcomingApproved()` | 0 | **1** | **TC1:** Future approved events exist → returns DataTable. |
| 35 | `IsRegistered()` | 0 | **1** | **TC1:** User registered for event → returns true. |
| 36 | `Register()` | 0 | **1** | **TC1:** Valid eventId, userId → inserts with ticket code. |
| 37 | `GetTicketsByUser()` | 0 | **1** | **TC1:** User has tickets → returns DataTable. |
| 38 | `GetBySociety()` | 0 | **1** | **TC1:** Society has events → returns DataTable. |
| 39 | `Create()` | 0 | **1** | **TC1:** Valid params → inserts Pending event. |
| 40 | `Cancel()` | 0 | **1** | **TC1:** Valid eventId → sets status to Cancelled. |
| 41 | `GetPending()` | 0 | **1** | **TC1:** Pending events exist → returns joined DataTable. |
| 42 | `Approve()` | 0 | **1** | **TC1:** Valid eventId → sets status to Approved. |
| 43 | `Reject()` | 0 | **1** | **TC1:** Valid eventId → sets status to Cancelled. |
| 44 | `GetPendingCount()` | 0 | **1** | **TC1:** Pending events exist → returns count. |
| 45 | `GetAll()` | 0 | **1** | **TC1:** Events exist → returns full joined table. |

---

### 7. DAL — `TaskDAL`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 46 | `GetBySociety()` | 0 | **1** | **TC1:** Society has tasks → returns DataTable. |
| 47 | `Create()` | 0 | **1** | **TC1:** Valid params → inserts Pending task. |
| 48 | `MarkComplete()` | 0 | **1** | **TC1:** Valid taskId → sets status to Completed. |

---

### 8. DAL — `AnnouncementDAL`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 49 | `GetBySociety()` | 0 | **1** | **TC1:** Society has announcements → returns DataTable. |
| 50 | `Create()` | 0 | **1** | **TC1:** Valid params → inserts announcement. |

---

### 9. Helpers — `Session`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 51 | `Clear()` | 0 | **1** | **TC1:** Session has values → all properties reset to defaults. |

---

### 10. Helpers — `PasswordHasher`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 52 | `Hash()` | 0 | **1** | **TC1:** Plain-text password "P@ss1" → returns BCrypt hash string. |
| 53 | `Verify()` | 0 | **1** | **TC1:** Correct password + matching hash → returns true. |

---

### 11. Entry — `Program`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 54 | `Main()` | 0 | **1** | **TC1:** Application starts → EnvConfig loaded, LoginForm displayed. |

---

### 12. Forms/Auth — `LoginForm`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 55 | `LoginForm()` *(ctor)* | 0 | **1** | **TC1:** Constructor called → form initialises correctly. |
| 56 | `BtnLogin_Click()` | `if`+`\|\|` (2), `if`+`\|\|` (2), `switch` 3 cases (3), `catch` (1) | **9** | **TC1:** Empty email field → validation warning shown. **TC2:** Empty password field → validation warning shown. **TC3:** Email not found in DB (user == null) → "Invalid email or password". **TC4:** Email found but wrong password → "Invalid email or password". **TC5:** Valid Student login → StudentDashboard opens. **TC6:** Valid SocietyHead login → SocietyDashboard opens. **TC7:** Valid Admin login → AdminDashboard opens. **TC8:** Unknown role in DB → InvalidOperationException thrown. **TC9:** Database connection fails → catch block shows error. |
| 57 | `LnkRegister_LinkClicked()` | 0 | **1** | **TC1:** Link clicked → RegisterForm shown, LoginForm hidden. |

---

### 13. Forms/Auth — `RegisterForm`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 58 | `RegisterForm()` *(ctor)* | 0 | **1** | **TC1:** Constructor called → form initialises correctly. |
| 59 | `BtnRegister_Click()` | `if`+`\|\|`+`\|\|` (3), `if` (1), `catch` (1), `if` (1), `if` (1) | **8** | **TC1:** Empty FullName → validation warning. **TC2:** Empty Email → validation warning. **TC3:** Empty Password → validation warning. **TC4:** Password ≠ ConfirmPassword → "Passwords do not match". **TC5:** Email already exists → "account already exists" warning. **TC6:** Registration succeeds → success message, navigate to login. **TC7:** Registration fails (DB returns false) → "Registration failed" error. **TC8:** Database exception thrown → catch block shows error. |
| 60 | `LnkLogin_LinkClicked()` | 0 | **1** | **TC1:** Link clicked → navigates to login. |
| 61 | `NavigateToLogin()` | 0 | **1** | **TC1:** Called → LoginForm created and shown, current form closed. |

---

### 14. Forms/Student — `StudentDashboard`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 62 | `StudentDashboard()` *(ctor)* | 0 | **1** | **TC1:** Constructor → welcome label shows Session.FullName. |
| 63 | `BtnBrowseSocieties_Click()` | 0 | **1** | **TC1:** Click → BrowseSocieties shown, dashboard hidden. |
| 64 | `BtnMyMemberships_Click()` | 0 | **1** | **TC1:** Click → MyMemberships shown, dashboard hidden. |
| 65 | `BtnBrowseEvents_Click()` | 0 | **1** | **TC1:** Click → BrowseEvents shown, dashboard hidden. |
| 66 | `BtnMyTickets_Click()` | 0 | **1** | **TC1:** Click → MyTickets shown, dashboard hidden. |
| 67 | `BtnLogout_Click()` | 0 | **1** | **TC1:** Click → Session cleared, LoginForm shown, dashboard closed. |

---

### 15. Forms/Student — `BrowseSocieties`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 68 | `BrowseSocieties()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 69 | `BrowseSocieties_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadSocieties(). |
| 70 | `LoadSocieties()` | `catch` (1), `if` (1) | **3** | **TC1:** Active societies loaded, grid has SocietyID column → column hidden. **TC2:** Active societies loaded, no SocietyID column → grid shown as-is. **TC3:** Database error → catch shows error message. |
| 71 | `BtnApply_Click()` | `if` (1), `catch` (1), `if` (1), `if` (1) | **5** | **TC1:** No row selected → "Please select a society" warning. **TC2:** Already applied → orange label "already applied". **TC3:** Apply succeeds → green label "request sent". **TC4:** Apply fails (returns false) → no label change. **TC5:** Database error → catch shows error message. |
| 72 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → StudentDashboard shown, form closed. |

---

### 16. Forms/Student — `MyMemberships`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 73 | `MyMemberships()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 74 | `MyMemberships_Load()` | `catch` (1) | **2** | **TC1:** Load succeeds → grid shows memberships. **TC2:** Database error → catch shows error message. |
| 75 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → StudentDashboard shown, form closed. |

---

### 17. Forms/Student — `BrowseEvents`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 76 | `BrowseEvents()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 77 | `BrowseEvents_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadEvents(). |
| 78 | `LoadEvents()` | `catch` (1), `if` (1) | **3** | **TC1:** Events loaded, EventID column exists → column hidden. **TC2:** Events loaded, no EventID column → grid shown as-is. **TC3:** Database error → catch shows error. |
| 79 | `BtnRegister_Click()` | `if` (1), `catch` (1), `if` (1), `if` (1) | **5** | **TC1:** No row selected → "Please select an event" warning. **TC2:** Already registered → orange label "already registered". **TC3:** Registration succeeds → green label "Registered successfully". **TC4:** Registration fails → no label change. **TC5:** Database error → catch shows error. |
| 80 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → StudentDashboard shown, form closed. |

---

### 18. Forms/Student — `MyTickets`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 81 | `MyTickets()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 82 | `MyTickets_Load()` | `catch` (1) | **2** | **TC1:** Load succeeds → grid shows tickets. **TC2:** Database error → catch shows error. |
| 83 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → StudentDashboard shown, form closed. |

---

### 19. Forms/Society — `SocietyDashboard`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 84 | `SocietyDashboard()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 85 | `SocietyDashboard_Load()` | `catch` (1), `foreach` (1), `if` (1), `if` (1) | **5** | **TC1:** Head has an active society → SocietyID set, welcome label shows society name, nav buttons enabled. **TC2:** Head has no active society (pending) → "pending admin approval" label, nav buttons disabled. **TC3:** Head has multiple societies, first is inactive, second is Active → finds active row. **TC4:** Head has no societies at all (empty DataTable) → pending message. **TC5:** Database error → catch shows error. |
| 86 | `EnableNavButtons()` | 0 | **1** | **TC1:** enabled=true → all navigation buttons enabled. |
| 87 | `BtnManageMembers_Click()` | 0 | **1** | **TC1:** Click → ManageMembers shown, dashboard hidden. |
| 88 | `BtnManageEvents_Click()` | 0 | **1** | **TC1:** Click → ManageEvents shown, dashboard hidden. |
| 89 | `BtnManageTasks_Click()` | 0 | **1** | **TC1:** Click → ManageTasks shown, dashboard hidden. |
| 90 | `BtnReports_Click()` | 0 | **1** | **TC1:** Click → SocietyReports shown, dashboard hidden. |
| 91 | `BtnLogout_Click()` | 0 | **1** | **TC1:** Click → Session cleared, LoginForm shown. |

---

### 20. Forms/Society — `ManageMembers`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 92 | `ManageMembers()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 93 | `ManageMembers_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadMembers(). |
| 94 | `LoadMembers()` | `catch` (1), `if` (1), `?:` (1), `if` (1) | **5** | **TC1:** SocietyID is null → returns immediately. **TC2:** Filter = "All" → loads all members. **TC3:** Filter = "Pending" → loads filtered members. **TC4:** Grid has MembershipID column → column hidden. **TC5:** Database error → catch shows error. |
| 95 | `CmbFilter_SelectedIndexChanged()` | 0 | **1** | **TC1:** Filter changed → calls LoadMembers(). |
| 96 | `BtnApprove_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** No row selected → returns immediately. **TC2:** Row selected → membership approved, grid reloaded. **TC3:** Database error → catch shows error. |
| 97 | `BtnReject_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** No row selected → returns immediately. **TC2:** Row selected → membership rejected, grid reloaded. **TC3:** Database error → catch shows error. |
| 98 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → SocietyDashboard shown, form closed. |

---

### 21. Forms/Society — `ManageEvents`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 99 | `ManageEvents()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 100 | `ManageEvents_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadEvents(). |
| 101 | `LoadEvents()` | `catch` (1), `if` (1), `if` (1) | **4** | **TC1:** SocietyID null → returns immediately. **TC2:** Events loaded, EventID column exists → column hidden. **TC3:** Events loaded successfully without EventID column. **TC4:** Database error → catch shows error. |
| 102 | `BtnCreate_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** Dialog cancelled → no action. **TC2:** Dialog OK → event created, grid reloaded. **TC3:** Database error during creation → catch shows error. |
| 103 | `BtnCancelEvent_Click()` | `if` (1), `if` (1), `catch` (1) | **4** | **TC1:** No row selected → returns immediately. **TC2:** Confirmation dialog → user clicks No → no action. **TC3:** Confirmation → Yes → event cancelled, grid reloaded. **TC4:** Database error → catch shows error. |
| 104 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → SocietyDashboard shown, form closed. |

---

### 22. Forms/Society — `CreateEventDialog`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 105 | `CreateEventDialog()` *(ctor + validation lambda)* | `if`+`\|\|` (2) | **3** | **TC1:** Title and Venue provided → properties set, DialogResult.OK. **TC2:** Title empty → validation warning, DialogResult.None. **TC3:** Venue empty → validation warning, DialogResult.None. |

---

### 23. Forms/Society — `ManageTasks`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 106 | `ManageTasks()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 107 | `ManageTasks_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadTasks(). |
| 108 | `LoadTasks()` | `catch` (1), `if` (1), `if` (1) | **4** | **TC1:** SocietyID null → returns immediately. **TC2:** Tasks loaded, TaskID column exists → column hidden. **TC3:** Tasks loaded, no TaskID column. **TC4:** Database error → catch shows error. |
| 109 | `BtnAssign_Click()` | `if` (1), `if` (1), `catch` (1) | **4** | **TC1:** SocietyID null → returns immediately. **TC2:** Dialog cancelled → no action. **TC3:** Dialog OK → task created, grid reloaded. **TC4:** Database error → catch shows error. |
| 110 | `BtnComplete_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** No row selected → returns immediately. **TC2:** Row selected → task marked complete. **TC3:** Database error → catch shows error. |
| 111 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → SocietyDashboard shown, form closed. |

---

### 24. Forms/Society — `AssignTaskDialog`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 112 | `AssignTaskDialog()` *(ctor + validation lambda)* | `if`+`\|\|` (2) | **3** | **TC1:** Member selected and Title provided → properties set. **TC2:** No member selected → validation warning. **TC3:** Title empty → validation warning. |

---

### 25. Forms/Society — `SocietyReports`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 113 | `SocietyReports()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 114 | `SocietyReports_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadReports(). |
| 115 | `LoadReports()` | `catch` (1), `if` (1), `if` (1), `if` (1) | **5** | **TC1:** SocietyID null → returns immediately. **TC2:** Reports loaded, both ID columns exist → hidden. **TC3:** Members loaded, UserID column missing → skipped. **TC4:** Events loaded, EventID column missing → skipped. **TC5:** Database error → catch shows error. |
| 116 | `BtnRefresh_Click()` | 0 | **1** | **TC1:** Click → calls LoadReports(). |
| 117 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → SocietyDashboard shown, form closed. |

---

### 26. Forms/Admin — `AdminDashboard`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 118 | `AdminDashboard()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 119 | `AdminDashboard_Load()` | `catch` (1) | **2** | **TC1:** Stats loaded → labels show counts. **TC2:** Database error → catch shows error. |
| 120 | `BtnManageUsers_Click()` | 0 | **1** | **TC1:** Click → ManageUsers shown, dashboard hidden. |
| 121 | `BtnManageSocieties_Click()` | 0 | **1** | **TC1:** Click → ManageSocieties shown, dashboard hidden. |
| 122 | `BtnApproveEvents_Click()` | 0 | **1** | **TC1:** Click → ApproveEvents shown, dashboard hidden. |
| 123 | `BtnReports_Click()` | 0 | **1** | **TC1:** Click → AdminReports shown, dashboard hidden. |
| 124 | `BtnLogout_Click()` | 0 | **1** | **TC1:** Click → Session cleared, LoginForm shown. |

---

### 27. Forms/Admin — `ManageUsers`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 125 | `ManageUsers()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 126 | `ManageUsers_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadUsers(). |
| 127 | `LoadUsers()` | `catch` (1), `if` (1) | **3** | **TC1:** Users loaded, UserID column hidden. **TC2:** Users loaded, no UserID column. **TC3:** Database error → catch shows error. |
| 128 | `BtnSearch_Click()` | `catch` (1), `?:` (1), `if` (1) | **4** | **TC1:** Empty search term → loads all users. **TC2:** Non-empty search term → loads filtered users. **TC3:** UserID column exists → hidden. **TC4:** Database error → catch shows error. |
| 129 | `BtnDelete_Click()` | `if` (1), `if` (1), `catch` (1) | **4** | **TC1:** No row selected → returns immediately. **TC2:** Confirmation → No → no action. **TC3:** Confirmation → Yes → user deleted, grid reloaded. **TC4:** Database error → catch shows error. |
| 130 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → AdminDashboard shown, form closed. |

---

### 28. Forms/Admin — `ManageSocieties`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 131 | `ManageSocieties()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 132 | `ManageSocieties_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadSocieties(). |
| 133 | `LoadSocieties()` | `catch` (1), `if` (1) | **3** | **TC1:** Societies loaded, SocietyID column hidden. **TC2:** No SocietyID column. **TC3:** Database error → catch shows error. |
| 134 | `BtnCreate_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** Dialog cancelled → no action. **TC2:** Dialog OK → society created, grid reloaded. **TC3:** Database error → catch shows error. |
| 135 | `BtnApprove_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** No row selected → returns immediately. **TC2:** Row selected → society approved. **TC3:** Database error → catch shows error. |
| 136 | `BtnSuspend_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** No row selected → returns immediately. **TC2:** Row selected → society suspended. **TC3:** Database error → catch shows error. |
| 137 | `BtnDelete_Click()` | `if` (1), `if` (1), `catch` (1) | **4** | **TC1:** No row selected → returns immediately. **TC2:** Confirmation → No → no action. **TC3:** Confirmation → Yes → society deleted. **TC4:** Database error → catch shows error. |
| 138 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → AdminDashboard shown, form closed. |

---

### 29. Forms/Admin — `CreateSocietyDialog`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 139 | `CreateSocietyDialog()` *(ctor + validation lambda)* | `if`+`\|\|` (2) | **3** | **TC1:** Name and Head provided → properties set. **TC2:** Name empty → validation warning. **TC3:** No head selected → validation warning. |

---

### 30. Forms/Admin — `ApproveEvents`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 140 | `ApproveEvents()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 141 | `ApproveEvents_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadPendingEvents(). |
| 142 | `LoadPendingEvents()` | `catch` (1), `if` (1) | **3** | **TC1:** Pending events loaded, EventID hidden. **TC2:** No EventID column. **TC3:** Database error → catch shows error. |
| 143 | `BtnApprove_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** No row selected → returns immediately. **TC2:** Row selected → event approved. **TC3:** Database error → catch shows error. |
| 144 | `BtnReject_Click()` | `if` (1), `catch` (1) | **3** | **TC1:** No row selected → returns immediately. **TC2:** Row selected → event rejected. **TC3:** Database error → catch shows error. |
| 145 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → AdminDashboard shown, form closed. |

---

### 31. Forms/Admin — `AdminReports`

| # | Method | Decision Points | CC | Test Cases |
|---|--------|----------------:|---:|------------|
| 146 | `AdminReports()` *(ctor)* | 0 | **1** | **TC1:** Constructor → form initialises. |
| 147 | `AdminReports_Load()` | 0 | **1** | **TC1:** Form loads → calls LoadReports(). |
| 148 | `LoadReports()` | `catch` (1) | **2** | **TC1:** Reports loaded into all three grids. **TC2:** Database error → catch shows error. |
| 149 | `BtnRefresh_Click()` | 0 | **1** | **TC1:** Click → calls LoadReports(). |
| 150 | `BtnBack_Click()` | 0 | **1** | **TC1:** Click → AdminDashboard shown, form closed. |

---

### Models — `User`, `Society`, `Event`, `Membership`, `TaskItem`, `Announcement`

All six model classes contain **zero methods** (auto-properties only). They have no cyclomatic complexity to analyse and are listed as **WMC = 0** in the CK Metrics (Task 4).

---

## Summary Statistics

| Metric | Value |
|--------|------:|
| **Total Methods Analysed** | **150** |
| **Total CC (sum)** | **249** |
| **Average CC** | **1.66** |
| **Median CC** | **1** |
| **Highest CC** | **9** — `LoginForm.BtnLogin_Click` |
| **Second Highest CC** | **8** — `RegisterForm.BtnRegister_Click` |
| **Lowest CC** | **1** — 105 methods (70.0%) |
| **Methods with CC = 1** | **105** (70.0%) — simple, straight-line execution |
| **Methods with CC 2–4** | **33** (22.0%) — low complexity |
| **Methods with CC 5–7** | **10** (6.7%) — moderate complexity |
| **Methods with CC 8–10** | **2** (1.3%) — high complexity |

### CC Distribution by Module

| Module | Methods | Sum CC | Avg CC |
|--------|--------:|-------:|-------:|
| Config (EnvConfig) | 2 | 6 | 3.00 |
| DAL (7 classes) | 50 | 60 | 1.20 |
| Helpers (Session, PasswordHasher) | 3 | 3 | 1.00 |
| Entry (Program) | 1 | 1 | 1.00 |
| Forms/Auth | 7 | 22 | 3.14 |
| Forms/Student | 16 | 32 | 2.00 |
| Forms/Society (incl. dialogs) | 34 | 72 | 2.12 |
| Forms/Admin (incl. dialog) | 31 | 53 | 1.71 |
| **Models** | **0** | **0** | **N/A** |

### Methods Potentially Needing Refactoring (CC ≥ 5)

| # | Class | Method | CC | Suggestion |
|---|-------|--------|----|------------|
| 1 | LoginForm | `BtnLogin_Click` | 9 | Extract role-dispatch logic into a separate method; split validation from authentication. |
| 2 | RegisterForm | `BtnRegister_Click` | 8 | Extract input validation into a dedicated `ValidateInput()` method. |
| 3 | BrowseSocieties | `BtnApply_Click` | 5 | Acceptable; could extract "already applied" check. |
| 4 | BrowseEvents | `BtnRegister_Click` | 5 | Acceptable; mirrors BrowseSocieties pattern. |
| 5 | SocietyDashboard | `SocietyDashboard_Load` | 5 | Acceptable; extract "find active society" into helper. |
| 6 | ManageMembers | `LoadMembers` | 5 | Acceptable; filter + null check + column hide. |
| 7 | SocietyReports | `LoadReports` | 5 | Acceptable; loads two grids with ID hiding. |

### Risk Interpretation

| CC Range | Risk Level | Count | % |
|----------|-----------|------:|--:|
| 1 | Trivial — no branching | 105 | 70.0% |
| 2–4 | Low — simple conditions | 33 | 22.0% |
| 5–10 | Moderate — consider review | 12 | 8.0% |
| 11–20 | High — refactor recommended | 0 | 0.0% |
| > 20 | Very High — must refactor | 0 | 0.0% |

**Overall Assessment:** The codebase has very low complexity overall (avg CC = 1.66). 92% of methods have CC ≤ 4, indicating a clean, well-structured design. Only two methods exceed CC = 5, both in authentication forms where validation and role-dispatch logic naturally increase branching. No method reaches the "high risk" threshold of CC > 10.

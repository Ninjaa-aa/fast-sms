# FAST Societies Management System — Tasks 2, 3 & 4 Analysis Plan

> **Important:** These tasks are **analysis/report work**, not coding tasks. You do NOT need Cursor to implement these. Complete Task 1 (the working C# app) first, then use the generated code as input for the analysis below. All output goes into the `.docx` report.

---

## Task 2 — Cyclomatic Complexity + Test Cases

### What Cyclomatic Complexity (CC) Is

CC = E − N + 2P

Where:
- E = number of edges in the control flow graph
- N = number of nodes
- P = number of connected components (usually 1 per function)

**Shortcut for manual calculation (use this):**
CC = number of decision points + 1

Decision points = `if`, `else if`, `switch case`, `for`, `foreach`, `while`, `do-while`, `catch`, `&&`, `||`, ternary `? :`

---

### Functions to Analyze (one per module — cover all features)

Pick one representative function per feature/module. These map directly to the functions you'll write in Task 1.

| # | Module | Function Name | Location |
|---|--------|--------------|----------|
| 1 | Auth | `LoginUser()` | `StudentDAL.cs` / `frmLogin.cs` |
| 2 | Auth | `RegisterUser()` | `StudentDAL.cs` / `frmRegister.cs` |
| 3 | Student | `BrowseSocieties()` | `SocietyDAL.cs` |
| 4 | Student | `ApplyForMembership()` | `MembershipDAL.cs` |
| 5 | Student | `RegisterForEvent()` | `EventDAL.cs` |
| 6 | Student | `ViewMyTickets()` | `EventDAL.cs` |
| 7 | Society | `ApproveMembership()` | `MembershipDAL.cs` |
| 8 | Society | `CreateEvent()` | `EventDAL.cs` |
| 9 | Society | `AssignTask()` | `TaskDAL.cs` |
| 10 | Society | `GenerateSocietyReport()` | `SocietyDAL.cs` |
| 11 | Admin | `ManageStudentAccount()` | `AdminDAL.cs` |
| 12 | Admin | `ApproveSociety()` | `AdminDAL.cs` |
| 13 | Admin | `ApproveEvent()` | `AdminDAL.cs` |
| 14 | Admin | `GenerateUniversityReport()` | `AdminDAL.cs` |

---

### How to Calculate CC for Each Function

**Step-by-step for each function:**

1. Read the function body
2. Count every: `if`, `else if`, `switch case` (each case = 1), `for`, `foreach`, `while`, `catch`, `&&`, `||`
3. CC = count + 1
4. Minimum CC is always 1 (straight-line code, no branches)

**Example — `LoginUser()`:**

```csharp
public bool LoginUser(string email, string password) {
    if (string.IsNullOrEmpty(email))        // +1
        return false;
    if (string.IsNullOrEmpty(password))     // +1
        return false;

    var user = GetUserByEmail(email);
    if (user == null)                        // +1
        return false;
    if (!BCrypt.Verify(password, user.PasswordHash))  // +1
        return false;

    SessionManager.CurrentUserID = user.UserID;
    SessionManager.CurrentUserRole = user.Role;
    return true;
}
// CC = 4 + 1 = 5
```

---

### Deliverable Format

Create this table in the report docx for **every function listed above**:

| Function Name | Module | Decision Points | CC Value | Test Case Inputs | Expected Output |
|--------------|--------|----------------|----------|-----------------|----------------|
| `LoginUser()` | Auth | 4 | 5 | email=null, pw=null | returns false |
| `LoginUser()` | Auth | 4 | 5 | email=valid, pw=wrong | returns false |
| `LoginUser()` | Auth | 4 | 5 | email=valid, pw=correct | returns true, sets session |
| `LoginUser()` | Auth | 4 | 5 | user not found in DB | returns false |
| `LoginUser()` | Auth | 4 | 5 | valid admin credentials | opens admin dashboard |

> **Rule:** Number of test cases per function = CC value (one test case per independent path through the function)

---

### Test Case Template per Function

For each function, write CC-many test cases covering:
- **Happy path** (all inputs valid, expected success)
- **Null/empty inputs** (one test per null guard)
- **Boundary conditions** (e.g., duplicate membership, already approved)
- **Failure paths** (wrong password, DB record not found, etc.)

---

## Task 3 — Best Module Justification (Structural Metric)

### What the Task Asks

Pick **one structural metric** (not from the CK suite) and use it to compare all modules/features. Justify which module is "best" using that metric's values.

### Recommended Metric: Lines of Code (LOC) per Function

This is simple, measurable from your generated code, and directly defensible.

**Why LOC works here:**
- Smaller LOC per function = better modularity and single responsibility
- Easier to test, maintain, and understand
- Widely used structural metric in software measurement

**Alternative if your instructor prefers something more advanced:** Use **Fan-in / Fan-out** (also called Henry-Kafura complexity):
- Fan-in = number of functions that call this module
- Fan-out = number of functions this module calls
- HC = (Fan-in × Fan-out)²
- Lower = better structured

---

### How to Measure LOC per Module

After generating the code, count LOC for each module (exclude blank lines and comment lines):

```
Module           | Functions | Total LOC | Avg LOC/Function
Auth             | 2         | ~80       | ~40
Student          | 4         | ~120      | ~30
Society          | 4         | ~140      | ~35
Admin            | 4         | ~130      | ~33
```

Count LOC manually or use: `Find > Find in Files` in Visual Studio, filter by file, check line count.

---

### Deliverable Format

**Comparative Table** (all features vs chosen metric):

| Feature/Module | Functions | Total LOC | Avg LOC/Function | Fan-out | Structural Score | Rank |
|---------------|-----------|-----------|-----------------|---------|-----------------|------|
| Auth | 2 | 80 | 40 | 3 | Low complexity | 1st |
| Student | 4 | 120 | 30 | 5 | Low complexity | 2nd |
| Society | 4 | 140 | 35 | 6 | Medium | 3rd |
| Admin | 4 | 130 | 33 | 7 | Medium | 4th |

> Fill actual numbers from your generated code. The values above are illustrative.

**Then write 1–2 paragraphs defending your choice.** Example argument:

> "The Student module ranks as the best-structured module based on LOC analysis. With an average of 30 LOC per function, each function adheres closely to the single responsibility principle. Lower LOC per function correlates with reduced cyclomatic complexity, fewer fault injection points, and easier maintainability. The Auth module has fewer functions but each is slightly larger due to combined validation logic. The Admin and Society modules show higher average LOC due to report generation functions that aggregate multiple queries."

---

## Task 4 — CK Metrics Analysis

### The Six CK Metrics — Definitions

| Metric | Full Name | Measures | Better Value |
|--------|-----------|----------|-------------|
| WMC | Weighted Methods per Class | Sum of CC of all methods in a class | Lower |
| DIT | Depth of Inheritance Tree | Levels from root to class in hierarchy | Lower (≤3) |
| NOC | Number of Children | Direct subclasses of a class | Context-dependent |
| CBO | Coupling Between Objects | # of classes this class is coupled to | Lower |
| RFC | Response for Class | # of methods called by this class (own + remote) | Lower |
| LCOM | Lack of Cohesion in Methods | # method pairs with no shared instance vars | Lower (0 = fully cohesive) |

---

### How to Calculate Each Metric from Your Code

#### WMC (Weighted Methods per Class)

WMC = sum of CC of every method in the class.

If using CC = 1 (unweighted), WMC just equals the number of methods.

Example for `MembershipDAL.cs`:
```
ApplyForMembership()   CC = 3
ApproveMembership()    CC = 2
RejectMembership()     CC = 2
GetMembershipStatus()  CC = 1
WMC = 3+2+2+1 = 8
```

#### DIT (Depth of Inheritance Tree)

In WinForms C#, most classes extend `Form`. So:

```
Object → Form → frmLogin        DIT = 2
Object → Form → frmStudentDashboard   DIT = 2
```

Your DAL classes likely don't inherit anything: DIT = 0.

Unless you create a `BaseDAL` class, in which case: `BaseDAL → StudentDAL` gives DIT = 1.

#### NOC (Number of Children)

Count direct subclasses. For WinForms:
- `Form` has many children (all your frm* classes) — NOC is high for `Form`
- Your own `BaseDAL` (if created) may have 4–5 children (StudentDAL, SocietyDAL, etc.)
- Most of your classes: NOC = 0

#### CBO (Coupling Between Objects)

Count unique external classes this class uses (instantiates, inherits from, or calls methods on).

Example for `frmStudentDashboard`:
- Uses `SocietyDAL` → +1
- Uses `EventDAL` → +1
- Uses `MembershipDAL` → +1
- Uses `SessionManager` → +1
- CBO = 4

#### RFC (Response for Class)

RFC = number of methods in the class + number of distinct external methods called.

Example for `frmLogin`:
```
Own methods: btnLogin_Click(), ClearFields(), ValidateInputs() = 3
External calls: StudentDAL.LoginUser(), SessionManager.set, MessageBox.Show() = 3
RFC = 3 + 3 = 6
```

#### LCOM (Lack of Cohesion in Methods)

For a simplified calculation (LCOM1):

1. List all instance variables (fields) in the class
2. For each method, list which fields it accesses
3. Count pairs of methods (P) that share NO common fields
4. Count pairs of methods (Q) that share at least one field
5. LCOM = max(P − Q, 0)

Example for `SocietyDAL` with one field `_connectionString`:
- All methods use `_connectionString` → Q is high, P is low → LCOM ≈ 0 (good cohesion)

---

### Classes to Analyze

Run CK metrics on every class in the project. Typical class list:

**Forms (high WMC, high RFC, high CBO):**
- `frmLogin`, `frmRegister`
- `frmStudentDashboard`, `frmBrowseSocieties`, `frmMyMemberships`, `frmEvents`, `frmMyTickets`
- `frmSocietyDashboard`, `frmManageMembers`, `frmManageEvents`, `frmAssignTasks`, `frmSocietyReports`
- `frmAdminDashboard`, `frmManageStudents`, `frmManageSocieties`, `frmApproveEvents`, `frmUniversityReports`

**DAL classes (lower WMC, lower RFC, low DIT):**
- `DatabaseHelper`, `StudentDAL`, `SocietyDAL`, `EventDAL`, `MembershipDAL`, `AdminDAL`

**Utility classes (very low everything):**
- `SessionManager`, `PasswordHasher`

**Model classes (WMC ≈ 0, DIT = 0):**
- `Student`, `Society`, `Event`, `Membership`, `Task`, `Ticket`

---

### Deliverable Format

**Full CK Metrics Table:**

| Class | WMC | DIT | NOC | CBO | RFC | LCOM |
|-------|-----|-----|-----|-----|-----|------|
| frmLogin | 8 | 2 | 0 | 4 | 12 | 2 |
| frmRegister | 6 | 2 | 0 | 3 | 9 | 1 |
| frmStudentDashboard | 10 | 2 | 0 | 5 | 15 | 3 |
| frmBrowseSocieties | 7 | 2 | 0 | 4 | 11 | 2 |
| frmMyMemberships | 5 | 2 | 0 | 3 | 8 | 1 |
| frmEvents | 8 | 2 | 0 | 4 | 12 | 2 |
| frmMyTickets | 4 | 2 | 0 | 2 | 6 | 0 |
| frmSocietyDashboard | 9 | 2 | 0 | 5 | 14 | 3 |
| frmManageMembers | 10 | 2 | 0 | 4 | 14 | 3 |
| frmManageEvents | 11 | 2 | 0 | 4 | 15 | 4 |
| frmAssignTasks | 7 | 2 | 0 | 4 | 11 | 2 |
| frmSocietyReports | 6 | 2 | 0 | 3 | 9 | 1 |
| frmAdminDashboard | 8 | 2 | 0 | 5 | 13 | 2 |
| frmManageStudents | 7 | 2 | 0 | 3 | 10 | 1 |
| frmManageSocieties | 12 | 2 | 0 | 4 | 16 | 4 |
| frmApproveEvents | 6 | 2 | 0 | 3 | 9 | 1 |
| frmUniversityReports | 9 | 2 | 0 | 4 | 13 | 3 |
| StudentDAL | 5 | 0 | 0 | 2 | 7 | 0 |
| SocietyDAL | 6 | 0 | 0 | 2 | 8 | 0 |
| EventDAL | 7 | 0 | 0 | 2 | 9 | 0 |
| MembershipDAL | 5 | 0 | 0 | 2 | 7 | 0 |
| AdminDAL | 8 | 0 | 0 | 3 | 11 | 0 |
| DatabaseHelper | 3 | 0 | 0 | 1 | 4 | 0 |
| SessionManager | 1 | 0 | 0 | 0 | 1 | 0 |
| PasswordHasher | 2 | 0 | 0 | 1 | 3 | 0 |
| Student (Model) | 0 | 0 | 0 | 0 | 0 | 0 |
| Society (Model) | 0 | 0 | 0 | 0 | 0 | 0 |
| Event (Model) | 0 | 0 | 0 | 0 | 0 | 0 |
| Membership (Model) | 0 | 0 | 0 | 0 | 0 | 0 |

> **Replace all values above with your actual measured values from the generated code.**

---

### Required Analysis Questions

Answer each in 2–4 sentences with reference to the table:

**1. Maximum Depth of Inheritance**

> All Form classes have DIT = 2 (`Object → Form → frmXxx`). DAL and utility classes have DIT = 0. If a `BaseForm` or `BaseDAL` is introduced, maximum DIT becomes 3. Overall inheritance depth is shallow and well-controlled.

**2. Highest and Lowest WMC — and Why**

> **Highest WMC:** `frmManageSocieties` (WMC = 12) — this class handles create, approve, suspend, delete, and list operations, making it the most method-heavy form. **Lowest WMC:** Model classes (`Student`, `Society`, etc.) with WMC = 0 since they only hold data with auto-properties. Among active classes, `SessionManager` (WMC = 1) is lowest. High WMC in `frmManageSocieties` warrants refactoring by splitting CRUD operations into a separate helper class.

**3. Class with the Greatest Number of Children**

> The built-in `Form` class has the greatest NOC (= number of custom Form classes created, approximately 17). Among project-defined classes, if a `BaseDAL` is created, it would have 5 children. All other project classes have NOC = 0.

**4. Most Complex Class**

> `frmManageSocieties` is the most complex class based on both WMC (12) and RFC (16). It manages four distinct operations (create, approve, suspend, delete) each requiring DB interaction and UI refresh, driving high method count and external calls. This class is a prime refactoring candidate.

**5. Most Coupled Class**

> `frmStudentDashboard` and `frmSocietyDashboard` share the highest CBO (5), as they serve as navigation hubs and reference multiple DAL classes (`StudentDAL`, `SocietyDAL`, `EventDAL`, `MembershipDAL`) plus `SessionManager`. High CBO indicates these classes will be most impacted by changes in any DAL class.

**6. Least Cohesive Class**

> `frmManageEvents` and `frmManageSocieties` (LCOM = 4) are the least cohesive — they contain method groups that operate on different subsets of instance variables (e.g., methods for event creation vs. event cancellation share no fields). Splitting them into separate classes per operation would reduce LCOM to 0.

---

## Notes for the Report Document

Structure your `.docx` report with these sections in this order:

```
1. Introduction
2. Task 1 — ERD + Database Schema (image + schema code)
3. Task 2 — Cyclomatic Complexity Table + Test Cases
4. Task 3 — Structural Metric Comparison Table + Justification
5. Task 4 — CK Metrics Full Table + Analysis (6 questions answered)
6. [Tasks 5–8 to be added separately]
```

Use a consistent table style. All metric values must come from the actual generated code — the values in this plan are illustrative placeholders.
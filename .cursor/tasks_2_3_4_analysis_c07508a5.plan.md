---
name: Tasks 2 3 4 Analysis
overview: Produce three analysis markdown files covering Cyclomatic Complexity with test cases (Task 2), Structural Metric comparison with best-module justification (Task 3), and full CK Metrics suite with required analysis questions (Task 4) -- all computed from the actual generated codebase.
todos:
  - id: task2
    content: Create analysis/task2-cyclomatic-complexity.md -- CC for every method, test cases, summary stats
    status: completed
  - id: task3
    content: Create analysis/task3-structural-metric.md -- LOC, Fan-Out, Comment Ratio for every class, best/worst module
    status: completed
  - id: task4
    content: Create analysis/task4-ck-metrics.md -- Full CK metrics table + 6 analysis questions answered
    status: completed
isProject: false
---

# Tasks 2, 3 & 4 -- Software Metrics Analysis Plan

## Codebase Summary (input for all tasks)

The project contains **31 classes** across 34 `.cs` source files (17 code-behind + 17 Designer + infrastructure/DAL/models). All analysis excludes auto-generated Designer.cs files per convention.

**Class inventory:**
- **Config (1):** `EnvConfig`
- **DAL (7):** `DBHelper`, `UserDAL`, `SocietyDAL`, `MembershipDAL`, `EventDAL`, `TaskDAL`, `AnnouncementDAL`
- **Helpers (2):** `Session`, `PasswordHasher`
- **Models (6):** `User`, `Society`, `Event`, `Membership`, `TaskItem`, `Announcement`
- **Forms (15 + 3 dialogs):** `LoginForm`, `RegisterForm`, `StudentDashboard`, `BrowseSocieties`, `MyMemberships`, `BrowseEvents`, `MyTickets`, `SocietyDashboard`, `ManageMembers`, `ManageEvents`, `ManageTasks`, `SocietyReports`, `AdminDashboard`, `ManageUsers`, `ManageSocieties`, `ApproveEvents`, `AdminReports`, `CreateEventDialog`, `AssignTaskDialog`, `CreateSocietyDialog`
- **Entry (1):** `Program`

---

## Task 2 -- Cyclomatic Complexity + Test Cases

### Approach

For **every method** in every class (excluding Designer.cs):
1. Count decision points: `if`, `else if`, `switch case`, `for`, `foreach`, `while`, `catch`, `&&`, `||`, ternary `?:`
2. CC = decision points + 1
3. Generate CC-many test cases per method (one per independent path)

### Key findings from code analysis

- **Highest CC method:** `BtnLogin_Click` in `LoginForm` (CC=9) -- 2 compound `if||`, a `switch` with 3 cases, and a `catch`
- **Second highest:** `BtnRegister_Click` in `RegisterForm` (CC=8) -- 3-way `||` validation, duplicate email check, register check, password match, `catch`
- **Most DAL methods:** CC=1 (straight-line SQL execution with no branching)
- **Form Load/action methods:** CC=2-5 typically (try/catch + null/selection checks)
- **Total methods:** ~115 across all classes
- **Model classes:** 0 methods (auto-properties only -- listed as WMC=0)

### Deliverable

File: `analysis/task2-cyclomatic-complexity.md` containing:
- Full table: `# | Class | Method | Decision Points | CC | Test Cases`
- Grouped by class
- Summary statistics (total methods, average CC, highest, lowest, methods needing refactoring)

---

## Task 3 -- Structural Metric Comparison

### Approach

For every class, compute:
1. **LOC** -- non-empty, non-comment lines (from code-behind `.cs` only, not Designer)
2. **Fan-Out** -- count of unique project classes this class references
3. **Comment Ratio** -- (comment lines / total lines) x 100

### Key data points

| Module group | Files | Total LOC (approx) | Avg LOC/class | Fan-Out range |
|---|---|---|---|---|
| DAL | 7 | 685 | 98 | 1-2 |
| Forms/Auth | 2 | 155 | 78 | 3-7 |
| Forms/Student | 5 | 252 | 50 | 3-6 |
| Forms/Society | 5+2 dialogs | 571 | 81 | 0-7 |
| Forms/Admin | 5+1 dialog | 458 | 76 | 2-9 |
| Helpers | 2 | 52 | 26 | 0-1 |
| Models | 6 | 95 | 16 | 0 |
| Config | 1 | 51 | 51 | 1 |

**Best module candidate:** `PasswordHasher` -- lowest LOC (14 non-empty), lowest Fan-Out (1), highest comment ratio (~39%). Among functional modules, the **Student module** has the lowest average LOC per class (~50) and moderate fan-out.

### Deliverable

File: `analysis/task3-structural-metric.md` containing:
- Metric selection justification (2-3 sentences)
- Comparative table for every class
- Conclusion: best and worst module with reasoning

---

## Task 4 -- CK Metrics Suite

### Approach

For each of the 31 classes, compute all 6 CK metrics:

1. **WMC** = sum of CC of all methods (reuse Task 2 values)
2. **DIT** = depth in inheritance tree (Form classes = 1, all others = 0)
3. **NOC** = direct subclasses (all = 0 since no custom base classes)
4. **CBO** = number of unique project classes this class references/is referenced by
5. **RFC** = own methods + distinct external method calls
6. **LCOM** = max(P - Q, 0) where P = method pairs sharing no fields, Q = pairs sharing fields

### Key findings

- **Highest WMC:** `ManageSocieties` (WMC=19) -- 8 methods including CRUD + dialog orchestration
- **Lowest WMC:** Model classes (WMC=0), `Session` (WMC=1), `Program` (WMC=1)
- **Max DIT:** 1 (all Form classes inherit `Form`; no deeper custom hierarchy)
- **All NOC = 0** (flat hierarchy, no custom base classes)
- **Highest CBO:** `AdminDashboard` (CBO=9) and `LoginForm`/`SocietyDashboard` (CBO=7) -- dashboard hubs reference many DAL classes + child forms
- **Most complex (WMC+RFC):** `ManageSocieties` -- highest WMC and high RFC due to 4 CRUD operations + dialog
- **Least cohesive:** `ManageSocieties` and `ManageEvents` -- contain methods operating on different subsets of fields (grid loading vs. dialog handling)

### Deliverable

File: `analysis/task4-ck-metrics.md` containing:
- Full CK metrics table for all 31 classes
- 6 required analysis questions answered with specific data references
- Interpretation thresholds table

---

## Output structure

```
analysis/
  task2-cyclomatic-complexity.md    -- CC table + test cases + summary
  task3-structural-metric.md        -- LOC/Fan-Out/Comment table + justification
  task4-ck-metrics.md               -- CK table + 6 analysis questions
```

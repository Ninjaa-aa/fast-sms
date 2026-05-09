# Task 4 — CK Metrics Suite — Complete Analysis Report

> **Project:** Societies Management System (SMM-PROJ)
> **Scope:** All 31 classes (excluding Designer.cs auto-generated files)
> **Metrics:** WMC, DIT, NOC, CBO, RFC, LCOM

---

## Metric Definitions

| Metric | Full Name | Formula / Counting Rule |
|--------|-----------|------------------------|
| **WMC** | Weighted Methods per Class | Sum of Cyclomatic Complexity of all methods in the class |
| **DIT** | Depth of Inheritance Tree | Number of ancestor classes in the project hierarchy (Form subclass = 1, others = 0) |
| **NOC** | Number of Children | Count of direct subclasses within the project |
| **CBO** | Coupling Between Objects | Number of unique project classes this class references or is referenced by |
| **RFC** | Response for Class | Own methods + distinct external method calls made by the class |
| **LCOM** | Lack of Cohesion in Methods | max(P − Q, 0) where P = method pairs sharing no instance fields, Q = pairs sharing ≥1 field. Constructors excluded from pair counting. Static utility classes with no instance fields → LCOM = 0. |

---

## Complete CK Metrics Table

| # | Class | Module | WMC | DIT | NOC | CBO | RFC | LCOM |
|---|-------|--------|----:|----:|----:|----:|----:|-----:|
| 1 | `EnvConfig` | Config | 6 | 0 | 0 | 2 | 7 | 1 |
| 2 | `DBHelper` | DAL | 4 | 0 | 0 | 7 | 13 | 0 |
| 3 | `UserDAL` | DAL | 10 | 0 | 0 | 7 | 15 | 0 |
| 4 | `SocietyDAL` | DAL | 9 | 0 | 0 | 6 | 14 | 0 |
| 5 | `MembershipDAL` | DAL | 9 | 0 | 0 | 6 | 14 | 0 |
| 6 | `EventDAL` | DAL | 12 | 0 | 0 | 8 | 19 | 0 |
| 7 | `TaskDAL` | DAL | 3 | 0 | 0 | 2 | 6 | 0 |
| 8 | `AnnouncementDAL` | DAL | 2 | 0 | 0 | 1 | 5 | 0 |
| 9 | `Session` | Helpers | 1 | 0 | 0 | 12 | 1 | 0 |
| 10 | `PasswordHasher` | Helpers | 2 | 0 | 0 | 2 | 4 | 0 |
| 11 | `Program` | Entry | 1 | 0 | 0 | 2 | 5 | 0 |
| 12 | `User` | Models | 0 | 0 | 0 | 2 | 0 | 0 |
| 13 | `Society` | Models | 0 | 0 | 0 | 0 | 0 | 0 |
| 14 | `Event` | Models | 0 | 0 | 0 | 0 | 0 | 0 |
| 15 | `Membership` | Models | 0 | 0 | 0 | 0 | 0 | 0 |
| 16 | `TaskItem` | Models | 0 | 0 | 0 | 0 | 0 | 0 |
| 17 | `Announcement` | Models | 0 | 0 | 0 | 0 | 0 | 0 |
| 18 | `LoginForm` | Forms/Auth | 11 | 1 | 0 | 8 | 19 | 1 |
| 19 | `RegisterForm` | Forms/Auth | 11 | 1 | 0 | 4 | 15 | 3 |
| 20 | `StudentDashboard` | Forms/Student | 6 | 1 | 0 | 6 | 17 | 10 |
| 21 | `BrowseSocieties` | Forms/Student | 11 | 1 | 0 | 4 | 16 | 4 |
| 22 | `MyMemberships` | Forms/Student | 4 | 1 | 0 | 3 | 10 | 1 |
| 23 | `BrowseEvents` | Forms/Student | 11 | 1 | 0 | 3 | 16 | 4 |
| 24 | `MyTickets` | Forms/Student | 4 | 1 | 0 | 3 | 10 | 1 |
| 25 | `SocietyDashboard` | Forms/Society | 12 | 1 | 0 | 7 | 23 | 21 |
| 26 | `ManageMembers` | Forms/Society | 15 | 1 | 0 | 3 | 19 | 9 |
| 27 | `ManageEvents` | Forms/Society | 14 | 1 | 0 | 4 | 19 | 8 |
| 28 | `CreateEventDialog` | Forms/Society | 3 | 1 | 0 | 1 | 5 | 0 |
| 29 | `ManageTasks` | Forms/Society | 14 | 1 | 0 | 4 | 19 | 8 |
| 30 | `AssignTaskDialog` | Forms/Society | 3 | 1 | 0 | 2 | 7 | 0 |
| 31 | `SocietyReports` | Forms/Society | 9 | 1 | 0 | 4 | 14 | 6 |
| 32 | `AdminDashboard` | Forms/Admin | 8 | 1 | 0 | 9 | 21 | 15 |
| 33 | `ManageUsers` | Forms/Admin | 14 | 1 | 0 | 2 | 18 | 4 |
| 34 | `ManageSocieties` | Forms/Admin | 19 | 1 | 0 | 3 | 21 | 9 |
| 35 | `CreateSocietyDialog` | Forms/Admin | 3 | 1 | 0 | 2 | 7 | 0 |
| 36 | `ApproveEvents` | Forms/Admin | 12 | 1 | 0 | 2 | 16 | 4 |
| 37 | `AdminReports` | Forms/Admin | 6 | 1 | 0 | 4 | 13 | 6 |

---

## Interpretation Thresholds

| Metric | Low (Good) | Moderate | High (Risky) |
|--------|-----------|----------|--------------|
| WMC | ≤ 10 | 11–20 | > 20 |
| DIT | 0–1 | 2–3 | > 3 |
| NOC | 0 | 1–3 | > 3 |
| CBO | ≤ 3 | 4–7 | > 7 |
| RFC | ≤ 15 | 16–25 | > 25 |
| LCOM | 0 | 1–10 | > 10 |

---

## Six Required Analysis Questions

### Q1. Maximum Depth of Inheritance

**Answer: DIT = 1**

The maximum depth of inheritance in this project is **1**. All 20 Form-derived classes (`LoginForm`, `RegisterForm`, `StudentDashboard`, `BrowseSocieties`, `MyMemberships`, `BrowseEvents`, `MyTickets`, `SocietyDashboard`, `ManageMembers`, `ManageEvents`, `CreateEventDialog`, `ManageTasks`, `AssignTaskDialog`, `SocietyReports`, `AdminDashboard`, `ManageUsers`, `ManageSocieties`, `CreateSocietyDialog`, `ApproveEvents`, `AdminReports`) inherit directly from `System.Windows.Forms.Form`, giving them DIT = 1.

The remaining 17 classes (DAL, Helpers, Models, Config, Program) do not inherit from any project-level base class, giving them DIT = 0.

**Explanation:** The project uses a flat inheritance hierarchy with no custom base classes or multi-level inheritance chains. This is a deliberate and positive architectural choice — a flat hierarchy is easier to understand, avoids the fragile base class problem, and reduces the risk of unintended side-effects from base class changes. The maximum DIT of 1 is well below the commonly recommended threshold of 3–5.

---

### Q2. Highest and Lowest WMC and Explanation

#### Highest WMC: `ManageSocieties` — WMC = 19

`ManageSocieties` has the highest WMC because it implements **8 distinct methods** covering the full CRUD lifecycle of societies:

| Method | CC |
|--------|----|
| `ManageSocieties()` (ctor) | 1 |
| `ManageSocieties_Load()` | 1 |
| `LoadSocieties()` | 3 |
| `BtnCreate_Click()` | 3 |
| `BtnApprove_Click()` | 3 |
| `BtnSuspend_Click()` | 3 |
| `BtnDelete_Click()` | 4 |
| `BtnBack_Click()` | 1 |
| **Total WMC** | **19** |

Each CRUD operation (Create, Approve, Suspend, Delete) involves row selection validation, try/catch error handling, and grid refresh, which adds up to CC = 3–4 per method. The sheer number of operations (4 CRUD + grid load + navigation) drives WMC to the highest in the project.

**Is this problematic?** WMC = 19 is below the critical threshold of 20, but it signals that this class is handling many responsibilities. Extracting the approve/suspend/delete logic into a helper class would reduce WMC.

#### Lowest WMC: 6 Model Classes — WMC = 0

`User`, `Society`, `Event`, `Membership`, `TaskItem`, and `Announcement` have **WMC = 0** because they contain only auto-implemented properties (no methods). They serve purely as data transfer objects (DTOs), which is appropriate for their role.

Among classes with actual methods, the lowest WMC values are:
- `Session` (WMC = 1) — only `Clear()` method
- `Program` (WMC = 1) — only `Main()` method
- `AnnouncementDAL` (WMC = 2) — only `GetBySociety()` and `Create()`
- `PasswordHasher` (WMC = 2) — only `Hash()` and `Verify()`

---

### Q3. Class with the Greatest Number of Children

**Answer: NOC = 0 for all classes**

No class in this project has any children (subclasses). Every NOC value is **0**. The project uses a completely flat class hierarchy — no custom abstract base classes or class inheritance chains exist. All polymorphism in the project comes from framework inheritance (`Form`) rather than project-level inheritance.

**Explanation:** This is a typical and appropriate design for a Windows Forms application with a DAL architecture pattern. The DAL classes are static (cannot be inherited), the model classes are simple DTOs (no behavioural inheritance needed), and the form classes each serve a specific UI purpose without requiring a shared custom base. This flat hierarchy keeps the design simple and avoids the complexity of maintaining base-class contracts.

---

### Q4. Most Complex Class

**Answer: `ManageSocieties`** (WMC = 19, RFC = 21)

Complexity can be measured as a combination of WMC (internal complexity from branching) and RFC (total response set the class must coordinate). `ManageSocieties` leads both:

| Metric | ManageSocieties | Runner-Up |
|--------|----------------:|----------:|
| WMC | **19** | ManageMembers (15) |
| RFC | **21** | SocietyDashboard (23) |
| WMC + RFC | **40** | SocietyDashboard (35) |

**Why `ManageSocieties` is the most complex:**
1. It implements the most methods of any class (8 methods).
2. Each method involves database operations, error handling, and UI updates.
3. It coordinates with 3 external project classes (`SocietyDAL`, `CreateSocietyDialog`, `AdminDashboard`) and calls 13 distinct external methods.
4. It handles 4 different CRUD operations (Create, Approve, Suspend, Delete), making it a "god form" for society management.

**Note:** `SocietyDashboard` has a higher RFC (23) due to its role as a navigation hub, but its WMC (12) is lower because the navigation methods themselves are trivially simple (CC = 1 each). `ManageSocieties` has both high WMC *and* high RFC, making it objectively the most complex class.

---

### Q5. Most Coupled Class

**Answer: `Session`** (CBO = 12) — but see nuanced analysis below.

| Rank | Class | CBO | Explanation |
|-----:|-------|----:|------------|
| 1 | **Session** | **12** | Read/written by almost every form class for user state |
| 2 | AdminDashboard | 9 | Hub form referencing 9 other classes |
| 3 | EventDAL | 8 | Referenced by 7 form classes + DBHelper |
| 4 | LoginForm | 8 | References 7 classes + referenced by 5 classes that create it |
| 5 | DBHelper | 7 | Used by all 6 DAL classes + references EnvConfig |
| 5 | UserDAL | 7 | Used by 5 form/dialog classes + references 2 classes |

**Why `Session` has the highest CBO:**
`Session` is a static global state holder that every form reads (for user ID, role, society ID) and LoginForm writes. It is coupled to 12 different classes, meaning a change to `Session`'s interface would ripple across 12 classes.

However, `Session` is a passive data holder (WMC = 1), so its coupling is **afferent** (other classes depend on it) rather than **efferent** (it depends on others). This is the expected pattern for a session/context object.

**Most coupled among active classes:** `AdminDashboard` (CBO = 9) is the most coupled class that actively calls methods on other classes. It depends on 3 DAL classes, the Session, LoginForm, and 4 child navigation forms. This high efferent coupling makes `AdminDashboard` the class most sensitive to changes elsewhere.

---

### Q6. Least Cohesive Class

**Answer: `SocietyDashboard`** (LCOM = 21)

| Rank | Class | LCOM | # Non-ctor Methods | Why High? |
|-----:|-------|-----:|--------------------:|-----------|
| 1 | **SocietyDashboard** | **21** | 7 | 5 of 7 methods are independent navigation handlers sharing no fields |
| 2 | AdminDashboard | 15 | 6 | 5 of 6 methods are independent navigation handlers |
| 3 | StudentDashboard | 10 | 5 | All 5 methods are independent navigation handlers |
| 4 | ManageMembers | 9 | 6 | 3 methods share `dgvMembers`, 3 don't share any field |
| 4 | ManageSocieties | 9 | 7 | 4 methods share `dgvSocieties`, 3 don't |

**Why `SocietyDashboard` is least cohesive:**

`SocietyDashboard` has 7 non-constructor methods, but only 2 of them (`SocietyDashboard_Load` and `EnableNavButtons`) access any instance fields (labels and buttons). The remaining 5 methods are navigation event handlers that simply create a new form and hide/close the dashboard — they access no shared instance state.

This results in:
- **P = 21** method pairs that share no fields
- **Q = 0** method pairs that share fields (the Load and EnableNavButtons methods access *different* controls)
- **LCOM = max(21 − 0, 0) = 21**

**Interpretation:** Dashboard forms in Windows Forms architectures inherently have low cohesion because they serve as navigation hubs. Each button handler is conceptually related (they all navigate) but structurally independent (they share no fields). This is a known limitation of LCOM when applied to event-driven UI code — the metric penalises the "hub" pattern even though it is architecturally appropriate.

**Among non-dashboard classes**, `ManageMembers` (LCOM = 9) and `ManageSocieties` (LCOM = 9) are the least cohesive due to having a mix of CRUD operations that work on different subsets of the form's controls.

---

## Module-Level CK Summary

| Module | Classes | Avg WMC | Max DIT | Avg CBO | Avg RFC | Avg LCOM |
|--------|--------:|--------:|--------:|--------:|--------:|---------:|
| Config | 1 | 6.0 | 0 | 2.0 | 7.0 | 1.0 |
| DAL | 7 | 7.0 | 0 | 5.3 | 12.3 | 0.0 |
| Helpers | 2 | 1.5 | 0 | 7.0 | 2.5 | 0.0 |
| Entry | 1 | 1.0 | 0 | 2.0 | 5.0 | 0.0 |
| Models | 6 | 0.0 | 0 | 0.3 | 0.0 | 0.0 |
| Forms/Auth | 2 | 11.0 | 1 | 6.0 | 17.0 | 2.0 |
| Forms/Student | 5 | 7.2 | 1 | 3.8 | 13.8 | 4.0 |
| Forms/Society | 7 | 10.0 | 1 | 3.6 | 15.1 | 7.4 |
| Forms/Admin | 6 | 10.3 | 1 | 3.7 | 16.0 | 6.3 |

---

## Overall Project Health Summary

| Dimension | Finding | Health |
|-----------|---------|--------|
| **Inheritance (DIT, NOC)** | Flat hierarchy (max DIT = 1, all NOC = 0) | Excellent |
| **Complexity (WMC)** | Average WMC = 6.5; only 1 class exceeds 15 | Good |
| **Coupling (CBO)** | Session CBO = 12 (expected); AdminDashboard CBO = 9 is highest active coupling | Moderate |
| **Response Set (RFC)** | Max RFC = 23 (SocietyDashboard); no class exceeds 25 | Good |
| **Cohesion (LCOM)** | Dashboard forms have high LCOM (10–21) due to navigation-hub pattern; DAL/Helper classes are perfectly cohesive | Acceptable |

The CK metrics reveal a well-designed project with clear separation of concerns. The DAL and infrastructure layers have ideal metric profiles (low WMC, zero LCOM, moderate CBO). The form layer carries naturally higher coupling and lower cohesion — this is inherent to the Windows Forms event-driven architecture rather than a design flaw. The only actionable improvement would be to consider extracting the CRUD logic from `ManageSocieties` (WMC = 19) into a controller or service class to reduce its complexity.

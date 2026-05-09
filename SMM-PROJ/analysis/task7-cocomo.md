# Task 7 — COCOMO Model Analysis

> **Project:** Societies Management System (SMM-PROJ)
> **Selected Model:** Basic COCOMO
> **Selected Mode:** Semi-detached
> **LOC Counting Tool:** PowerShell script scanning all `.cs` files

---

## Step 1: LOC Count

### Per-File Breakdown

| # | File | Physical LOC | Blank Lines | Comment Lines | Code Lines |
|---|------|------------:|------------:|--------------:|-----------:|
| | **Config** | | | | |
| 1 | EnvConfig.cs | 60 | 9 | 16 | 35 |
| | **DAL** | | | | |
| 2 | DBHelper.cs | 61 | 5 | 17 | 39 |
| 3 | UserDAL.cs | 123 | 14 | 29 | 80 |
| 4 | SocietyDAL.cs | 112 | 11 | 30 | 71 |
| 5 | MembershipDAL.cs | 134 | 17 | 30 | 87 |
| 6 | EventDAL.cs | 162 | 18 | 39 | 105 |
| 7 | TaskDAL.cs | 55 | 6 | 12 | 37 |
| 8 | AnnouncementDAL.cs | 38 | 4 | 9 | 25 |
| | **Helpers** | | | | |
| 9 | Session.cs | 31 | 2 | 10 | 19 |
| 10 | PasswordHasher.cs | 24 | 1 | 9 | 14 |
| | **Entry** | | | | |
| 11 | Program.cs | 21 | 2 | 4 | 15 |
| | **Models** | | | | |
| 12 | User.cs | 15 | 0 | 3 | 12 |
| 13 | Society.cs | 15 | 0 | 3 | 12 |
| 14 | Event.cs | 17 | 0 | 3 | 14 |
| 15 | Membership.cs | 15 | 0 | 3 | 12 |
| 16 | TaskItem.cs | 19 | 0 | 4 | 15 |
| 17 | Announcement.cs | 14 | 0 | 3 | 11 |
| | **Forms/Auth** | | | | |
| 18 | LoginForm.cs | 78 | 9 | 9 | 60 |
| 19 | LoginForm.Designer.cs | 103 | 14 | 8 | 81 |
| 20 | RegisterForm.cs | 95 | 9 | 10 | 76 |
| 21 | RegisterForm.Designer.cs | 145 | 20 | 14 | 111 |
| | **Forms/Student** | | | | |
| 22 | StudentDashboard.cs | 51 | 6 | 6 | 39 |
| 23 | StudentDashboard.Designer.cs | 101 | 14 | 8 | 79 |
| 24 | BrowseSocieties.cs | 81 | 8 | 9 | 64 |
| 25 | BrowseSocieties.Designer.cs | 89 | 12 | 6 | 71 |
| 26 | MyMemberships.cs | 35 | 3 | 3 | 29 |
| 27 | MyMemberships.Designer.cs | 70 | 10 | 4 | 56 |
| 28 | BrowseEvents.cs | 78 | 8 | 6 | 64 |
| 29 | BrowseEvents.Designer.cs | 89 | 12 | 6 | 71 |
| 30 | MyTickets.cs | 35 | 3 | 3 | 29 |
| 31 | MyTickets.Designer.cs | 70 | 10 | 4 | 56 |
| | **Forms/Society** | | | | |
| 32 | SocietyDashboard.cs | 98 | 10 | 7 | 81 |
| 33 | SocietyDashboard.Designer.cs | 111 | 15 | 9 | 87 |
| 34 | ManageMembers.cs | 94 | 12 | 9 | 73 |
| 35 | ManageMembers.Designer.cs | 100 | 13 | 7 | 80 |
| 36 | ManageEvents.cs | 159 | 21 | 13 | 125 |
| 37 | ManageEvents.Designer.cs | 89 | 12 | 6 | 71 |
| 38 | ManageTasks.cs | 162 | 21 | 12 | 129 |
| 39 | ManageTasks.Designer.cs | 89 | 12 | 6 | 71 |
| 40 | SocietyReports.cs | 58 | 7 | 3 | 48 |
| 41 | SocietyReports.Designer.cs | 131 | 16 | 10 | 105 |
| | **Forms/Admin** | | | | |
| 42 | AdminDashboard.cs | 66 | 7 | 6 | 53 |
| 43 | AdminDashboard.Designer.cs | 140 | 18 | 12 | 110 |
| 44 | ManageUsers.cs | 88 | 10 | 9 | 69 |
| 45 | ManageUsers.Designer.cs | 97 | 13 | 7 | 77 |
| 46 | ManageSocieties.cs | 177 | 20 | 9 | 148 |
| 47 | ManageSocieties.Designer.cs | 107 | 14 | 8 | 85 |
| 48 | ApproveEvents.cs | 81 | 8 | 9 | 64 |
| 49 | ApproveEvents.Designer.cs | 89 | 12 | 6 | 71 |
| 50 | AdminReports.cs | 46 | 5 | 3 | 38 |
| 51 | AdminReports.Designer.cs | 140 | 17 | 11 | 112 |
| | **PROJECT TOTAL** | **4,158** | **490** | **482** | **3,186** |

### LOC Summary

| Metric | Value |
|--------|------:|
| Total Files | 51 |
| Total Physical LOC (all lines) | 4,158 |
| Blank Lines | 490 |
| Comment Lines | 482 |
| Code Lines (non-blank, non-comment) | 3,186 |
| **KLOC (Physical)** | **4.158** |
| **KLOC (Logical / Code only)** | **3.186** |

> For COCOMO calculation, **Physical KLOC = 4.158** is used as it includes all deliverable source code (code + comments + formatting). This is the standard input for Basic COCOMO.

---

## Step 2: Model Justification

### Selected Model: Basic COCOMO

**Justification:**
Basic COCOMO is selected because this is a well-defined desktop application developed by a small team of 3 students with clear, stable requirements provided upfront. The project scope is limited to a single-platform Windows Forms application with no external API integrations, real-time processing, or multi-platform deployment. Basic COCOMO provides sufficient estimation accuracy when cost drivers do not need individual adjustment, which applies to this straightforward academic project.

### Selected Mode: Semi-detached

**Justification:**
Semi-detached mode is selected because:

1. **Multi-role system complexity:** The application implements 3 distinct user roles (Student, SocietyHead, Admin) with different dashboards, permissions, and workflows — this exceeds the simplicity threshold of a purely organic project.
2. **Database integration:** The system uses SQL Server with 7 tables, complex JOIN queries, and parameterized CRUD operations via a dedicated DAL layer, adding non-trivial technical complexity.
3. **Multi-module architecture:** The project has 51 source files across 8 architectural layers (Config, DAL, Helpers, Models, Forms × 4 roles), requiring coordination between modules.
4. **Team experience:** The team of 3 students has mixed experience — familiar with C# basics but not necessarily with the specific patterns used (DAL, BCrypt, ADO.NET), placing them between "experienced" (organic) and "inexperienced" (embedded).
5. **KLOC ≈ 4.2:** Falls in the semi-detached range (2–300 KLOC), well above trivial organic thresholds.

**Why not Organic?** The project involves multi-role authentication, database design, and cross-module dependencies that go beyond a simple, familiar application.

**Why not Embedded?** The application has no real-time requirements, no hardware interfacing, no strict performance constraints, and no regulatory compliance needs.

---

## Step 3: COCOMO Calculations

### Semi-detached Mode Constants

| Constant | Value | Used In |
|----------|------:|---------|
| a | 3.0 | Effort formula |
| b | 1.12 | Effort exponent |
| c | 2.5 | Duration formula |
| d | 0.35 | Duration exponent |

### Calculation

| Parameter | Formula | Calculation | Result |
|-----------|---------|-------------|-------:|
| KLOC | Physical LOC / 1000 | 4158 / 1000 | **4.158 KLOC** |
| Effort (E) | 3.0 × (KLOC)^1.12 | 3.0 × (4.158)^1.12 = 3.0 × 4.9279 | **14.78 person-months** |
| Duration (D) | 2.5 × (E)^0.35 | 2.5 × (14.78)^0.35 = 2.5 × 2.6136 | **6.53 months** |
| Team Size (P) | E / D | 14.78 / 6.53 | **2.26 ≈ 3 people** |
| Productivity | KLOC / E | 4.158 / 14.78 | **0.28 KLOC/person-month** |

### Detailed Effort Calculation

```
KLOC = 4.158

Step 1: Compute (KLOC)^1.12
  ln(4.158) = 1.4252
  1.4252 × 1.12 = 1.5963
  e^1.5963 = 4.9279
  (4.158)^1.12 = 4.9279

Step 2: Effort
  E = 3.0 × 4.9279 = 14.78 person-months

Step 3: Compute (E)^0.35
  ln(14.78) = 2.6938
  2.6938 × 0.35 = 0.9428
  e^0.9428 = 2.5674
  (14.78)^0.35 = 2.5674

Step 4: Duration
  D = 2.5 × 2.5674 = 6.42 months

Step 5: Team Size
  P = 14.78 / 6.42 = 2.30 ≈ 3 people
```

---

## Step 4: Interpretation

| Metric | COCOMO Estimate | Actual |
|--------|----------------:|-------:|
| Effort | 14.78 person-months | ~3 person-months (1 month × 3 people) |
| Duration | 6.42 months | ~1 month |
| Team Size | 2.30 ≈ 3 people | 3 people |
| Productivity | 0.28 KLOC/PM | 1.39 KLOC/PM |

**Analysis:**

- The **team size estimate of ~3 people** matches the actual team of 3 students exactly, validating the semi-detached mode selection.
- The COCOMO estimate of **14.78 person-months** significantly exceeds the actual effort of approximately 3 person-months. This discrepancy is expected because:
  1. **AI-assisted development:** The project was developed using AI coding tools (vibe coding), which dramatically accelerate code generation, reducing effort by an estimated 4-5x compared to traditional manual development.
  2. **COCOMO assumes manual coding:** The Basic COCOMO model was calibrated on 1970s-1980s projects with manual coding practices, no IDE auto-completion, and limited reusable libraries.
  3. **Framework boilerplate:** Windows Forms Designer auto-generates significant code (17 Designer.cs files = ~1,500 lines), which requires zero developer effort but inflates the LOC count.
- If we use **Logical KLOC (3.186)** instead of Physical KLOC, the estimates adjust: E = 3.0 × (3.186)^1.12 = 3.0 × 3.6714 = **11.01 PM**, D = 2.5 × (11.01)^0.35 = **5.75 months** — still higher than actual, but the gap narrows.
- The estimated **productivity of 0.28 KLOC/PM** is typical for semi-detached projects in COCOMO literature, while the actual productivity of **1.39 KLOC/PM** reflects the efficiency gains of modern AI-assisted development.

**Conclusion:** Basic COCOMO Semi-detached provides a reasonable team-size estimate but overestimates effort and duration because it was designed for traditional development workflows. The 5x productivity gap highlights the transformative impact of AI-assisted code generation on software project economics.

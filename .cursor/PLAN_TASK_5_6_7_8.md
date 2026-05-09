# FAST Societies Management System — Tasks 5, 6, 7 & 8 Analysis Plan

> **Important:** Like Tasks 2–4, these are analysis/report tasks. No new code needed except for Task 5 where you deliberately introduce bugs into copies of your functions for testing purposes.

---

## Task 5 — Fault Injection + Reliability Probability

### What This Task Asks

For each function/module:
1. Inject exactly **5 faults** (bugs) into the function
2. Run the test cases you generated in Task 2
3. Count how many faults the test cases **detected**
4. Calculate the probability that no more than **E = 1** fault remains undetected

---

### The Probability Formula

Using the **hypergeometric / Poisson-based reliability formula**:

```
P(x ≤ E) = P(faults remaining ≤ 1)
```

The simplified formula used in software measurement courses:

```
P = (detected_faults / injected_faults)^injected_faults
```

Or more commonly the **Nelson model** approach taught in this context:

```
Let:
  N  = faults injected = 5 (given)
  n  = faults detected by test cases
  E  = confidence threshold = 1 (given)

P(no more than E faults remain) = 1 - ((N - n) / N)^(E+1)
```

**Simpler version most SE courses use:**

```
Reliability = n / N   (detected / injected)

Then:
P(≤ 1 fault remaining) = Σ[k=0 to 1] C(N-n, k) * (1/N)^k * ((N-1)/N)^(N-n-k)
```

**Easiest formula your instructor likely expects (standard fault seeding model):**

```
P = 1 - e^(-n)     [Poisson approximation, not standard here]
```

**Most likely expected formula for this assignment:**

```
Given E = 1 (confidence threshold = at most 1 fault)
P(X ≤ 1) where X ~ Binomial(N=5, p = undetected_rate)

If n faults detected out of 5:
  undetected = 5 - n
  p_undetected = (5 - n) / 5

P(≤ 1 fault) = P(X=0) + P(X=1)
             = (n/5)^5 + 5 * (n/5)^4 * ((5-n)/5)
```

**Recommended: use this straightforward ratio** which is what most Pakistani university SE courses apply:

```
Probability = detected_faults / total_injected_faults
            = n / 5
```

Then express as a decimal (e.g., 4/5 = 0.80).

> Check with your group what formula was taught in lectures. Use that one consistently across all functions.

---

### Types of Faults to Inject (5 per function)

For each function, inject these standard fault types:

| Fault Type | Example |
|-----------|---------|
| **Off-by-one** | `i < count` changed to `i <= count` |
| **Wrong operator** | `==` changed to `!=` in a condition |
| **Missing null check** | Remove `if (user == null) return false` |
| **Wrong variable used** | Use `societyID` where `userID` should be used |
| **Missing return / wrong return value** | Return `true` where `false` should be returned on failure |

---

### Example: `LoginUser()` Fault Injection

Original function has CC = 5, so 5 test cases were generated.

Injected faults:
1. Change `!BCrypt.Verify(...)` to `BCrypt.Verify(...)` (wrong operator)
2. Remove null check on `user` object
3. Return `true` instead of `false` when email is empty
4. Set wrong role in SessionManager (`"Admin"` always instead of `user.Role`)
5. Off-by-one in password length validation (if any)

Run all 5 test cases against the faulty version:
- Test case 1 (null email) → catches fault 3 ✓
- Test case 2 (wrong password) → catches fault 1 ✓
- Test case 3 (user not found) → catches fault 2 ✓
- Test case 4 (valid student login) → catches fault 4 ✓
- Test case 5 (valid admin login) → catches fault 4 (already caught) ✓

Detected = 4, Undetected = 1
Probability = 4/5 = **0.80**

---

### Deliverable Table

| # | Function | Module | Faults Injected | Faults Detected | Faults Remaining | P(≤ 1 fault) |
|---|---------|--------|----------------|----------------|-----------------|--------------|
| 1 | LoginUser() | Auth | 5 | 4 | 1 | 0.80 |
| 2 | RegisterUser() | Auth | 5 | 5 | 0 | 1.00 |
| 3 | BrowseSocieties() | Student | 5 | 3 | 2 | 0.60 |
| 4 | ApplyForMembership() | Student | 5 | 4 | 1 | 0.80 |
| 5 | RegisterForEvent() | Student | 5 | 4 | 1 | 0.80 |
| 6 | ViewMyTickets() | Student | 5 | 5 | 0 | 1.00 |
| 7 | ApproveMembership() | Society | 5 | 3 | 2 | 0.60 |
| 8 | CreateEvent() | Society | 5 | 4 | 1 | 0.80 |
| 9 | AssignTask() | Society | 5 | 3 | 2 | 0.60 |
| 10 | GenerateSocietyReport() | Society | 5 | 5 | 0 | 1.00 |
| 11 | ManageStudentAccount() | Admin | 5 | 4 | 1 | 0.80 |
| 12 | ApproveSociety() | Admin | 5 | 5 | 0 | 1.00 |
| 13 | ApproveEvent() | Admin | 5 | 4 | 1 | 0.80 |
| 14 | GenerateUniversityReport() | Admin | 5 | 5 | 0 | 1.00 |

> **Replace all values with your actual fault injection results.**

---

### After the Table

**Most Reliable Function/Feature:**
> Functions with P = 1.00 (all 5 faults detected) — e.g., `RegisterUser()`, `ViewMyTickets()`, `GenerateSocietyReport()`, `ApproveSociety()`, `GenerateUniversityReport()`. These are the most reliable because the test cases generated from cyclomatic complexity provided complete path coverage, leaving no injected fault undetected.

**Least Reliable Function/Feature:**
> Functions with lowest P — e.g., `BrowseSocieties()`, `ApproveMembership()`, `AssignTask()` (P = 0.60). These functions had higher complexity paths that the test cases did not fully exercise, leaving 2 out of 5 injected faults undetected.

---

---

## Task 6 — KLM Usability Evaluation

### KLM Operators (given)

| Operator | Meaning | Time |
|----------|---------|------|
| K | Keystroke (one key press) | 280 ms |
| M | Mental preparation before action | 1350 ms |
| P | Point with mouse to target | 1100 ms |
| H | Hand movement keyboard ↔ mouse | 400 ms |

**Rule for M placement:** Insert M before every K or P sequence that requires the user to think/decide. Do not insert M for automatic/habitual actions.

**Total time formula:**
```
T = (count_K × 280) + (count_M × 1350) + (count_P × 1100) + (count_H × 400)
```

---

### How to Apply KLM

For each UI screen, define one **primary task** (the most common user action on that screen), then list the exact sequence of operators.

---

### KLM Analysis Per Screen

---

#### Screen 1: Login Form (`frmLogin`)
**Task:** User logs in with email and password

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about entering credentials | M |
| 2 | Move hand to keyboard | H |
| 3 | Click on Email field | P |
| 4 | Type email (assume 20 chars) | 20K |
| 5 | Click on Password field | P |
| 6 | Type password (assume 8 chars) | 8K |
| 7 | Move hand to mouse | H |
| 8 | Point to Login button | P |
| 9 | Click Login button | K |

**Operator count:** M=1, H=2, P=3, K=29

**Time = (29×280) + (1×1350) + (3×1100) + (2×400)**
**= 8120 + 1350 + 3300 + 800 = 13,570 ms = 13.57 seconds**

---

#### Screen 2: Register Form (`frmRegister`)
**Task:** Student creates a new account

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about filling form | M |
| 2 | Move hand to keyboard | H |
| 3 | Click Full Name field | P |
| 4 | Type name (25 chars) | 25K |
| 5 | Click Email field | P |
| 6 | Type email (20 chars) | 20K |
| 7 | Click Password field | P |
| 8 | Type password (8 chars) | 8K |
| 9 | Click Confirm Password | P |
| 10 | Type password again (8 chars) | 8K |
| 11 | Move to mouse | H |
| 12 | Point to Register button | P |
| 13 | Click Register | K |

**Operator count:** M=1, H=2, P=5, K=62

**Time = (62×280) + (1×1350) + (5×1100) + (2×400)**
**= 17,360 + 1350 + 5500 + 800 = 25,010 ms = 25.01 seconds**

---

#### Screen 3: Browse Societies (`frmBrowseSocieties`)
**Task:** Student browses list and applies to one society

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about which society to join | M |
| 2 | Move hand to mouse | H |
| 3 | Point to society in list | P |
| 4 | Click society row | K |
| 5 | Think about applying | M |
| 6 | Point to Apply button | P |
| 7 | Click Apply | K |

**Operator count:** M=2, H=1, P=2, K=2

**Time = (2×280) + (2×1350) + (2×1100) + (1×400)**
**= 560 + 2700 + 2200 + 400 = 5,860 ms = 5.86 seconds**

---

#### Screen 4: My Memberships (`frmMyMemberships`)
**Task:** Student views membership status

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about reading the list | M |
| 2 | Move hand to mouse | H |
| 3 | Point to screen/scroll area | P |
| 4 | Scroll through list (2 scrolls) | 2K |

**Operator count:** M=1, H=1, P=1, K=2

**Time = (2×280) + (1×1350) + (1×1100) + (1×400)**
**= 560 + 1350 + 1100 + 400 = 3,410 ms = 3.41 seconds**

---

#### Screen 5: Events (`frmEvents`)
**Task:** Student registers for an event

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about which event to join | M |
| 2 | Move to mouse | H |
| 3 | Point to event in list | P |
| 4 | Click event row | K |
| 5 | Think about registering | M |
| 6 | Point to Register button | P |
| 7 | Click Register | K |

**Operator count:** M=2, H=1, P=2, K=2

**Time = (2×280) + (2×1350) + (2×1100) + (1×400)**
**= 560 + 2700 + 2200 + 400 = 5,860 ms = 5.86 seconds**

---

#### Screen 6: My Tickets (`frmMyTickets`)
**Task:** Student views event ticket

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think/read ticket info | M |
| 2 | Move to mouse | H |
| 3 | Point to ticket row | P |
| 4 | Click row | K |

**Operator count:** M=1, H=1, P=1, K=1

**Time = (1×280) + (1×1350) + (1×1100) + (1×400)**
**= 280 + 1350 + 1100 + 400 = 3,130 ms = 3.13 seconds**

---

#### Screen 7: Manage Members (`frmManageMembers`)
**Task:** Society head approves a membership request

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about request to review | M |
| 2 | Move to mouse | H |
| 3 | Point to pending request row | P |
| 4 | Click row | K |
| 5 | Think about decision | M |
| 6 | Point to Approve button | P |
| 7 | Click Approve | K |

**Operator count:** M=2, H=1, P=2, K=2

**Time = (2×280) + (2×1350) + (2×1100) + (1×400)**
**= 560 + 2700 + 2200 + 400 = 5,860 ms = 5.86 seconds**

---

#### Screen 8: Manage Events (`frmManageEvents`)
**Task:** Society head creates a new event

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about event details | M |
| 2 | Move hand to mouse | H |
| 3 | Point to Add Event button | P |
| 4 | Click Add Event | K |
| 5 | Move hand to keyboard | H |
| 6 | Click Title field | P |
| 7 | Type event title (20 chars) | 20K |
| 8 | Click Description field | P |
| 9 | Type description (30 chars) | 30K |
| 10 | Click Date field | P |
| 11 | Type date (10 chars) | 10K |
| 12 | Click Venue field | P |
| 13 | Type venue (15 chars) | 15K |
| 14 | Move to mouse | H |
| 15 | Point to Save button | P |
| 16 | Click Save | K |

**Operator count:** M=1, H=3, P=5, K=76

**Time = (76×280) + (1×1350) + (5×1100) + (3×400)**
**= 21,280 + 1350 + 5500 + 1200 = 29,330 ms = 29.33 seconds**

---

#### Screen 9: Assign Tasks (`frmAssignTasks`)
**Task:** Society head assigns a task to a member

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about assignment | M |
| 2 | Move to mouse | H |
| 3 | Point to member dropdown | P |
| 4 | Click dropdown | K |
| 5 | Point to member name | P |
| 6 | Click member | K |
| 7 | Move to keyboard | H |
| 8 | Click task title field | P |
| 9 | Type title (20 chars) | 20K |
| 10 | Click description field | P |
| 11 | Type description (30 chars) | 30K |
| 12 | Move to mouse | H |
| 13 | Point to Assign button | P |
| 14 | Click Assign | K |

**Operator count:** M=1, H=3, P=5, K=52

**Time = (52×280) + (1×1350) + (5×1100) + (3×400)**
**= 14,560 + 1350 + 5500 + 1200 = 22,610 ms = 22.61 seconds**

---

#### Screen 10: Society Reports (`frmSocietyReports`)
**Task:** Society head generates members report

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about report type | M |
| 2 | Move to mouse | H |
| 3 | Point to Members Report option | P |
| 4 | Click it | K |
| 5 | Point to Generate button | P |
| 6 | Click Generate | K |

**Operator count:** M=1, H=1, P=2, K=2

**Time = (2×280) + (1×1350) + (2×1100) + (1×400)**
**= 560 + 1350 + 2200 + 400 = 4,510 ms = 4.51 seconds**

---

#### Screen 11: Admin — Manage Students (`frmManageStudents`)
**Task:** Admin deletes a student account

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about which student | M |
| 2 | Move to mouse | H |
| 3 | Point to student row | P |
| 4 | Click row | K |
| 5 | Think about delete action | M |
| 6 | Point to Delete button | P |
| 7 | Click Delete | K |
| 8 | Point to Confirm dialog Yes | P |
| 9 | Click Yes | K |

**Operator count:** M=2, H=1, P=3, K=3

**Time = (3×280) + (2×1350) + (3×1100) + (1×400)**
**= 840 + 2700 + 3300 + 400 = 7,240 ms = 7.24 seconds**

---

#### Screen 12: Admin — Manage Societies (`frmManageSocieties`)
**Task:** Admin approves a pending society

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about pending list | M |
| 2 | Move to mouse | H |
| 3 | Point to pending society | P |
| 4 | Click row | K |
| 5 | Think about approving | M |
| 6 | Point to Approve button | P |
| 7 | Click Approve | K |

**Operator count:** M=2, H=1, P=2, K=2

**Time = (2×280) + (2×1350) + (2×1100) + (1×400)**
**= 560 + 2700 + 2200 + 400 = 5,860 ms = 5.86 seconds**

---

#### Screen 13: Admin — Approve Events (`frmApproveEvents`)
**Task:** Admin approves a pending event

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about pending event | M |
| 2 | Move to mouse | H |
| 3 | Point to event row | P |
| 4 | Click row | K |
| 5 | Think before approving | M |
| 6 | Point to Approve button | P |
| 7 | Click Approve | K |

**Operator count:** M=2, H=1, P=2, K=2

**Time = (2×280) + (2×1350) + (2×1100) + (1×400)**
**= 560 + 2700 + 2200 + 400 = 5,860 ms = 5.86 seconds**

---

#### Screen 14: Admin — University Reports (`frmUniversityReports`)
**Task:** Admin generates a university-wide report

| Step | Action | Operator |
|------|--------|----------|
| 1 | Think about which report | M |
| 2 | Move to mouse | H |
| 3 | Point to report tab | P |
| 4 | Click tab | K |
| 5 | Point to Generate button | P |
| 6 | Click Generate | K |

**Operator count:** M=1, H=1, P=2, K=2

**Time = (2×280) + (1×1350) + (2×1100) + (1×400)**
**= 560 + 1350 + 2200 + 400 = 4,510 ms = 4.51 seconds**

---

### KLM Summary Table (Deliverable)

| # | Screen / UI | Primary Task | K | M | P | H | Total Time (ms) | Total Time (s) |
|---|------------|-------------|---|---|---|---|----------------|----------------|
| 1 | frmLogin | Login with credentials | 29 | 1 | 3 | 2 | 13,570 | 13.57 |
| 2 | frmRegister | Register new account | 62 | 1 | 5 | 2 | 25,010 | 25.01 |
| 3 | frmBrowseSocieties | Apply to a society | 2 | 2 | 2 | 1 | 5,860 | 5.86 |
| 4 | frmMyMemberships | View membership status | 2 | 1 | 1 | 1 | 3,410 | 3.41 |
| 5 | frmEvents | Register for event | 2 | 2 | 2 | 1 | 5,860 | 5.86 |
| 6 | frmMyTickets | View ticket | 1 | 1 | 1 | 1 | 3,130 | 3.13 |
| 7 | frmManageMembers | Approve membership | 2 | 2 | 2 | 1 | 5,860 | 5.86 |
| 8 | frmManageEvents | Create new event | 76 | 1 | 5 | 3 | 29,330 | 29.33 |
| 9 | frmAssignTasks | Assign task to member | 52 | 1 | 5 | 3 | 22,610 | 22.61 |
| 10 | frmSocietyReports | Generate society report | 2 | 1 | 2 | 1 | 4,510 | 4.51 |
| 11 | frmManageStudents | Delete student account | 3 | 2 | 3 | 1 | 7,240 | 7.24 |
| 12 | frmManageSocieties | Approve society | 2 | 2 | 2 | 1 | 5,860 | 5.86 |
| 13 | frmApproveEvents | Approve event | 2 | 2 | 2 | 1 | 5,860 | 5.86 |
| 14 | frmUniversityReports | Generate university report | 2 | 1 | 2 | 1 | 4,510 | 4.51 |

**Most time-consuming UI:** `frmManageEvents` (29.33s) — high keystroke count due to form entry
**Fastest UI:** `frmMyTickets` (3.13s) — read-only, minimal interaction

---

---

## Task 7 — COCOMO Model

### Which Model to Select and Why

**Select: Basic COCOMO**

**Justification:**
- The project is a single self-contained desktop application with well-understood requirements
- The team is small (3 students) working in a familiar environment (C# WinForms)
- Requirements were fully given upfront (not evolving) — no need for the intermediate/detailed model's phase-level cost drivers
- Basic COCOMO is appropriate when you have LOC estimated and need a straightforward effort calculation without detailed attribute ratings

**Project Mode: Semi-detached**

Justification for semi-detached (not organic, not embedded):
- More complex than a trivial class project (multi-role system, DB, multiple modules) → not organic
- Not a hard real-time or hardware-constrained system → not embedded
- A moderately complex desktop app built by a mixed-experience team = semi-detached

---

### Basic COCOMO Formulas

```
E  = a × (KLOC)^b        [Person-Months]
D  = c × (E)^d           [Duration in Months]
P  = E / D               [People required]

For Semi-detached mode:
  a = 3.0,  b = 1.12
  c = 2.5,  d = 0.35
```

---

### Step 1: Estimate KLOC

After generating the code, count total LOC using Lizard or VS Code Metrics, then convert:

```
Example (replace with your actual count):
Total LOC = 3,200 lines
KLOC = 3.2
```

---

### Step 2: Apply Formulas

```
E = 3.0 × (3.2)^1.12
  = 3.0 × 3.64
  = 10.92 Person-Months

D = 2.5 × (10.92)^0.35
  = 2.5 × 2.41
  = 6.03 Months

P = 10.92 / 6.03
  = 1.81 ≈ 2 People
```

---

### Deliverable Format

| Parameter | Value |
|-----------|-------|
| COCOMO Type | Basic COCOMO |
| Project Mode | Semi-detached |
| Total LOC | [your actual count] |
| KLOC | [LOC / 1000] |
| Effort (E) | [calculated] Person-Months |
| Duration (D) | [calculated] Months |
| People Required (P) | [calculated] |

**Then write a paragraph:**

> "Basic COCOMO was selected because the project requirements were fully defined prior to development and the system complexity is moderate. The semi-detached mode was chosen as the application involves multi-role functionality with database integration, placing it above a simple organic project, while it lacks the hardware and real-time constraints that characterize embedded systems. With [X] KLOC of generated code, the estimated effort is [E] person-months over [D] months, requiring approximately [P] developers — consistent with the 3-person team assigned to this project."

---

---

## Task 8 — Documentation Ratio

### Formula (given)

```
Documentation Ratio = Total LOC / Commented Lines
```

> Note: Some sources define it the other way (Comments / Total LOC × 100%). Use exactly what the assignment states: Total LOC ÷ Commented Lines. A **lower ratio** means better documentation (more comments per line of code).

---

### How to Measure

**Option 1 — Lizard + cloc (recommended)**

```bash
# Install cloc
pip install cloc
# OR download from: https://github.com/AlDanial/cloc

# Run in your project folder
cloc . --include-lang=C#
```

cloc output gives you:
- **Blank lines**
- **Comment lines** ← use this as "Commented Lines"
- **Code lines** ← use this as "Total LOC"

**Option 2 — Manual count in Visual Studio**

Use Find (Ctrl+F) → check "Use Regular Expressions" → search:
```
^\s*//
```
This finds all single-line comment lines. Count the matches per file.

For block comments search:
```
/\*[\s\S]*?\*/
```

---

### Example Calculation

```
cloc output example:
  Language    Files   Blank   Comment   Code
  C#          24      420     186       3,014

Total LOC      = 3,014
Commented Lines = 186

Documentation Ratio = 3014 / 186 = 16.2
```

Interpretation: For every 16 lines of code, there is 1 comment line. A ratio below 10 is generally considered well-documented.

---

### Deliverable Format

| Metric | Value |
|--------|-------|
| Total Files | [count] |
| Total LOC (Code lines) | [count] |
| Blank Lines | [count] |
| Comment Lines | [count] |
| **Documentation Ratio** | **Total LOC / Comment Lines = X** |

**Then write 2–3 sentences:**

> "The documentation ratio of [X] indicates that vibe-coded (AI-generated) code produces [well/poorly] documented output. AI tools like GitHub Copilot and Claude tend to generate functional code with minimal inline comments, as they optimize for syntactic correctness rather than human readability. A ratio of [X] means one comment exists for every [X] lines of code, which [meets / falls below] the commonly accepted threshold of 1 comment per 10 lines (ratio ≤ 10)."

---

## Report Section Order (Final)

```
1. Introduction
2. Task 1 — Code + Database Schema + ERD
3. Task 2 — Cyclomatic Complexity + Test Cases
4. Task 3 — Structural Metric Comparison
5. Task 4 — CK Metrics Analysis
6. Task 5 — Fault Injection + Reliability Table
7. Task 6 — KLM Usability Evaluation
8. Task 7 — COCOMO Model
9. Task 8 — Documentation Ratio
10. Conclusion
```
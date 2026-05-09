# Task 5, 6, 7, 8 Analysis Requirements
# SE-4011 Software Measurement and Metrics
# Societies Management System

---

# TASK 5 — Fault Injection + Reliability Analysis

## What You Need To Do

Inject exactly 5 faults into each module/feature of the application, then calculate the
probability that each module has no more than 1 fault (confidence threshold E = 1).

## Fault Injection Rules

For each module, inject these 5 types of faults (one of each):

```
Fault 1 — Off-by-one error
  Example: Use <= instead of <, or i+1 instead of i

Fault 2 — Wrong condition
  Example: Change == to !=, or AND to OR in an if statement

Fault 3 — Null reference (missing null check)
  Example: Remove a null check before using an object

Fault 4 — Wrong SQL / Logic error
  Example: Change WHERE clause, or use wrong column name

Fault 5 — Missing validation
  Example: Remove an empty field check before DB insert
```

## Reliability Formula

Use the Poisson distribution to calculate probability of no more than E=1 fault:

```
P(x ≤ E) = P(x ≤ 1) = P(x=0) + P(x=1)

Where:
  λ (lambda) = average number of faults remaining after testing
  λ = faults_injected / (test_cases_run + 1)
     = 5 / (CC_value + 1)    ← use Cyclomatic Complexity from Task 2 as test cases run

P(x=0) = e^(-λ)
P(x=1) = λ × e^(-λ)
P(x ≤ 1) = e^(-λ) × (1 + λ)

Final probability = round to 4 decimal places
Express as percentage too: × 100
```

## Example Calculation

```
Module: LoginForm
Faults Injected = 5
CC from Task 2 = 6 (so 6 test cases run)
λ = 5 / (6 + 1) = 5/7 = 0.7143

P(x ≤ 1) = e^(-0.7143) × (1 + 0.7143)
          = 0.4895 × 1.7143
          = 0.8392
          = 83.92%
```

## Step-by-Step Instructions for Cursor

1. Read task2-cyclomatic-complexity.md to get CC value for each module
2. For each module/class list the 5 injected faults specifically
3. Calculate λ = 5 / (CC + 1)
4. Calculate P(x ≤ 1) = e^(-λ) × (1 + λ)
5. Rank all modules by probability (highest to lowest)
6. Identify most and least reliable

## Output Format Required

```markdown
## Fault Injection Details (per module)

### LoginForm
| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed password length check from > 0 to >= 0 | btnLogin_Click |
| 2 | Wrong condition | Changed == to != in role comparison | btnLogin_Click |
| 3 | Null reference | Removed null check on user object | btnLogin_Click |
| 4 | Wrong SQL | Changed AND to OR in WHERE clause of login query | DBConnection |
| 5 | Missing validation | Removed empty email field check | btnLogin_Click |

[Repeat for every module]

---

## Reliability Summary Table

| # | Module/Feature | Faults Injected | CC (Test Cases) | λ (lambda) | P(x ≤ 1) | Reliability % | Rank |
|---|---------------|-----------------|-----------------|------------|----------|---------------|------|
| 1 | LoginForm | 5 | 6 | 0.7143 | 0.8392 | 83.92% | X |
| 2 | RegisterForm | 5 | X | X | X | X% | X |
| 3 | BrowseSocieties | 5 | X | X | X | X% | X |
| 4 | MyMemberships | 5 | X | X | X | X% | X |
| 5 | BrowseEvents | 5 | X | X | X | X% | X |
| 6 | MyTickets | 5 | X | X | X | X% | X |
| 7 | SocietyDashboard | 5 | X | X | X | X% | X |
| 8 | ManageMembers | 5 | X | X | X | X% | X |
| 9 | ManageEvents | 5 | X | X | X | X% | X |
| 10 | ManageTasks | 5 | X | X | X | X% | X |
| 11 | SocietyReports | 5 | X | X | X | X% | X |
| 12 | AdminDashboard | 5 | X | X | X | X% | X |
| 13 | ManageUsers | 5 | X | X | X | X% | X |
| 14 | ManageSocieties | 5 | X | X | X | X% | X |
| 15 | ApproveEvents | 5 | X | X | X | X% | X |
| 16 | AdminReports | 5 | X | X | X | X% | X |
| 17 | DBConnection | 5 | X | X | X | X% | X |

---

## Conclusions

### Most Reliable Function/Feature
- Module: [Name]
- Reliability: X%
- Reason: This module has the highest CC value (most test cases run = X),
  meaning more paths were tested, resulting in the lowest λ and highest
  probability of having ≤1 fault remaining.

### Least Reliable Function/Feature
- Module: [Name]
- Reliability: X%
- Reason: This module has the lowest CC value (fewest test cases = X),
  meaning fewer paths were tested, resulting in the highest λ and lowest
  probability of having ≤1 fault remaining.
```

---

# TASK 6 — KLM Usability Evaluation

## What You Need To Do

Evaluate every UI screen in the application using the Keystroke Level Model (KLM).
Calculate the total execution time for completing the PRIMARY task on each screen.

## KLM Operators

```
K = Keystroke (pressing one key)         = 280 ms
M = Mental preparation (before action)  = 1350 ms
P = Pointing (moving mouse to target)   = 1100 ms
H = Hand movement keyboard ↔ mouse      = 400 ms
```

## KLM Rules

```
Rule 1: M placement
  - Place M before every K that begins a sequence of keystrokes
  - Place M before every P that selects a command
  - Do NOT place M between keystrokes in a sequence
  - Do NOT place M before a P that follows another P

Rule 2: Counting K
  - Each character typed = 1K
  - Pressing Enter/Tab/Backspace = 1K each
  - But for text fields: count average characters, not exact
    (use realistic averages: email = 20K, password = 8K, name = 15K)

Rule 3: Counting P
  - Moving mouse to any button, field, or control = 1P
  - Clicking = included in P (no separate K for click)

Rule 4: Counting H
  - Each switch between keyboard and mouse = 1H
  - Starting at keyboard then moving to mouse = 1H
  - Starting at mouse then moving to keyboard = 1H
```

## Screens to Evaluate

Evaluate the PRIMARY TASK for each screen:

```
Screen 1:  LoginForm          → Task: Log in with email and password
Screen 2:  RegisterForm       → Task: Register a new student account
Screen 3:  StudentDashboard   → Task: Navigate to Browse Societies
Screen 4:  BrowseSocieties    → Task: Apply for membership in a society
Screen 5:  MyMemberships      → Task: View membership status
Screen 6:  BrowseEvents       → Task: Register for an event
Screen 7:  MyTickets          → Task: View event passes
Screen 8:  SocietyDashboard   → Task: Navigate to Manage Members
Screen 9:  ManageMembers      → Task: Approve a membership request
Screen 10: ManageEvents       → Task: Create a new event
Screen 11: ManageTasks        → Task: Assign a task to a member
Screen 12: SocietyReports     → Task: Generate member report
Screen 13: AdminDashboard     → Task: Navigate to Manage Societies
Screen 14: ManageUsers        → Task: Search and delete a user
Screen 15: ManageSocieties    → Task: Approve a pending society
Screen 16: ApproveEvents      → Task: Approve a pending event
Screen 17: AdminReports       → Task: View university-wide report
```

## Output Format Required

For each screen, show the operator sequence then calculate total time:

```markdown
## KLM Evaluation Results

---

### Screen 1: LoginForm
**Primary Task**: Log in with email and password

| Step | Action | Operator | Time (ms) |
|------|--------|----------|-----------|
| 1 | Think before starting | M | 1350 |
| 2 | Move mouse to Email field | P | 1100 |
| 3 | Switch to keyboard | H | 400 |
| 4 | Think before typing email | M | 1350 |
| 5 | Type email (20 chars) | 20K | 5600 |
| 6 | Press Tab to password field | K | 280 |
| 7 | Think before typing password | M | 1350 |
| 8 | Type password (8 chars) | 8K | 2240 |
| 9 | Switch to mouse | H | 400 |
| 10 | Move mouse to Login button | P | 1100 |
| 11 | Think before clicking | M | 1350 |
| 12 | Click Login button | P | 1100 |
| **TOTAL** | | | **16620 ms = 16.62 seconds** |

---

### Screen 2: RegisterForm
**Primary Task**: Register a new student account

| Step | Action | Operator | Time (ms) |
...

[Continue for all 17 screens]

---

## KLM Summary Table

| # | Screen | Operator Sequence | Total Time (ms) | Total Time (sec) | Efficiency Rank |
|---|--------|-------------------|-----------------|------------------|-----------------|
| 1 | LoginForm | M P H M 20K K M 8K H P M P | 16620 | 16.62s | X |
| 2 | RegisterForm | ... | X | Xs | X |
...
| 17 | AdminReports | ... | X | Xs | X |

---

## Usability Analysis

### Fastest Screen (Most Efficient)
- Screen: [Name] with [X ms]
- Why: Minimal steps, fewer fields, simple task

### Slowest Screen (Least Efficient)
- Screen: [Name] with [X ms]
- Why: Many fields, complex task, multiple interactions

### Overall Observations
- Average task completion time: X ms
- Total keyboard time vs mouse time breakdown
- Recommendations to reduce time (e.g., Tab order optimization, default values, keyboard shortcuts)
```

---

# TASK 7 — COCOMO Model

## What You Need To Do

Count total Lines of Code (LOC) in the project, select the appropriate COCOMO model,
justify your selection, and calculate effort, duration, and team size.

## Step 1: Count LOC

```
Instructions for Cursor:
- Scan every .cs file in the project
- Count ALL lines including blank lines and comments (physical LOC)
- Also count only non-blank, non-comment lines (logical LOC)
- Convert to KLOC = LOC / 1000
- Report both numbers
```

## Step 2: COCOMO Model Selection

### Which Model to Use

```
Basic COCOMO — use when:
  ✓ Simple project
  ✓ Well-understood requirements
  ✓ Small team (2-5 people)
  ✓ No complex algorithms
  → THIS IS YOUR MODEL (3 students, simple desktop app, clear requirements)

Intermediate COCOMO — use when:
  - More experience needed
  - Some novel elements
  - Medium team

Detailed COCOMO — use when:
  - Large complex system
  - Multiple subsystems
  - Enterprise level
```

### Project Mode Selection

```
Organic Mode — use when:
  ✓ Small team (< 5 people) → YES (3 students)
  ✓ Familiar with application type → YES (standard desktop app)
  ✓ Flexible requirements → YES (university project)
  ✓ KLOC < 50 → YES (expected ~5-15 KLOC)
  → USE ORGANIC MODE

Semi-detached Mode — medium projects
Embedded Mode — strict hardware/software constraints
```

## Step 3: COCOMO Formulas (Organic Mode)

```
Effort (E)   = 2.4 × (KLOC)^1.05    [person-months]
Duration (D) = 2.5 × (E)^0.38       [months]
Team Size    = E / D                  [people]
Productivity = KLOC / E              [KLOC per person-month]

Constants for Organic mode:
  a = 2.4, b = 1.05 (for Effort)
  c = 2.5, d = 0.38 (for Duration)
```

## Step 4: Example Calculation

```
Assume KLOC = 8 (adjust with actual value)

E = 2.4 × (8)^1.05
  = 2.4 × 8.5748
  = 20.58 person-months

D = 2.5 × (20.58)^0.38
  = 2.5 × 3.456
  = 8.64 months

Team Size = 20.58 / 8.64 = 2.38 ≈ 3 people  ← matches our 3-person team!

Productivity = 8 / 20.58 = 0.39 KLOC/person-month
```

## Output Format Required

```markdown
## COCOMO Analysis

### Step 1: LOC Count

| File | Physical LOC | Logical LOC | Comment Lines |
|------|-------------|-------------|---------------|
| LoginForm.cs | X | X | X |
| RegisterForm.cs | X | X | X |
...
| **TOTAL** | **X** | **X** | **X** |

Total Physical LOC = X
Total Logical LOC = X
KLOC = X / 1000 = X KLOC

---

### Step 2: Model Justification

**Selected Model**: Basic COCOMO
**Selected Mode**: Organic

**Justification**:
Basic COCOMO is selected because this is a small, well-defined desktop
application developed by a team of 3 students with clear and stable
requirements. The project scope is limited to a single-platform WinForms
application without complex real-time or embedded constraints.

Organic mode is selected because:
1. Team size is 3 people (well below the 5-person threshold)
2. The team is familiar with C# and database applications
3. Requirements are flexible and well-understood from the project brief
4. Estimated KLOC falls well below 50 KLOC
5. No hardware or real-time constraints exist

**Why not Intermediate?**: No cost drivers need adjustment for this
standard academic project. Basic COCOMO provides sufficient accuracy.

**Why not Embedded?**: The application has no real-time requirements,
no hardware interfacing, and no strict performance constraints.

---

### Step 3: COCOMO Calculations

| Parameter | Formula | Calculation | Result |
|-----------|---------|-------------|--------|
| KLOC | Total LOC / 1000 | X / 1000 | X KLOC |
| Effort (E) | 2.4 × (KLOC)^1.05 | 2.4 × (X)^1.05 | X person-months |
| Duration (D) | 2.5 × (E)^0.38 | 2.5 × (X)^0.38 | X months |
| Team Size | E / D | X / X | X people |
| Productivity | KLOC / E | X / X | X KLOC/person-month |

---

### Step 4: Interpretation

- The estimated effort of **X person-months** means one developer would
  need X months to complete this project alone.
- With a team of 3, the actual duration is approximately **X months**.
- This aligns with the actual development time of approximately **1 month**
  for this academic project, validating our COCOMO model selection.
- The team size estimate of **~3 people** exactly matches our actual group size.
```

---

# TASK 8 — Documentation Ratio

## What You Need To Do

Calculate the Documentation Ratio for every file and the whole project.

## Formula

```
Documentation Ratio = Total LOC / Commented Lines

Note: LOWER ratio = BETTER documented
(means more comments relative to code)

Example:
Total LOC = 1000
Commented Lines = 200
Documentation Ratio = 1000 / 200 = 5.0

Interpretation:
Ratio = 1.0 → every line is a comment (over-documented)
Ratio = 5.0 → 1 comment per 5 lines of code (good)
Ratio = 10.0 → 1 comment per 10 lines (under-documented)
Ratio > 15 → poorly documented
```

## What Counts as a Commented Line

```
Count these as commented lines:
✓ Single-line comments:     // this is a comment
✓ XML doc comments:         /// <summary>
✓ XML doc content:          /// <param name="x">
✓ Block comment lines:      /* comment */
✓ Block comment start:      /*
✓ Block comment end:        */
✓ Inline comments count the whole line if comment is present

Do NOT count:
✗ Blank/empty lines
✗ Lines with only code (no comment)
✗ Closing braces }
```

## Step-by-Step Instructions for Cursor

```
1. Scan every .cs file in the project
2. For each file count:
   a. Total lines (including blank)
   b. Total LOC (non-blank lines)
   c. Commented lines (any line with // or /* or ///)
3. Calculate per-file ratio = LOC / Commented Lines
4. Calculate overall project ratio = Total LOC / Total Commented Lines
5. Classify each file as: Well-documented / Average / Poorly-documented
```

## Classification Thresholds

```
Ratio < 5    → Well-documented (excellent)
Ratio 5-10   → Average (acceptable)
Ratio 10-15  → Under-documented (needs improvement)
Ratio > 15   → Poorly documented (unacceptable)
```

## Output Format Required

```markdown
## Documentation Ratio Analysis

### Per-File Breakdown

| # | File | Total Lines | LOC | Commented Lines | Documentation Ratio | Classification |
|---|------|-------------|-----|-----------------|--------------------|--------------:|
| 1 | DBConnection.cs | X | X | X | X.X | Well-documented |
| 2 | Session.cs | X | X | X | X.X | Average |
| 3 | LoginForm.cs | X | X | X | X.X | Under-documented |
| 4 | RegisterForm.cs | X | X | X | X.X | ... |
| 5 | StudentDashboard.cs | X | X | X | X.X | ... |
| 6 | BrowseSocieties.cs | X | X | X | X.X | ... |
| 7 | MyMemberships.cs | X | X | X | X.X | ... |
| 8 | BrowseEvents.cs | X | X | X | X.X | ... |
| 9 | MyTickets.cs | X | X | X | X.X | ... |
| 10 | SocietyDashboard.cs | X | X | X | X.X | ... |
| 11 | ManageMembers.cs | X | X | X | X.X | ... |
| 12 | ManageEvents.cs | X | X | X | X.X | ... |
| 13 | ManageTasks.cs | X | X | X | X.X | ... |
| 14 | SocietyReports.cs | X | X | X | X.X | ... |
| 15 | AdminDashboard.cs | X | X | X | X.X | ... |
| 16 | ManageUsers.cs | X | X | X | X.X | ... |
| 17 | ManageSocieties.cs | X | X | X | X.X | ... |
| 18 | ApproveEvents.cs | X | X | X | X.X | ... |
| 19 | AdminReports.cs | X | X | X | X.X | ... |
| 20 | User.cs | X | X | X | X.X | ... |
| 21 | Society.cs | X | X | X | X.X | ... |
| 22 | Event.cs | X | X | X | X.X | ... |
| 23 | Membership.cs | X | X | X | X.X | ... |
| 24 | Task.cs | X | X | X | X.X | ... |
| 25 | Announcement.cs | X | X | X | X.X | ... |
| | **PROJECT TOTAL** | **X** | **X** | **X** | **X.X** | **[Classification]** |

---

### Summary Statistics

| Metric | Value |
|--------|-------|
| Total Lines (all files) | X |
| Total LOC (non-blank) | X |
| Total Commented Lines | X |
| **Overall Documentation Ratio** | **X.X** |
| Overall Classification | X |
| Best Documented File | [Name] (Ratio = X.X) |
| Worst Documented File | [Name] (Ratio = X.X) |
| Files Well-documented (< 5) | X out of 25 |
| Files Average (5-10) | X out of 25 |
| Files Under-documented (> 10) | X out of 25 |

---

### Analysis of Vibe-Coded Documentation

**Observation about AI/Vibe-Coded projects:**
When code is generated using AI tools (vibe coding), documentation quality
depends entirely on how well the prompts specified comment requirements.

**Findings**:
- The overall project ratio of [X.X] indicates the code is [classification]
- AI-generated code tends to [under/over]-document because [reason based on actual ratio]
- Files with ratio > 10 need additional hand-written comments before submission
- The [best file] is best documented likely because [reason]

**Recommendation**:
Files with ratio > 10 should have comments added manually. Target ratio
should be between 5-8 for a balanced, readable codebase.
```

---

# HOW TO USE THIS IN CURSOR

Run these prompts one at a time:

## Prompt 1 — Task 5
```
Read task5-6-7-8-requirements.md.
Also read analysis/task2-cyclomatic-complexity.md for CC values.
Perform Task 5: inject 5 faults per module, calculate lambda and
reliability probability P(x ≤ 1) using Poisson formula for each module.
Show fault injection details per module, then the full summary table.
Identify most and least reliable module.
Save to analysis/task5-fault-injection.md
```

## Prompt 2 — Task 6
```
Read task5-6-7-8-requirements.md.
Perform Task 6: KLM evaluation for all 17 UI screens.
For each screen show the step-by-step operator table with times.
Produce the summary table ranked by total time.
Identify fastest and slowest screen.
Save to analysis/task6-klm-evaluation.md
```

## Prompt 3 — Task 7
```
Read task5-6-7-8-requirements.md.
Count LOC in every .cs file.
Perform Task 7: select and apply COCOMO model with full justification.
Show all calculations in the table format specified.
Save to analysis/task7-cocomo.md
```

## Prompt 4 — Task 8
```
Read task5-6-7-8-requirements.md.
Scan every .cs file and count total lines, LOC, and commented lines.
Perform Task 8: calculate documentation ratio per file and overall.
Classify each file.
Save to analysis/task8-documentation-ratio.md
```

---

# EXPECTED OUTPUT FILES

```
analysis/
├── task2-cyclomatic-complexity.md   (from previous requirements)
├── task3-structural-metric.md       (from previous requirements)
├── task4-ck-metrics.md              (from previous requirements)
├── task5-fault-injection.md         ← NEW
├── task6-klm-evaluation.md          ← NEW
├── task7-cocomo.md                  ← NEW
└── task8-documentation-ratio.md     ← NEW
```

Copy all 6 analysis files into your final Word document for submission.
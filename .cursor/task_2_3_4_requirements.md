# Task 2, 3, 4 Analysis Requirements
# SE-4011 Software Measurement and Metrics
# Societies Management System

---

# TASK 2 — Cyclomatic Complexity + Test Cases

## What You Need To Do

Go through EVERY method/function in the entire project and compute its Cyclomatic Complexity.

## Formula

```
M = E - N + 2P
Where:
  E = number of edges in the control flow graph
  N = number of nodes in the control flow graph
  P = number of connected components (always 1 for a single method)

Simpler counting rule (use this):
M = Number of decision points + 1

Decision points are: if, else if, while, for, foreach, case, catch, &&, ||, ternary (? :)
```

## Step-by-Step Instructions for Cursor

1. Scan every `.cs` file in the project
2. For every `public`, `private`, and `protected` method, count all decision points
3. Add 1 to get Cyclomatic Complexity
4. Write test cases equal to the complexity number (one test per independent path)

## Output Format Required

Generate a markdown table with this EXACT structure:

```
| # | Class Name | Method Name | Decision Points Found | Cyclomatic Complexity (M) | Test Case Inputs |
|---|------------|-------------|----------------------|--------------------------|-----------------|
| 1 | LoginForm | btnLogin_Click | if(email empty), if(password empty), if(user found) | 4 | TC1: empty email → error; TC2: empty password → error; TC3: wrong credentials → error; TC4: valid credentials → redirect |
| 2 | DBConnection | ExecuteNonQuery | try/catch | 2 | TC1: valid query → success; TC2: invalid query → exception caught |
...
```

## Rules
- Every method must be listed, even simple ones (complexity = 1 if no decisions)
- Test case inputs must be SPECIFIC (actual values, not just "valid input")
- Group by Class Name
- At the end, add a summary:
  - Total methods analyzed
  - Average cyclomatic complexity
  - Highest complexity method (most complex)
  - Lowest complexity method (simplest)
  - Methods with M > 10 (need refactoring)

## Example Analysis for a Login Method

```csharp
// Example method
private void btnLogin_Click(object sender, EventArgs e) {
    if (string.IsNullOrEmpty(txtEmail.Text))        // decision 1
    {
        MessageBox.Show("Email required");
        return;
    }
    if (string.IsNullOrEmpty(txtPassword.Text))     // decision 2
    {
        MessageBox.Show("Password required");
        return;
    }
    var user = db.GetUser(txtEmail.Text, txtPassword.Text);
    if (user == null)                               // decision 3
    {
        MessageBox.Show("Invalid credentials");
        return;
    }
    if (user.Role == "Admin")                       // decision 4
        OpenAdminDashboard();
    else if (user.Role == "SocietyHead")            // decision 5
        OpenSocietyDashboard();
    else
        OpenStudentDashboard();
}
// M = 5 + 1 = 6
// Need 6 test cases
```

---

# TASK 3 — Best Module Identification Using Structural Metric

## What You Need To Do

Identify the BEST module (class/form) in the project using a structural metric of your choice. You must justify your choice of metric and defend which module is best.

## Chosen Metric: Lines of Code (LOC) + Fan-Out (Structural Complexity)

Use BOTH of these metrics together:

### Metric 1: Lines of Code (LOC)
- Count non-empty, non-comment lines in each class
- Lower LOC = simpler, more focused module

### Metric 2: Fan-Out
- Count how many OTHER classes this class calls/uses
- Lower Fan-Out = less dependent, more independent module

### Metric 3: Comment Ratio
- (Commented Lines / Total Lines) × 100
- Higher % = better documented

### Best Module = Lowest LOC + Lowest Fan-Out + Highest Comment Ratio

## Step-by-Step Instructions for Cursor

1. Scan every `.cs` file (Forms, Models, Database folder)
2. For each class/file count:
   - Total lines
   - Non-empty lines (LOC)
   - Comment lines (lines starting with // or inside /* */)
   - How many other classes it references (Fan-Out)
3. Calculate Comment Ratio = (Comment Lines / Total Lines) × 100
4. Rank modules from best to worst

## Output Format Required

```
## Metric Selection Justification
[Write 2-3 sentences: "We selected LOC and Fan-Out as structural metrics because...
LOC measures module size and complexity while Fan-Out measures coupling between modules.
A good module should be small, focused, and independent."]

## Comparative Table

| # | Module/Class | Total Lines | LOC | Comment Lines | Comment Ratio % | Fan-Out (dependencies) | Overall Rank |
|---|-------------|-------------|-----|---------------|-----------------|----------------------|-------------|
| 1 | DBConnection | 80 | 60 | 20 | 25% | 0 | 1st (Best) |
| 2 | User (Model) | 30 | 20 | 10 | 33% | 0 | 2nd |
| 3 | LoginForm | 120 | 90 | 30 | 25% | 3 | 3rd |
...

## Conclusion
Best Module: [Name]
Reason: It has the lowest LOC (X lines), lowest Fan-Out (X dependencies), 
and highest comment ratio (X%), making it the most well-structured, 
independent, and maintainable module in the system.

Worst Module: [Name]  
Reason: It has the highest LOC (X lines) and Fan-Out (X), indicating 
it handles too many responsibilities and is tightly coupled.
```

---

# TASK 4 — CK Metrics Suite

## What You Need To Do

Apply all 6 CK (Chidamber & Kemerer) metrics to EVERY class in the project.

## The 6 Metrics — Definitions and How to Count

### 1. WMC — Weighted Methods per Class
```
WMC = Sum of Cyclomatic Complexity of ALL methods in the class
(Use the values calculated in Task 2)

Example:
Class LoginForm has 3 methods with CC = 6, 2, 1
WMC = 6 + 2 + 1 = 9
```

### 2. DIT — Depth of Inheritance Tree
```
DIT = How many levels deep is this class in the inheritance chain

Rules:
- A class that extends nothing (or only Object) → DIT = 0
- A class that extends Form → DIT = 1
- A class that extends another custom Form → DIT = 2

For WinForms:
- All Form classes → DIT = 1 (they inherit from System.Windows.Forms.Form)
- Model classes (no inheritance) → DIT = 0
- DBConnection → DIT = 0
```

### 3. NOC — Number of Children
```
NOC = How many classes DIRECTLY inherit from this class

For most WinForms projects: NOC = 0 for almost everything
Exception: if you have a BaseForm that other forms inherit from → NOC = number of those forms
```

### 4. CBO — Coupling Between Objects
```
CBO = Number of OTHER classes that this class uses or is used by

How to count:
- Count every unique class name referenced inside this class
- Include: instantiated objects, method parameters, return types
- Exclude: System.* built-in types (String, Int, List, etc.)
- Exclude: the class itself

Example: LoginForm uses DBConnection, Session, StudentDashboard, AdminDashboard, SocietyDashboard
CBO = 5
```

### 5. RFC — Response for Class
```
RFC = Number of methods in the class + Number of methods called by those methods

How to count:
- Count all methods defined IN the class
- Count all unique external method calls made from inside the class
- RFC = own methods + external method calls

Example:
LoginForm has 2 own methods (btnLogin_Click, LoadForm)
Inside those methods it calls: MessageBox.Show, db.GetUser, Session.Set, form.Show
RFC = 2 + 4 = 6
```

### 6. LCOM — Lack of Cohesion in Methods
```
LCOM = P - Q (if P > Q, else 0)
Where:
  P = number of method pairs that DO NOT share instance variables
  Q = number of method pairs that DO share instance variables

Simpler interpretation:
- LCOM = 0 → highly cohesive (good)
- LCOM > 0 → low cohesion (methods not working on same data)

How to compute:
1. List all instance variables (fields) of the class
2. For each method, note which instance variables it uses
3. For each pair of methods: if they share NO variables → P++, if they share ≥1 variable → Q++
4. LCOM = max(0, P - Q)
```

## Step-by-Step Instructions for Cursor

1. List every class in the project
2. For each class, compute all 6 metrics using definitions above
3. Use Task 2 CC values for WMC calculation
4. Produce the full table
5. Answer the 5 required questions at the end

## Output Format Required

```
## CK Metrics Table

| Class | WMC | DIT | NOC | CBO | RFC | LCOM |
|-------|-----|-----|-----|-----|-----|------|
| DBConnection | X | X | X | X | X | X |
| Session | X | X | X | X | X | X |
| User | X | X | X | X | X | X |
| Society | X | X | X | X | X | X |
| Event | X | X | X | X | X | X |
| Membership | X | X | X | X | X | X |
| Task | X | X | X | X | X | X |
| Announcement | X | X | X | X | X | X |
| LoginForm | X | X | X | X | X | X |
| RegisterForm | X | X | X | X | X | X |
| StudentDashboard | X | X | X | X | X | X |
| BrowseSocieties | X | X | X | X | X | X |
| MyMemberships | X | X | X | X | X | X |
| BrowseEvents | X | X | X | X | X | X |
| MyTickets | X | X | X | X | X | X |
| SocietyDashboard | X | X | X | X | X | X |
| ManageMembers | X | X | X | X | X | X |
| ManageEvents | X | X | X | X | X | X |
| ManageTasks | X | X | X | X | X | X |
| SocietyReports | X | X | X | X | X | X |
| AdminDashboard | X | X | X | X | X | X |
| ManageUsers | X | X | X | X | X | X |
| ManageSocieties | X | X | X | X | X | X |
| ApproveEvents | X | X | X | X | X | X |
| AdminReports | X | X | X | X | X | X |

---

## Required Analysis Questions

### Q1: Maximum Depth of Inheritance
- Which class has the highest DIT value?
- What does this mean for the system?
- Is deep inheritance a problem here?

### Q2: Highest and Lowest WMC
- Highest WMC: [Class Name] with WMC = X
  - Explanation: This class has the most methods and highest complexity because it handles [reason]
  - Is this acceptable or a design smell?
- Lowest WMC: [Class Name] with WMC = X
  - Explanation: This class is simple because it only [reason]

### Q3: Class with Greatest Number of Children (NOC)
- Which class has NOC > 0?
- If all NOC = 0: explain why (flat hierarchy is common in WinForms)
- What would be the benefit of introducing a BaseForm class?

### Q4: Most Complex Class
- Based on WMC + RFC combined
- [Class Name] is most complex with WMC=X and RFC=X
- Why is it complex? What does it do?
- Refactoring suggestion to reduce complexity

### Q5: Most Coupled Class
- Class with highest CBO value
- [Class Name] with CBO = X
- List all the classes it is coupled to
- Why is high coupling a problem?
- How could it be reduced?

### Q6: Least Cohesive Class
- Class with highest LCOM value
- [Class Name] with LCOM = X
- Why is it least cohesive? What methods don't share data?
- How could cohesion be improved?

---

## Interpretation Thresholds (use these in your analysis)

| Metric | Good Range | Warning | Critical |
|--------|-----------|---------|----------|
| WMC | 1-10 | 11-20 | >20 |
| DIT | 0-2 | 3-4 | >4 |
| NOC | 0-5 | 6-10 | >10 |
| CBO | 0-5 | 6-10 | >10 |
| RFC | 1-20 | 21-50 | >50 |
| LCOM | 0 | 1-5 | >5 |
```

---

# HOW TO USE THIS FILE IN CURSOR

Paste these prompts into Cursor one at a time:

## Prompt 1 — Task 2
```
Read task2-3-4-requirements.md. 
Scan every .cs file in this project.
Perform Task 2: compute Cyclomatic Complexity for every method.
Output a complete markdown table as specified, grouped by class.
Include summary statistics at the end.
Save output to analysis/task2-cyclomatic-complexity.md
```

## Prompt 2 — Task 3
```
Read task2-3-4-requirements.md.
Perform Task 3: compute LOC, Fan-Out, and Comment Ratio for every class.
Use the metric definitions provided.
Output the comparative table and conclusion as specified.
Identify and justify the best and worst module.
Save output to analysis/task3-structural-metric.md
```

## Prompt 3 — Task 4
```
Read task2-3-4-requirements.md.
Perform Task 4: compute all 6 CK metrics (WMC, DIT, NOC, CBO, RFC, LCOM) for every class.
Use WMC values from task2-cyclomatic-complexity.md.
Output the complete CK metrics table.
Then answer all 6 required analysis questions with detailed explanations.
Save output to analysis/task4-ck-metrics.md
```

---

# OUTPUT FILES EXPECTED

After Cursor runs all three prompts, you should have:
```
analysis/
├── task2-cyclomatic-complexity.md
├── task3-structural-metric.md
└── task4-ck-metrics.md
```

Copy the content of these files into your final Word document submission.
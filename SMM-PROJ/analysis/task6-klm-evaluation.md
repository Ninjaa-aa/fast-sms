# Task 6 — KLM Usability Evaluation

> **Project:** Societies Management System (SMM-PROJ)
> **Screens Evaluated:** 17 UI forms
> **KLM Operators:** K = 280 ms, M = 1350 ms, P = 1100 ms, H = 400 ms

---

## KLM Operator Reference

| Operator | Meaning | Time (ms) |
|----------|---------|----------:|
| **K** | Keystroke (one key press) | 280 |
| **M** | Mental preparation (think before action) | 1350 |
| **P** | Pointing (mouse move + click) | 1100 |
| **H** | Hand movement (keyboard ↔ mouse switch) | 400 |

**M Placement Rules:**
- Place M before every K sequence that begins typing
- Place M before every P that selects a command/button
- Do NOT place M between consecutive keystrokes in a single field
- Do NOT place M before a P that immediately follows another P in a selection sequence

---

## KLM Evaluation Results

---

### Screen 1: LoginForm
**Primary Task:** Log in with email and password

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about entering credentials | M | 1350 |
| 2 | Move mouse to Email field | P | 1100 |
| 3 | Switch hand to keyboard | H | 400 |
| 4 | Think before typing email | M | 1350 |
| 5 | Type email (20 chars) | 20K | 5600 |
| 6 | Press Tab to Password field | K | 280 |
| 7 | Think before typing password | M | 1350 |
| 8 | Type password (8 chars) | 8K | 2240 |
| 9 | Switch hand to mouse | H | 400 |
| 10 | Think before clicking Login | M | 1350 |
| 11 | Move mouse to Login button | P | 1100 |
| | **TOTAL** | **M=4 H=2 P=2 K=29** | **16,520 ms (16.52s)** |

---

### Screen 2: RegisterForm
**Primary Task:** Register a new student account

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about filling form | M | 1350 |
| 2 | Move mouse to Full Name field | P | 1100 |
| 3 | Switch to keyboard | H | 400 |
| 4 | Think before typing name | M | 1350 |
| 5 | Type full name (15 chars) | 15K | 4200 |
| 6 | Press Tab to Email field | K | 280 |
| 7 | Think before typing email | M | 1350 |
| 8 | Type email (20 chars) | 20K | 5600 |
| 9 | Press Tab to Password field | K | 280 |
| 10 | Think before typing password | M | 1350 |
| 11 | Type password (8 chars) | 8K | 2240 |
| 12 | Press Tab to Confirm Password | K | 280 |
| 13 | Type confirm password (8 chars) | 8K | 2240 |
| 14 | Switch to mouse | H | 400 |
| 15 | Think before selecting role | M | 1350 |
| 16 | Move mouse to Role dropdown | P | 1100 |
| 17 | Select "Student" option | P | 1100 |
| 18 | Think before clicking Register | M | 1350 |
| 19 | Move mouse to Register button | P | 1100 |
| | **TOTAL** | **M=6 H=2 P=4 K=54** | **27,620 ms (27.62s)** |

---

### Screen 3: StudentDashboard
**Primary Task:** Navigate to Browse Societies

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about which button to click | M | 1350 |
| 2 | Move mouse to Browse Societies button | P | 1100 |
| | **TOTAL** | **M=1 P=1** | **2,450 ms (2.45s)** |

---

### Screen 4: BrowseSocieties
**Primary Task:** Apply for membership in a society

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about which society to join | M | 1350 |
| 2 | Move mouse to desired society row | P | 1100 |
| 3 | Think about applying | M | 1350 |
| 4 | Move mouse to Apply button | P | 1100 |
| | **TOTAL** | **M=2 P=2** | **4,900 ms (4.90s)** |

---

### Screen 5: MyMemberships
**Primary Task:** View membership status (read-only)

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think / read the membership list | M | 1350 |
| 2 | Move mouse to scroll through list | P | 1100 |
| 3 | Scroll down (2 scroll wheel ticks) | 2K | 560 |
| | **TOTAL** | **M=1 P=1 K=2** | **3,010 ms (3.01s)** |

---

### Screen 6: BrowseEvents
**Primary Task:** Register for an event

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about which event to register for | M | 1350 |
| 2 | Move mouse to desired event row | P | 1100 |
| 3 | Think about registering | M | 1350 |
| 4 | Move mouse to Register button | P | 1100 |
| | **TOTAL** | **M=2 P=2** | **4,900 ms (4.90s)** |

---

### Screen 7: MyTickets
**Primary Task:** View event passes (read-only)

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think / scan ticket list | M | 1350 |
| 2 | Move mouse to ticket row for details | P | 1100 |
| | **TOTAL** | **M=1 P=1** | **2,450 ms (2.45s)** |

---

### Screen 8: SocietyDashboard
**Primary Task:** Navigate to Manage Members

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about which section to manage | M | 1350 |
| 2 | Move mouse to Manage Members button | P | 1100 |
| | **TOTAL** | **M=1 P=1** | **2,450 ms (2.45s)** |

---

### Screen 9: ManageMembers
**Primary Task:** Approve a membership request

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about which request to review | M | 1350 |
| 2 | Move mouse to pending request row | P | 1100 |
| 3 | Think about approval decision | M | 1350 |
| 4 | Move mouse to Approve button | P | 1100 |
| | **TOTAL** | **M=2 P=2** | **4,900 ms (4.90s)** |

---

### Screen 10: ManageEvents
**Primary Task:** Create a new event (opens CreateEventDialog)

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about creating an event | M | 1350 |
| 2 | Move mouse to Create button | P | 1100 |
| 3 | *Dialog opens* — Move mouse to Title field | P | 1100 |
| 4 | Switch to keyboard | H | 400 |
| 5 | Think before typing title | M | 1350 |
| 6 | Type event title (20 chars) | 20K | 5600 |
| 7 | Switch to mouse | H | 400 |
| 8 | Move mouse to Description field | P | 1100 |
| 9 | Switch to keyboard | H | 400 |
| 10 | Think before typing description | M | 1350 |
| 11 | Type description (30 chars) | 30K | 8400 |
| 12 | Switch to mouse | H | 400 |
| 13 | Move mouse to Date picker | P | 1100 |
| 14 | Switch to keyboard | H | 400 |
| 15 | Type date (10 chars: dd/mm/yyyy) | 10K | 2800 |
| 16 | Switch to mouse | H | 400 |
| 17 | Move mouse to Venue field | P | 1100 |
| 18 | Switch to keyboard | H | 400 |
| 19 | Think before typing venue | M | 1350 |
| 20 | Type venue (15 chars) | 15K | 4200 |
| 21 | Switch to mouse | H | 400 |
| 22 | Think before clicking Create | M | 1350 |
| 23 | Move mouse to Create button | P | 1100 |
| | **TOTAL** | **M=5 H=8 P=7 K=75** | **34,750 ms (34.75s)** |

---

### Screen 11: ManageTasks
**Primary Task:** Assign a task to a member (opens AssignTaskDialog)

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about assigning a task | M | 1350 |
| 2 | Move mouse to Assign button | P | 1100 |
| 3 | *Dialog opens* — Think about member selection | M | 1350 |
| 4 | Move mouse to Member dropdown | P | 1100 |
| 5 | Select member from dropdown | P | 1100 |
| 6 | Move mouse to Title field | P | 1100 |
| 7 | Switch to keyboard | H | 400 |
| 8 | Think before typing title | M | 1350 |
| 9 | Type task title (20 chars) | 20K | 5600 |
| 10 | Switch to mouse | H | 400 |
| 11 | Move mouse to Description field | P | 1100 |
| 12 | Switch to keyboard | H | 400 |
| 13 | Think before typing description | M | 1350 |
| 14 | Type description (30 chars) | 30K | 8400 |
| 15 | Switch to mouse | H | 400 |
| 16 | Move mouse to Due Date picker | P | 1100 |
| 17 | Switch to keyboard | H | 400 |
| 18 | Type due date (10 chars) | 10K | 2800 |
| 19 | Switch to mouse | H | 400 |
| 20 | Think before clicking Assign | M | 1350 |
| 21 | Move mouse to Assign button | P | 1100 |
| | **TOTAL** | **M=5 H=6 P=7 K=60** | **29,750 ms (29.75s)** |

---

### Screen 12: SocietyReports
**Primary Task:** View member report (auto-loads on open)

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think / read the member report grid | M | 1350 |
| 2 | Move mouse to scroll area | P | 1100 |
| 3 | Scroll through member data (2 ticks) | 2K | 560 |
| 4 | Move mouse to events grid | P | 1100 |
| | **TOTAL** | **M=1 P=2 K=2** | **4,110 ms (4.11s)** |

---

### Screen 13: AdminDashboard
**Primary Task:** Navigate to Manage Societies

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about which section to manage | M | 1350 |
| 2 | Move mouse to Manage Societies button | P | 1100 |
| | **TOTAL** | **M=1 P=1** | **2,450 ms (2.45s)** |

---

### Screen 14: ManageUsers
**Primary Task:** Search for a user and delete them

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about searching for user | M | 1350 |
| 2 | Move mouse to Search text field | P | 1100 |
| 3 | Switch to keyboard | H | 400 |
| 4 | Think before typing search term | M | 1350 |
| 5 | Type search term (10 chars) | 10K | 2800 |
| 6 | Switch to mouse | H | 400 |
| 7 | Think before clicking Search | M | 1350 |
| 8 | Move mouse to Search button | P | 1100 |
| 9 | Think about which user to delete | M | 1350 |
| 10 | Move mouse to target user row | P | 1100 |
| 11 | Think before deleting | M | 1350 |
| 12 | Move mouse to Delete button | P | 1100 |
| 13 | Move mouse to Yes in confirmation dialog | P | 1100 |
| | **TOTAL** | **M=5 H=2 P=5 K=10** | **14,900 ms (14.90s)** |

---

### Screen 15: ManageSocieties
**Primary Task:** Approve a pending society

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about pending societies | M | 1350 |
| 2 | Move mouse to pending society row | P | 1100 |
| 3 | Think about approving | M | 1350 |
| 4 | Move mouse to Approve button | P | 1100 |
| | **TOTAL** | **M=2 P=2** | **4,900 ms (4.90s)** |

---

### Screen 16: ApproveEvents
**Primary Task:** Approve a pending event

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think about pending events | M | 1350 |
| 2 | Move mouse to pending event row | P | 1100 |
| 3 | Think about approving | M | 1350 |
| 4 | Move mouse to Approve button | P | 1100 |
| | **TOTAL** | **M=2 P=2** | **4,900 ms (4.90s)** |

---

### Screen 17: AdminReports
**Primary Task:** View university-wide report (auto-loads)

| Step | Action | Operator | Time (ms) |
|------|--------|----------|----------:|
| 1 | Think / scan the report grids | M | 1350 |
| 2 | Move mouse to members grid | P | 1100 |
| 3 | Scroll through members (2 ticks) | 2K | 560 |
| 4 | Move mouse to events grid | P | 1100 |
| | **TOTAL** | **M=1 P=2 K=2** | **4,110 ms (4.11s)** |

---

## KLM Summary Table

| # | Screen | Primary Task | K | M | P | H | Total (ms) | Total (s) | Rank |
|---|--------|-------------|--:|--:|--:|--:|-----------:|----------:|-----:|
| 1 | ManageEvents | Create a new event | 75 | 5 | 7 | 8 | 34,750 | 34.75 | 17 |
| 2 | ManageTasks | Assign a task | 60 | 5 | 7 | 6 | 29,750 | 29.75 | 16 |
| 3 | RegisterForm | Register account | 54 | 6 | 4 | 2 | 27,620 | 27.62 | 15 |
| 4 | LoginForm | Log in | 29 | 4 | 2 | 2 | 16,520 | 16.52 | 14 |
| 5 | ManageUsers | Search + delete user | 10 | 5 | 5 | 2 | 14,900 | 14.90 | 13 |
| 6 | BrowseSocieties | Apply for membership | 0 | 2 | 2 | 0 | 4,900 | 4.90 | 12 |
| 7 | BrowseEvents | Register for event | 0 | 2 | 2 | 0 | 4,900 | 4.90 | 12 |
| 8 | ManageMembers | Approve membership | 0 | 2 | 2 | 0 | 4,900 | 4.90 | 12 |
| 9 | ManageSocieties | Approve society | 0 | 2 | 2 | 0 | 4,900 | 4.90 | 12 |
| 10 | ApproveEvents | Approve event | 0 | 2 | 2 | 0 | 4,900 | 4.90 | 12 |
| 11 | SocietyReports | View report | 2 | 1 | 2 | 0 | 4,110 | 4.11 | 7 |
| 12 | AdminReports | View report | 2 | 1 | 2 | 0 | 4,110 | 4.11 | 7 |
| 13 | MyMemberships | View status | 2 | 1 | 1 | 0 | 3,010 | 3.01 | 6 |
| 14 | StudentDashboard | Navigate | 0 | 1 | 1 | 0 | 2,450 | 2.45 | 5 |
| 15 | SocietyDashboard | Navigate | 0 | 1 | 1 | 0 | 2,450 | 2.45 | 5 |
| 16 | AdminDashboard | Navigate | 0 | 1 | 1 | 0 | 2,450 | 2.45 | 5 |
| 17 | MyTickets | View passes | 0 | 1 | 1 | 0 | 2,450 | 2.45 | 5 |

---

## Usability Analysis

### Fastest Screen (Most Efficient)

- **Screens:** StudentDashboard, SocietyDashboard, AdminDashboard, MyTickets — all at **2,450 ms (2.45 seconds)**
- **Why:** These screens require only a single mental preparation (M) and one pointing action (P). Dashboard screens serve as navigation hubs with clearly labelled buttons, requiring no keyboard interaction. MyTickets auto-loads data, so the user only needs to scan the grid.

### Slowest Screen (Least Efficient)

- **Screen:** ManageEvents — **34,750 ms (34.75 seconds)**
- **Why:** Creating a new event requires the most user effort: opening a dialog, filling 4 text fields (Title, Description, Date, Venue) with 75 total keystrokes, and performing 8 hand switches between keyboard and mouse. The high H count (8) indicates significant friction from alternating between typing and pointing.

### Overall Statistics

| Metric | Value |
|--------|------:|
| Average task completion time | **9,592 ms (9.59s)** |
| Median task completion time | **4,900 ms (4.90s)** |
| Total keyboard time (all screens) | **65,520 ms** (234 keystrokes × 280 ms) |
| Total mental time (all screens) | **52,650 ms** (39 M operators × 1350 ms) |
| Total pointing time (all screens) | **47,300 ms** (43 P operators × 1100 ms) |
| Total hand-switch time (all screens) | **8,800 ms** (22 H operators × 400 ms) |

### Time Breakdown by Operator Type

| Operator | Total Count | Total Time (ms) | % of Total Time |
|----------|----------:|----------------:|----------------:|
| K (Keystrokes) | 234 | 65,520 | 37.6% |
| M (Mental) | 39 | 52,650 | 30.2% |
| P (Pointing) | 43 | 47,300 | 27.2% |
| H (Hand switch) | 22 | 8,800 | 5.1% |
| **Grand Total** | | **174,270** | **100%** |

### Recommendations to Improve Usability

1. **Tab order optimization:** Ensure all form fields have correct Tab order so users can Tab between fields without moving to the mouse. This would reduce H operators significantly on form-heavy screens (ManageEvents, ManageTasks, RegisterForm).
2. **Default values:** Pre-fill the DateTimePicker with today's date (already implemented), and set default role to "Student" in RegisterForm (already done via ComboBox default).
3. **Keyboard shortcuts:** Add Alt+key accelerators (e.g., Alt+L for Login, Alt+R for Register) to eliminate the need to point to buttons after typing.
4. **Auto-focus first field:** Ensure the first text field is focused on form load, eliminating the initial P operator for moving mouse to the first field.
5. **Enter key as submit:** The dialog forms already have `AcceptButton` set, allowing Enter to submit — this is good practice and saves one P operator.

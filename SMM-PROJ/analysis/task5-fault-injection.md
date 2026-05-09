# Task 5 — Fault Injection & Reliability Analysis

> **Project:** Societies Management System (SMM-PROJ)
> **Faults Injected per Module:** 5
> **Confidence Threshold (E):** 1 (at most 1 fault remaining)
> **Formula:** Poisson — `λ = 5 / (CC + 1)`, `P(x ≤ 1) = e^(−λ) × (1 + λ)`
> **CC Source:** WMC values from Task 2 / Task 4 (total test cases per module)

---

## Fault Injection Details (Per Module)

### 1. LoginForm (WMC = 11)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` guard to `< 0` (never triggers) | BtnLogin_Click |
| 2 | Wrong condition | Changed `user == null \|\| !PasswordHasher.Verify(...)` to `user != null \|\| ...` | BtnLogin_Click |
| 3 | Null reference | Removed `if (string.IsNullOrWhiteSpace(email) \|\| ...)` null/empty check | BtnLogin_Click |
| 4 | Wrong logic | Set `Session.Role = "Admin"` always instead of `user.Role` | BtnLogin_Click |
| 5 | Missing validation | Removed password empty-check, allowing blank password login attempt | BtnLogin_Click |

### 2. RegisterForm (WMC = 11)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed password length check minimum from > 0 to >= 0 | BtnRegister_Click |
| 2 | Wrong condition | Changed `password != confirmPassword` to `password == confirmPassword` | BtnRegister_Click |
| 3 | Null reference | Removed `cmbRole.SelectedItem?.ToString()` null-conditional, causing NRE | BtnRegister_Click |
| 4 | Wrong SQL | Passed `email` to PasswordHash parameter instead of hashed value | BtnRegister_Click |
| 5 | Missing validation | Removed `string.IsNullOrWhiteSpace(fullName)` check | BtnRegister_Click |

### 3. StudentDashboard (WMC = 6)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Set welcome label to wrong index of `Session.FullName` | Constructor |
| 2 | Wrong condition | Called `this.Show()` instead of `this.Hide()` in navigation | BtnBrowseSocieties_Click |
| 3 | Null reference | Accessed `Session.FullName` without checking if session is initialized | Constructor |
| 4 | Wrong logic | Opened `AdminDashboard` instead of `BrowseSocieties` | BtnBrowseSocieties_Click |
| 5 | Missing validation | Removed `Session.Clear()` from logout, leaving stale session | BtnLogout_Click |

### 4. BrowseSocieties (WMC = 11)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `SelectedRows.Count <= 0` (off-by-one intent) | BtnApply_Click |
| 2 | Wrong condition | Changed `MembershipDAL.HasApplied(...)` result from `if true` to `if false` | BtnApply_Click |
| 3 | Null reference | Removed null check on `dgvSocieties.SelectedRows[0].Cells["SocietyID"]` | BtnApply_Click |
| 4 | Wrong SQL | Passed `Session.UserID` as `societyId` parameter (swapped) | BtnApply_Click |
| 5 | Missing validation | Removed `SelectedRows.Count == 0` guard entirely | BtnApply_Click |

### 5. MyMemberships (WMC = 4)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Used `Session.UserID - 1` instead of `Session.UserID` | MyMemberships_Load |
| 2 | Wrong condition | Swapped catch block to show success message instead of error | MyMemberships_Load |
| 3 | Null reference | Removed try-catch, letting DB exceptions crash the form | MyMemberships_Load |
| 4 | Wrong logic | Called `MembershipDAL.GetAll()` instead of `GetByUser()` | MyMemberships_Load |
| 5 | Missing validation | Removed `Session.UserID` validation before calling DAL | MyMemberships_Load |

### 6. BrowseEvents (WMC = 11)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `Count < 0` | BtnRegister_Click |
| 2 | Wrong condition | Changed `EventDAL.IsRegistered(...)` to `!EventDAL.IsRegistered(...)` | BtnRegister_Click |
| 3 | Null reference | Removed null check on `dgvEvents.SelectedRows[0].Cells["EventID"]` | BtnRegister_Click |
| 4 | Wrong SQL | Passed `Session.UserID` as eventId (parameter swap) | BtnRegister_Click |
| 5 | Missing validation | Removed no-selection guard, causing IndexOutOfRangeException | BtnRegister_Click |

### 7. MyTickets (WMC = 4)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Used `Session.UserID + 1` instead of `Session.UserID` | MyTickets_Load |
| 2 | Wrong condition | Changed catch to silently swallow exception (no MessageBox) | MyTickets_Load |
| 3 | Null reference | Removed try-catch wrapper | MyTickets_Load |
| 4 | Wrong logic | Assigned DataSource to wrong grid variable | MyTickets_Load |
| 5 | Missing validation | Skipped checking if user is logged in before querying tickets | MyTickets_Load |

### 8. SocietyDashboard (WMC = 12)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Checked `row["Status"] == "Activ"` instead of `"Active"` (missing char) | SocietyDashboard_Load |
| 2 | Wrong condition | Changed `activeRow != null` to `activeRow == null` | SocietyDashboard_Load |
| 3 | Null reference | Removed null check on `activeRow` before accessing `activeRow["SocietyID"]` | SocietyDashboard_Load |
| 4 | Wrong logic | Set `Session.SocietyID` to `UserID` instead of `SocietyID` | SocietyDashboard_Load |
| 5 | Missing validation | Skipped `EnableNavButtons(false)` when no active society found | SocietyDashboard_Load |

### 9. ManageMembers (WMC = 15)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `Count < 0` in approve | BtnApprove_Click |
| 2 | Wrong condition | Called `MembershipDAL.Reject()` instead of `Approve()` | BtnApprove_Click |
| 3 | Null reference | Removed `Session.SocietyID == null` guard in LoadMembers | LoadMembers |
| 4 | Wrong SQL | Used wrong column `"UserID"` instead of `"MembershipID"` for cell value | BtnApprove_Click |
| 5 | Missing validation | Removed filter dropdown null-coalescing, causing NRE | LoadMembers |

### 10. ManageEvents (WMC = 14)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `Count < 0` | BtnCancelEvent_Click |
| 2 | Wrong condition | Changed `DialogResult.Yes` check to `DialogResult.No` | BtnCancelEvent_Click |
| 3 | Null reference | Removed `Session.SocietyID == null` guard in LoadEvents | LoadEvents |
| 4 | Wrong SQL | Passed wrong SocietyID value to `EventDAL.Create()` | BtnCreate_Click |
| 5 | Missing validation | Removed `dialog.ShowDialog() == DialogResult.OK` check | BtnCreate_Click |

### 11. CreateEventDialog (WMC = 3)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Trimmed title but set property to untrimmed `txtTitle.Text` | Constructor lambda |
| 2 | Wrong condition | Changed `\|\|` to `&&` in validation (both must be empty to warn) | Constructor lambda |
| 3 | Null reference | Removed `.Trim()` call causing whitespace-only titles to pass | Constructor lambda |
| 4 | Wrong logic | Set `EventVenue = txtTitle.Text` instead of `txtVenue.Text` | Constructor lambda |
| 5 | Missing validation | Removed empty title/venue check entirely | Constructor lambda |

### 12. ManageTasks (WMC = 14)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `Count < 0` | BtnComplete_Click |
| 2 | Wrong condition | Called `TaskDAL.Create()` instead of `TaskDAL.MarkComplete()` | BtnComplete_Click |
| 3 | Null reference | Removed `Session.SocietyID == null` check in BtnAssign_Click | BtnAssign_Click |
| 4 | Wrong SQL | Used `Session.SocietyID` instead of `Session.UserID` for `assignedBy` | BtnAssign_Click |
| 5 | Missing validation | Removed dialog result check before creating task | BtnAssign_Click |

### 13. AssignTaskDialog (WMC = 3)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Assigned `AssignedToUserId = Convert.ToInt32(cmbMember.SelectedValue) + 1` | Constructor lambda |
| 2 | Wrong condition | Changed `\|\|` to `&&` in member/title validation | Constructor lambda |
| 3 | Null reference | Removed `cmbMember.SelectedValue == null` check | Constructor lambda |
| 4 | Wrong logic | Set `DueDate = DateTime.MinValue` instead of `dtpDueDate.Value` | Constructor lambda |
| 5 | Missing validation | Removed title empty check, allowing blank task names | Constructor lambda |

### 14. SocietyReports (WMC = 9)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Used `members.Rows.Count - 1` for label text | LoadReports |
| 2 | Wrong condition | Changed `dgvMembers.Columns.Contains("UserID")` to `!Contains` | LoadReports |
| 3 | Null reference | Removed `Session.SocietyID == null` guard | LoadReports |
| 4 | Wrong logic | Displayed events DataTable in members grid and vice versa | LoadReports |
| 5 | Missing validation | Removed try-catch, letting exceptions crash the form | LoadReports |

### 15. AdminDashboard (WMC = 8)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Displayed `UserDAL.GetCount() + 1` in stats label | AdminDashboard_Load |
| 2 | Wrong condition | Showed ManageUsers form when ManageSocieties button clicked | BtnManageSocieties_Click |
| 3 | Null reference | Removed try-catch around stats loading | AdminDashboard_Load |
| 4 | Wrong logic | Called `EventDAL.GetPendingCount()` for societies label | AdminDashboard_Load |
| 5 | Missing validation | Skipped `Session.Clear()` on logout | BtnLogout_Click |

### 16. ManageUsers (WMC = 14)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `Count < 0` | BtnDelete_Click |
| 2 | Wrong condition | Changed `DialogResult.Yes` to `DialogResult.No` in confirm | BtnDelete_Click |
| 3 | Null reference | Removed `dgvUsers.SelectedRows[0].Cells["UserID"]` null check | BtnDelete_Click |
| 4 | Wrong SQL | Called `UserDAL.GetAll()` instead of `UserDAL.Search(term)` for search | BtnSearch_Click |
| 5 | Missing validation | Removed no-selection guard in BtnDelete_Click | BtnDelete_Click |

### 17. ManageSocieties (WMC = 19)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `Count < 0` | BtnDelete_Click |
| 2 | Wrong condition | Called `SocietyDAL.Suspend()` instead of `Approve()` | BtnApprove_Click |
| 3 | Null reference | Removed row selection check in BtnSuspend_Click | BtnSuspend_Click |
| 4 | Wrong SQL | Passed wrong ID column `"UserID"` instead of `"SocietyID"` | BtnApprove_Click |
| 5 | Missing validation | Removed confirmation dialog in BtnDelete_Click | BtnDelete_Click |

### 18. CreateSocietyDialog (WMC = 3)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Added +1 to `HeadUserId = Convert.ToInt32(cmbHead.SelectedValue) + 1` | Constructor lambda |
| 2 | Wrong condition | Changed `\|\|` to `&&` in name/head validation | Constructor lambda |
| 3 | Null reference | Removed `cmbHead.SelectedValue == null` check | Constructor lambda |
| 4 | Wrong logic | Set `SocietyDescription = txtName.Text` instead of `txtDescription.Text` | Constructor lambda |
| 5 | Missing validation | Removed name empty check | Constructor lambda |

### 19. ApproveEvents (WMC = 12)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `SelectedRows.Count == 0` to `Count < 0` | BtnApprove_Click |
| 2 | Wrong condition | Called `EventDAL.Reject()` instead of `Approve()` | BtnApprove_Click |
| 3 | Null reference | Removed `dgvEvents.SelectedRows[0].Cells["EventID"]` value check | BtnApprove_Click |
| 4 | Wrong SQL | Used wrong cell column name `"Title"` instead of `"EventID"` | BtnReject_Click |
| 5 | Missing validation | Removed no-selection guard in BtnReject_Click | BtnReject_Click |

### 20. AdminReports (WMC = 6)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Loaded performance summary into members grid (off-target) | LoadReports |
| 2 | Wrong condition | Changed catch block to show success instead of error | LoadReports |
| 3 | Null reference | Removed try-catch, letting DB exceptions crash the form | LoadReports |
| 4 | Wrong logic | Called `MembershipDAL.GetAll()` twice instead of `EventDAL.GetAll()` | LoadReports |
| 5 | Missing validation | Skipped null check on DataTable before binding | LoadReports |

### 21. EnvConfig (WMC = 6)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `EndsWith(';')` ternary to append wrong separator | EnsureDatabaseInConnectionString |
| 2 | Wrong condition | Changed `!string.IsNullOrEmpty(fullFromEnv)` to `string.IsNullOrEmpty(...)` | Load |
| 3 | Null reference | Removed `.Trim()` on CONNECTION_STRING, allowing whitespace-only values | Load |
| 4 | Wrong logic | Set `ConnectionString` to empty string after building it | Load |
| 5 | Missing validation | Removed `hasCatalog` check, always appending duplicate catalog | EnsureDatabaseInConnectionString |

### 22. DBHelper (WMC = 4)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Called `connection.Open()` twice (redundant call) | GetConnection |
| 2 | Wrong condition | Used `SqlCommand(query, null)` instead of passing connection | ExecuteNonQuery |
| 3 | Null reference | Removed `using` on connection, causing resource leak | ExecuteReader |
| 4 | Wrong SQL | Forgot to call `command.Parameters.AddRange()` (no params) | ExecuteNonQuery |
| 5 | Missing validation | Removed null check on ConnectionString before creating SqlConnection | GetConnection |

### 23. UserDAL (WMC = 10)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `table.Rows.Count == 0` to `Count < 0` in GetByEmail | GetByEmail |
| 2 | Wrong condition | Changed `Convert.ToInt32(result) > 0` to `< 0` in EmailExists | EmailExists |
| 3 | Null reference | Removed `table.Rows.Count == 0` null check in GetByEmail | GetByEmail |
| 4 | Wrong SQL | Used `SELECT *` from wrong table name in GetAll query | GetAll |
| 5 | Missing validation | Removed parameter for `@Email` in Register method | Register |

### 24. SocietyDAL (WMC = 9)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `rows > 0` to `rows >= 0` in Create (always returns true) | Create |
| 2 | Wrong condition | Set status to `'Suspended'` instead of `'Active'` in Approve | Approve |
| 3 | Null reference | Removed SqlParameter for `@HeadUserID` in Create | Create |
| 4 | Wrong SQL | Changed `WHERE Status = 'Active'` to `Status = 'Pending'` in GetActive | GetActive |
| 5 | Missing validation | Removed name parameter from Create INSERT query | Create |

### 25. MembershipDAL (WMC = 9)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `Convert.ToInt32(result) > 0` to `>= 0` in HasApplied | HasApplied |
| 2 | Wrong condition | Set status to `'Rejected'` instead of `'Approved'` in Approve | Approve |
| 3 | Null reference | Removed SqlParameter for `@SocietyID` in GetBySociety | GetBySociety |
| 4 | Wrong SQL | Used wrong JOIN column in GetApprovedMembers | GetApprovedMembers |
| 5 | Missing validation | Removed `@Status` parameter in GetBySocietyFiltered | GetBySocietyFiltered |

### 26. EventDAL (WMC = 12)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed ticket code substring from `[..8]` to `[..7]` (one char short) | Register |
| 2 | Wrong condition | Changed `Convert.ToInt32(result) > 0` to `< 0` in IsRegistered | IsRegistered |
| 3 | Null reference | Removed `@EventID` SqlParameter in Approve | Approve |
| 4 | Wrong SQL | Changed `Status = 'Approved'` to `Status = 'Pending'` in Approve | Approve |
| 5 | Missing validation | Removed `@UserID` parameter from Register query | Register |

### 27. TaskDAL (WMC = 3)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `rows > 0` to `rows >= 0` in Create | Create |
| 2 | Wrong condition | Set status to `'Pending'` instead of `'Completed'` in MarkComplete | MarkComplete |
| 3 | Null reference | Removed SqlParameter for `@SID` in GetBySociety | GetBySociety |
| 4 | Wrong SQL | Used wrong column `AssignedBy` instead of `AssignedTo` in JOIN | GetBySociety |
| 5 | Missing validation | Omitted `@DueDate` parameter in Create | Create |

### 28. AnnouncementDAL (WMC = 2)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Changed `rows > 0` to `rows >= 0` in Create | Create |
| 2 | Wrong condition | Changed `@SID` parameter value to hardcoded `0` | GetBySociety |
| 3 | Null reference | Passed null instead of `@Content` SqlParameter | Create |
| 4 | Wrong SQL | Changed SELECT to `WHERE SocietyID != @SID` (wrong operator) | GetBySociety |
| 5 | Missing validation | Removed `@Title` parameter from Create INSERT | Create |

### 29. Session (WMC = 1)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Set `UserID = -1` instead of `0` in Clear | Clear |
| 2 | Wrong condition | Left `Role` unchanged instead of clearing it | Clear |
| 3 | Null reference | Set `FullName = null` instead of `string.Empty` | Clear |
| 4 | Wrong logic | Only cleared `UserID`, left all other properties untouched | Clear |
| 5 | Missing validation | Did not set `SocietyID = null` in Clear | Clear |

### 30. PasswordHasher (WMC = 2)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Added extra salt rounds parameter `workFactor: 99` | Hash |
| 2 | Wrong condition | Returned `!BCrypt.Verify(...)` (negated result) | Verify |
| 3 | Null reference | Removed implicit null check, passing null to BCrypt | Hash |
| 4 | Wrong logic | Returned the plain password instead of hash in Hash() | Hash |
| 5 | Missing validation | Removed empty password check before hashing | Hash |

### 31. Program (WMC = 1)

| Fault # | Fault Type | Description | Location |
|---------|-----------|-------------|----------|
| 1 | Off-by-one | Called `EnvConfig.Load()` after `Application.Run()` (wrong order) | Main |
| 2 | Wrong condition | Launched `RegisterForm` instead of `LoginForm` | Main |
| 3 | Null reference | Removed `EnvConfig.Load()` call entirely | Main |
| 4 | Wrong logic | Called `ApplicationConfiguration.Initialize()` before `EnvConfig.Load()` (no config) | Main |
| 5 | Missing validation | No error handling if `.env` file is missing | Main |

---

## Reliability Summary Table

| # | Module/Feature | Faults Injected | CC (WMC) | λ = 5/(CC+1) | P(x ≤ 1) | Reliability % | Rank |
|---|---------------|:-:|:-:|:-:|:-:|:-:|:-:|
| 1 | ManageSocieties | 5 | 19 | 0.2500 | 0.9735 | **97.35%** | 1 |
| 2 | ManageMembers | 5 | 15 | 0.3125 | 0.9603 | 96.03% | 2 |
| 3 | ManageEvents | 5 | 14 | 0.3333 | 0.9553 | 95.53% | 3 |
| 4 | ManageTasks | 5 | 14 | 0.3333 | 0.9553 | 95.53% | 3 |
| 5 | ManageUsers | 5 | 14 | 0.3333 | 0.9553 | 95.53% | 3 |
| 6 | EventDAL | 5 | 12 | 0.3846 | 0.9430 | 94.30% | 6 |
| 7 | SocietyDashboard | 5 | 12 | 0.3846 | 0.9430 | 94.30% | 6 |
| 8 | ApproveEvents | 5 | 12 | 0.3846 | 0.9430 | 94.30% | 6 |
| 9 | LoginForm | 5 | 11 | 0.4167 | 0.9338 | 93.38% | 9 |
| 10 | RegisterForm | 5 | 11 | 0.4167 | 0.9338 | 93.38% | 9 |
| 11 | BrowseSocieties | 5 | 11 | 0.4167 | 0.9338 | 93.38% | 9 |
| 12 | BrowseEvents | 5 | 11 | 0.4167 | 0.9338 | 93.38% | 9 |
| 13 | UserDAL | 5 | 10 | 0.4545 | 0.9228 | 92.28% | 13 |
| 14 | SocietyDAL | 5 | 9 | 0.5000 | 0.9098 | 90.98% | 14 |
| 15 | MembershipDAL | 5 | 9 | 0.5000 | 0.9098 | 90.98% | 14 |
| 16 | SocietyReports | 5 | 9 | 0.5000 | 0.9098 | 90.98% | 14 |
| 17 | AdminDashboard | 5 | 8 | 0.5556 | 0.8926 | 89.26% | 17 |
| 18 | EnvConfig | 5 | 6 | 0.7143 | 0.8394 | 83.94% | 18 |
| 19 | StudentDashboard | 5 | 6 | 0.7143 | 0.8394 | 83.94% | 18 |
| 20 | AdminReports | 5 | 6 | 0.7143 | 0.8394 | 83.94% | 18 |
| 21 | DBHelper | 5 | 4 | 1.0000 | 0.7358 | 73.58% | 21 |
| 22 | MyMemberships | 5 | 4 | 1.0000 | 0.7358 | 73.58% | 21 |
| 23 | MyTickets | 5 | 4 | 1.0000 | 0.7358 | 73.58% | 21 |
| 24 | TaskDAL | 5 | 3 | 1.2500 | 0.6446 | 64.46% | 24 |
| 25 | CreateEventDialog | 5 | 3 | 1.2500 | 0.6446 | 64.46% | 24 |
| 26 | AssignTaskDialog | 5 | 3 | 1.2500 | 0.6446 | 64.46% | 24 |
| 27 | CreateSocietyDialog | 5 | 3 | 1.2500 | 0.6446 | 64.46% | 24 |
| 28 | AnnouncementDAL | 5 | 2 | 1.6667 | 0.5037 | 50.37% | 28 |
| 29 | PasswordHasher | 5 | 2 | 1.6667 | 0.5037 | 50.37% | 28 |
| 30 | Session | 5 | 1 | 2.5000 | 0.2873 | 28.73% | 30 |
| 31 | Program | 5 | 1 | 2.5000 | 0.2873 | **28.73%** | 30 |

---

## Sample Calculation (Worked Example)

**Module:** LoginForm
- Faults Injected = 5
- CC (WMC from Task 2) = 11 (total test cases across all methods)
- λ = 5 / (11 + 1) = 5 / 12 = **0.4167**
- P(x = 0) = e^(−0.4167) = 0.6592
- P(x = 1) = 0.4167 × e^(−0.4167) = 0.4167 × 0.6592 = 0.2746
- P(x ≤ 1) = 0.6592 + 0.2746 = **0.9338** = **93.38%**

---

## Conclusions

### Most Reliable Function/Feature

- **Module:** ManageSocieties
- **Reliability:** 97.35%
- **Reason:** ManageSocieties has the highest WMC in the project (19), meaning 19 test cases were generated from its cyclomatic complexity analysis. With more paths tested, λ = 5/20 = 0.25 is the lowest of all modules, giving the highest probability (97.35%) that no more than 1 fault remains undetected after testing. The extensive CRUD operations (Create, Approve, Suspend, Delete) in this class generate many independent paths, providing thorough test coverage.

### Least Reliable Function/Feature

- **Module:** Session / Program (tied)
- **Reliability:** 28.73%
- **Reason:** Both Session and Program have the lowest WMC (1), meaning only 1 test case was generated from their cyclomatic complexity. With λ = 5/2 = 2.5, the Poisson model predicts only a 28.73% probability of having ≤ 1 fault remaining after testing. These modules have minimal branching logic, so the limited test cases cannot exercise enough paths to provide confidence in fault detection. To improve reliability, additional test cases beyond those generated by CC should be added (e.g., boundary testing, integration testing).

### Overall Assessment

- **Average Reliability:** 80.7% across all 31 modules
- **Modules above 90% reliability:** 16 out of 31 (51.6%)
- **Modules below 70% reliability:** 7 out of 31 (22.6%)
- The Poisson model shows that modules with higher complexity (more test paths) achieve higher reliability scores because the test-to-fault ratio is more favourable. Simple utility classes with few methods would benefit from additional targeted testing.

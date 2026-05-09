# SE-4011 – Software Measurement and Metrices
## Auditing Society Management System
**Deadline – May 10, 2026**

**Group Project:**
- Group Size: 3 Students
- All members must explain their task in viva.
- Live demo compulsory.
- Only AI-generated code submission without any analysis = zero marks.

---

## Instructions

1. Make sure that you read and understand each instruction. If you have any questions or comments you are encouraged to discuss your problems with your colleagues (and instructors) on google classroom.
2. Keep a backup of your work always that will be helpful in preventing any mishap and avoid last hour submissions
3. Work division is provided at the end of the document. You will be evaluated for your own work.
4. AI / Generative tools are only allowed for coding task
5. Late submissions will be marked zero.

---

## Submission Items

1. ZIP Project (C# Desktop Application and SQL Server as Database)
2. A docx document with complete task and analysis
3. Submission to be done by only 1 group member.

**Note:** Start early so that you can finish it on time.

---

## Project Scenario: FAST Societies Management System

Our university campus has multiple student societies such as Gaming Society, Sports Society, Developers Club, Literary Society, and Media Society. These societies organize events, workshops, competitions, recruitment drives, and awareness campaigns throughout the semester.

The absence of a centralized digital platform for managing student societies creates communication gaps, inefficient event handling, poor record management, and lack of administrative oversight. Manual systems waste time, increase errors, and reduce student engagement. A complete Societies Management System is required to automate society registrations, event management, announcements, task coordination, approvals, and reporting for students, society members, and administrators.

Currently, society operations are handled manually through WhatsApp groups, spreadsheets, paper registrations, and verbal communication. Students often miss announcements, event registrations become disorganized, duplicate memberships occur, and attendance records are inaccurate. Society leaders also face difficulty coordinating members, assigning tasks, collecting feedback, and maintaining records.

The university administration lacks visibility into society performance, active memberships, event approvals, and resource allocation. There is no centralized system to monitor or regulate society activities.

To solve these issues, the university wants to develop, a desktop-based Societies Management System (using C# window form and SQL Server) that digitally connects students, society representatives, and university administration on one platform. The functional requirement for the system are as follows

---

## Functional Requirements

### 1. Student Functional Requirements

1. Students shall be able to create accounts and log in securely.
2. Students shall be able to browse available societies.
3. Students shall be able to apply for membership in societies.
4. Students shall be able to join multiple societies.
5. Students shall be able to view upcoming society events.
6. Students shall be able to register for events online.
7. Students shall be able to view their membership status.
8. Students shall be able to view event tickets/passes.

### 2. Society Functional Requirements

1. Society heads shall be able to create and manage society profiles.
2. Society heads shall be able to approve or reject membership requests.
3. Society members shall be able to manage internal member lists.
4. Societies shall be able to create, update, and cancel events.
5. Societies shall be able to assign tasks to members.
6. Societies shall be able to generate reports of members and events.

### 3. Admin Functional Requirements

1. Admin shall be able to manage all student accounts.
2. Admin shall be able to create, approve, suspend, or delete societies.
3. Admin shall be able to monitor all society activities.
4. Admin shall be able to approve event requests.
5. Admin shall be able to generate university-wide reports.

---

## Task 1:

Generate a complete .NET desktop application using AI-assisted coding tools. Also, generate complete database schema and add the ERD to the report.

**Deliverable:** Code + Schema

---

## Task 2:

Perform Cyclomatic Complexity for all functions and write test cases for each of the function.

**Deliverable:** Table with name of function, cyclomatic complexity and test case inputs

---

## Task 3:

Justify which of the above generate module is the best one. Select a metric of your choice and defend your answer in the report (structural Metric).

**Deliverable:** Comparative table with list of all features and the value of the metrics for each of the feature.

---

## Task 4:

Apply CK metrics on the complete generated project.

**CK Metrics:**
- WMC – Weighted Methods per Class
- DIT – Depth of Inheritance Tree
- NOC – Number of Children
- CBO – Coupling Between Objects
- RFC – Response for Class
- LCOM – Lack of Cohesion in Methods

**Deliverable:** A complete analysis report which also answers the following questions

- Maximum Depth of inheritance
- Highest and Lowest WMC and its explanation
- Class with the greatest number of children
- Most complex class
- Most coupled class
- Least cohesive class

---

## Task 5:

Inject 5 faults into each function/module of the generated application. Assume the confidence threshold (E) for all functions is 1.

**Deliverable.** A table with list of all features with number of faults identified and probability (no more than E fault). Use test cases generate after cyclomatic complexity

After the table, identify:
- Most Reliable Function/Feature (highest probability of having no more than 1 fault)
- Least Reliable Function/Feature (lowest probability of having no more than 1 fault)

---

## Task 6:

KLM Usability Evaluation on all the UI of the applications

Use the following KLM operators:
- K = Keystroke = 280 ms
- M = Mental preparation = 1350 ms
- P = Pointing = 1100 ms
- H = Hand movement keyboard ↔ mouse = 400 ms

---

## Task 7:

Apply suitable COCOMO Model. Provide justification for your selection.

---

## Task 8:

Find the documentation ration of the code generated by vibe coding

**Documentation Ratio = Total LOC / Commented Lines**

---

## Work Division

| Student 1 | Student 2 | Student 3 |
|-----------|-----------|-----------|
| **Shared Task** | | |
| Generate the .NET application using vibe coding. + Database Generation | | |
| Cyclomatic Complexity | Evaluation of best feature on structural metrics (other than CK Suite) | Apply suitable COCOMO model based on number of lines generated |
| Test Cases | CK suite evaluation for the whole project | Apply KLM on UI |
| Fault Injection | Refactoring suggestions for all modules with issues | Find the documentation ration of the code generated by vibe coding Documentation Ratio = Total LOC / Commented Lines |
---
name: PDF Report Generator
overview: Create a Python script using reportlab that generates a comprehensive PDF report for the Societies Management System project, containing all 8 task analyses, cover page with NUCES logo, table of contents, architecture diagram, and database schema.
todos:
  - id: setup
    content: Install dependencies (reportlab, Pillow) and set up the script file with constants, styles, and helpers
    status: pending
  - id: cover-toc
    content: Build cover page (logo, course info, team) and Table of Contents using reportlab multiBuild
    status: pending
  - id: task1-arch
    content: "Build Task 1 section: project overview, functional requirements, architecture diagram, database schema"
    status: pending
  - id: task2
    content: "Build Task 2 section: all 150 CC method rows grouped by class, summary stats, refactoring table"
    status: pending
  - id: task3
    content: "Build Task 3 section: metric justification, comparative table (37 rows), best/worst module analysis"
    status: pending
  - id: task4
    content: "Build Task 4 section: CK metrics table (37 rows), thresholds, 6 analysis questions"
    status: pending
  - id: task5
    content: "Build Task 5 section: 31 fault tables (5 faults each), reliability summary, most/least reliable"
    status: pending
  - id: task6
    content: "Build Task 6 section: 17 KLM screen tables, summary table, usability analysis"
    status: pending
  - id: task7
    content: "Build Task 7 section: LOC table (51 files), COCOMO calculations, interpretation"
    status: pending
  - id: task8
    content: "Build Task 8 section: documentation ratio table (51 files), classification, vibe-coding analysis"
    status: pending
  - id: test-run
    content: Run script, verify PDF output, fix any layout/pagination issues
    status: pending
isProject: false
---

# PDF Report Generator for SMM Project

## Approach

A single Python script (`generate_report.py`) using **reportlab** will produce the full PDF. The script will hardcode all analysis data (extracted from the 7 markdown files) rather than parsing markdown at runtime -- this avoids fragile parsing and ensures every table/paragraph is pixel-perfect.

## Dependencies

- `reportlab` -- PDF generation (tables, styles, TOC, images, page templates)
- `Pillow` -- image handling (used by reportlab for PNG embedding)

Install: `pip install reportlab Pillow`

## Assets (already available)

- **NUCES logo**: `assets/c__Users_LENOVO_..._image-9ad1d1ba-....png` (the round NUCES seal)
- **Architecture diagram**: `assets/c__Users_LENOVO_..._image-5f9605ad-....png` (the dark dependency flowchart)
- **Database schema**: [SMM-PROJ/Database/schema.sql](SMM-PROJ/Database/schema.sql)

## PDF Structure (matches [project_statement.md](.cursor/project_statement.md))

### Cover Page
- NUCES logo (centered, ~150px)
- Course: **SE-4011 -- Software Measurement and Metrics**
- Title: **FAST Societies Management System**
- Submitted to: **Dr. Atif Jillani**
- Section: **SE-D**
- Team members: Hammad Zahid 22I-2433, Abdullah Asif 22I-1527, Dawood Qammar 22I-2522

### Table of Contents
- Auto-generated using reportlab `TableOfContents` flowable
- Entries link to each Task heading and sub-heading

### Section 1: Project Overview
- Scenario paragraph (from project_statement.md)
- Functional Requirements (Student, Society, Admin) as bullet lists

### Section 2: Architecture
- Architecture diagram image (full-width)
- Brief description of the layered architecture (Config -> DAL -> Models -> Forms)

### Section 3: Task 1 -- Application & Database Schema
- Schema SQL listing formatted as a code block (from [schema.sql](SMM-PROJ/Database/schema.sql))
- 7-table summary: Users, Societies, Memberships, Events, EventRegistrations, Tasks, Announcements with column descriptions

### Section 4: Task 2 -- Cyclomatic Complexity
- Source: [task2-cyclomatic-complexity.md](SMM-PROJ/analysis/task2-cyclomatic-complexity.md)
- One table per class group (31 groups, 150 methods total)
- Each row: #, Method, Decision Points, CC, Test Cases
- Summary statistics table at the end
- CC Distribution by Module table
- Methods needing refactoring table

### Section 5: Task 3 -- Structural Metric (Best Module)
- Source: [task3-structural-metric.md](SMM-PROJ/analysis/task3-structural-metric.md)
- Metric selection justification paragraph
- Full comparative table (37 rows): Class, Module, LOC, Fan-Out, Comment Lines, Total Lines, Comment Ratio
- Module-level summary table
- Best/Worst class analysis with reasoning

### Section 6: Task 4 -- CK Metrics
- Source: [task4-ck-metrics.md](SMM-PROJ/analysis/task4-ck-metrics.md)
- Complete CK table (37 rows): Class, Module, WMC, DIT, NOC, CBO, RFC, LCOM
- Interpretation thresholds table
- 6 analysis questions with answers

### Section 7: Task 5 -- Fault Injection & Reliability
- Source: [task5-fault-injection.md](SMM-PROJ/analysis/task5-fault-injection.md)
- Per-module fault tables (31 modules x 5 faults each = 155 rows across 31 sub-tables)
- Summary reliability table ranked by P(x<=1)
- Worked example calculation
- Most/Least Reliable conclusions

### Section 8: Task 6 -- KLM Usability
- Source: [task6-klm-evaluation.md](SMM-PROJ/analysis/task6-klm-evaluation.md)
- 17 per-screen step-by-step operator tables
- Summary table with K, M, P, H counts and totals
- Fastest/slowest screen identification
- Overall statistics and time breakdown

### Section 9: Task 7 -- COCOMO Model
- Source: [task7-cocomo.md](SMM-PROJ/analysis/task7-cocomo.md)
- Per-file LOC breakdown table (51 files)
- LOC summary
- Model + Mode justification
- COCOMO calculation table
- Detailed step-by-step calculation
- Interpretation with estimated vs actual comparison

### Section 10: Task 8 -- Documentation Ratio
- Source: [task8-documentation-ratio.md](SMM-PROJ/analysis/task8-documentation-ratio.md)
- Per-file ratio table (51 files) with classification
- Project-wide ratio
- Classification distribution
- Best/worst files
- Vibe-coding analysis paragraphs

## Script Architecture

```
generate_report.py (~1500-2000 lines)
  |
  +-- CONSTANTS: colors (NUCES_BLUE=#0072BC), paths, margins
  +-- STYLES: heading1-3, body, code, table header/cell
  +-- HELPERS:
  |     make_table(data, col_widths) -> Table with style
  |     heading(text, level) -> Paragraph with TOC bookmark
  |     para(text) -> Paragraph with body style
  |     code_block(text) -> Preformatted code
  |     bullet_list(items) -> ListFlowable
  |
  +-- CONTENT BUILDERS:
  |     build_cover(story) -> adds cover page elements
  |     build_task1(story) -> project overview + schema
  |     build_task2(story) -> cyclomatic complexity tables
  |     build_task3(story) -> structural metrics
  |     build_task4(story) -> CK metrics
  |     build_task5(story) -> fault injection
  |     build_task6(story) -> KLM tables
  |     build_task7(story) -> COCOMO
  |     build_task8(story) -> documentation ratio
  |
  +-- PAGE TEMPLATES:
  |     on_first_page(canvas, doc) -> cover page decoration
  |     on_later_pages(canvas, doc) -> header + page number footer
  |
  +-- MAIN:
        story = []
        build_cover(story)
        story.append(TableOfContents)
        story.append(PageBreak)
        build_task1..8(story)
        doc.multiBuild(story)  # multiBuild required for TOC
```

Key reportlab features used:
- `SimpleDocTemplate` with `multiBuild` (for TOC page number resolution)
- `TableOfContents` flowable (auto-generated, clickable)
- `Table` with `repeatRows=1` (headers repeat on page breaks)
- `Image` for logo and architecture diagram
- `Paragraph` with `<b>`, `<i>`, `<font>` markup for inline formatting
- `Preformatted` for SQL code blocks
- Custom `PageTemplate` for headers/footers with page numbers

## Table Formatting Strategy

- Header rows: NUCES blue background (#0072BC), white bold text
- Alternating row colors: white / light gray (#F2F2F2)
- Font size: 7-8pt for large tables (Task 2, 5), 9pt for smaller ones
- Column widths: calculated proportionally to content
- `repeatRows=1` so headers show on every page
- Long text cells use `Paragraph` objects for word-wrapping (especially test case descriptions in Task 2)

## Output

- File: `SMM-PROJ/SMM_Report.pdf`
- Estimated: ~60-80 pages (Task 2 alone is ~15 pages, Task 5 is ~20 pages)

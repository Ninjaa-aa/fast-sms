---
name: Tasks 5-8 Metrics Analysis
overview: "Produce four analysis markdown files: Task 5 (Poisson fault injection reliability), Task 6 (KLM usability for 17 screens), Task 7 (Basic COCOMO Semi-detached), and Task 8 (Documentation Ratio per file and overall) -- all computed from the actual SMM-PROJ codebase."
todos: []
isProject: false
---

# Tasks 5, 6, 7 & 8 -- Software Metrics Analysis Plan

## Input Data (from Tasks 2-4)

The following data from previous tasks feeds into Tasks 5-8:

- **WMC per class** (from [task4-ck-metrics.md](SMM-PROJ/analysis/task4-ck-metrics.md)): Used as the CC value per module for Task 5
- **LOC and Comment Lines per class** (from [task3-structural-metric.md](SMM-PROJ/analysis/task3-structural-metric.md)): Reused for Task 8
- **51 total .cs files** (17 code-behind + 17 Designer + 7 DAL + 6 models + 2 helpers + 1 config + 1 program): All counted for Tasks 7 & 8

---

## Task 5 -- Fault Injection + Reliability (Poisson)

### Approach

For each of the **20 functional modules** (all classes with methods, excluding 6 model classes with WMC=0):

1. **Inject 5 specific faults** (one per fault type): off-by-one, wrong condition, null reference, wrong SQL/logic, missing validation
2. **Use WMC as CC** (total test cases for the module from Task 2)
3. **Calculate lambda**: `lambda = 5 / (WMC + 1)`
4. **Calculate reliability**: `P(x <= 1) = e^(-lambda) * (1 + lambda)`

### Key data points (
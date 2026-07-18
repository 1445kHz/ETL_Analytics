# Developer Guide: Coding with Business Rules

This guide explains how to develop, test, and sequence logic using the ETL Analytics Business Rules Engine.

---

## 1. Creating Your First Rule

The engine supports two languages. Choose based on your task:
- **T-SQL**: Best for bulk data analysis, audits against the database, and high-performance filtering.
- **C# Script**: Best for complex string manipulation, cross-referencing multiple data sources, and conditional branching.

### The IDE Experience
1. Navigate to **Business Rules** in the sidebar.
2. Click the **+** (Add) button.
3. Name your rule and select the type (TSQL or CSharp).
4. Write your code in the dark-themed editor (Consolas font).
5. Use the **Run Execution** button to see real-time logs and the resulting JSON object.

---

## 2. Coding in C# (Roslyn)

C# rules are scripts that have direct access to the application context.

### Global Variables
You don't need to import or initialize these; they are provided automatically:

| Variable | Description |
| :--- | :--- |
| `Jobs` | `List<JilJob>` - Every job from the latest AutoSys import. |
| `JobToPackageMappings` | `List<AutoSysJobToPackage>` - All job-to-SSIS mappings. |
| `PreviousResult` | The output from the rule immediately before this one in a bundle. |
| `StepResults[n]` | Dictionary of results from any previous step (keyed by SequenceOrder). |
| `Log("msg")` | Writes a message to the UI Watch Window. |

### Example Pattern: The "Filter"
```csharp
Log("Starting validation...");

// Filter jobs missing a standard prefix
var invalid = Jobs.Where(j => !j.JobName.StartsWith("ETL_")).ToList();

return new {
    Count = invalid.Count,
    Names = invalid.Select(i => i.JobName)
};
```

---

## 3. Coding in T-SQL

T-SQL rules run directly against the analytics database.

### Parameter Support
The engine automatically injects these parameters into your SQL scripts:
- `@PreviousResultJson`: The result of the previous step serialized as JSON.
- `@StepResultsJson`: All previous steps serialized as JSON.

### Example Pattern: The "Audit"
```sql
-- Find jobs that exist in AutoSys but haven't been mapped to a package
SELECT j.JobName, j.Application
FROM dbo.AutoSysJilJobs j
LEFT JOIN dbo.AutoSysJobToPackage m ON j.JobName = m.JobName
WHERE m.PackageName IS NULL;
```

### Example Pattern: Consuming Data from Step 1
```sql
-- Filter data from Step 1 using OPENJSON
SELECT 
    JSON_VALUE(value, '$.JobName') as JobName
FROM OPENJSON(@PreviousResultJson)
WHERE JSON_VALUE(value, '$.Priority') = 'High';
```

---

## 4. Building Rule Bundles

Bundles allow you to sequence rules into a single workflow.

### Data Piping
The output of Step 1 becomes the `PreviousResult` (C#) or `@PreviousResultJson` (SQL) of Step 2.

**Best Practice**: Always return an object or a list from your rules.
- **SQL**: Returns a list of rows.
- **C#**: Returns whatever you put after the `return` keyword.

---

## 5. Advanced: Conditional Branching

You can create "Smart Bundles" that decide what to do next based on data.

### Using `RunBundle`
In a C# rule, you can trigger another bundle entirely:

```csharp
var report = PreviousResult as dynamic;

if (report.CriticalErrors > 0) {
    Log("Critical errors found! Triggering Remediation Bundle.");
    await RunBundle("System Auto-Fix");
}

return "Audit Complete";
```

### Complex Routing with StepResults
If you are at Step 5 and need to check if Step 1 was successful:

```csharp
var step1 = StepResults[1] as dynamic;
if (step1?.Status == "Success") {
    // Proceed with logic...
}
```

---

## 6. Testing Tips
- **Watch Window**: Use `Log()` frequently in C# and `PRINT` in SQL to debug your logic.
- **JSON Preview**: The right-hand panel in the IDE shows exactly what your rule returns. Use this to ensure your data structure is compatible with the next step.
- **Standalone Mode**: You can run any rule by itself. If it uses `@PreviousResultJson`, the engine will pass an empty array `[]` so the script doesn't crash.

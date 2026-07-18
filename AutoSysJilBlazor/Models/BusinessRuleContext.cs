using System;
using System.Collections.Generic;

namespace AutoSysJilBlazor.Models;

/// <summary>
/// Provides a global context for Business Rules executed within the application.
/// Properties defined here are directly accessible in C# scripts.
/// </summary>
public class BusinessRuleContext
{
    /// <summary>
    /// The list of AutoSys jobs from the latest import.
    /// </summary>
    public List<JilJob> Jobs { get; set; } = new();

    /// <summary>
    /// Job to Package mappings.
    /// </summary>
    public List<AutoSysJobToPackage> JobToPackageMappings { get; set; } = new();

    /// <summary>
    /// The time the rule execution started.
    /// </summary>
    public DateTime ExecutionTime { get; set; } = DateTime.Now;

    /// <summary>
    /// The result from the previous rule in a bundle sequence.
    /// </summary>
    public object? PreviousResult { get; set; }

    /// <summary>
    /// A collection of all results from earlier steps in the current bundle execution, keyed by SequenceOrder.
    /// </summary>
    public Dictionary<int, object?> StepResults { get; set; } = new();

    /// <summary>
    /// Helper to log messages from within the script.
    /// </summary>
    public Action<string>? Log { get; set; }

    /// <summary>
    /// Allows a script to trigger the execution of another bundle by its name.
    /// Returns the final result of the triggered bundle.
    /// </summary>
    public Func<string, Task<object?>>? RunBundle { get; set; }
}

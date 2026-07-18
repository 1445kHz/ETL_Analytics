using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Dapper;
using Microsoft.Data.SqlClient;
using AutoSysJilBlazor.Models;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace AutoSysJilBlazor.Services;

public class BusinessRuleEngine
{
    private readonly string _connectionString;
    private readonly SqlDatabaseService _dbService;

    public BusinessRuleEngine(IConfiguration configuration, SqlDatabaseService dbService)
    {
        _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? throw new InvalidOperationException("Environment variable 'DB_CONNECTION_STRING' is not set.");
        _dbService = dbService;
    }

    public async Task<object?> ExecuteRuleAsync(
        BusinessRule rule,
        BusinessRuleContext? globals = null,
        Action<string>? appendLog = null)
    {
        if (globals != null)
        {
            globals.RunBundle = async (name) =>
            {
                var bundle = await _dbService.GetBusinessRuleBundleByNameAsync(name);
                if (bundle == null)
                {
                    appendLog?.Invoke($"[WARN] RunBundle: Bundle '{name}' not found.");
                    return null;
                }
                appendLog?.Invoke($"[INFO] Triggering nested bundle: {name}");
                return await ExecuteBundleAsync(bundle, globals, appendLog);
            };
        }

        appendLog?.Invoke($"[INFO] Starting execution of rule: {rule.Name} ({rule.RuleType})");

        try
        {
            if (rule.RuleType == RuleType.TSQL)
            {
                return await ExecuteTsqlAsync(rule.Code, globals, appendLog);
            }
            else if (rule.RuleType == RuleType.CSharp)
            {
                return await ExecuteCSharpAsync(rule.Code, globals, appendLog);
            }
            else
            {
                throw new NotSupportedException($"Rule type {rule.RuleType} is not supported.");
            }
        }
        catch (Exception ex)
        {
            appendLog?.Invoke($"[ERR] Execution failed: {ex.Message}");
            throw;
        }
    }

    public async Task<object?> ExecuteBundleAsync(
        BusinessRuleBundle bundle,
        BusinessRuleContext baseContext,
        Action<string>? appendLog = null)
    {
        appendLog?.Invoke($"[BUNDLE] --- Starting Bundle: {bundle.Name} ---");
        object? lastResult = null;

        foreach (var item in bundle.Items.OrderBy(i => i.SequenceOrder))
        {
            var rule = await _dbService.GetBusinessRuleByIdAsync(item.RuleId);
            if (rule == null)
            {
                appendLog?.Invoke($"[ERR] Rule ID {item.RuleId} not found. Skipping.");
                continue;
            }

            appendLog?.Invoke($"[BUNDLE] Step {item.SequenceOrder}: {rule.Name}");
            
            // Pipe results
            baseContext.PreviousResult = lastResult;
            
            try
            {
                lastResult = await ExecuteRuleAsync(rule, baseContext, appendLog);
                // Store in history
                baseContext.StepResults[item.SequenceOrder] = lastResult;
            }
            catch (Exception ex)
            {
                appendLog?.Invoke($"[BUNDLE] [FATAL] Step failed: {ex.Message}. Aborting bundle.");
                break;
            }
        }

        appendLog?.Invoke($"[BUNDLE] --- Bundle Finished: {bundle.Name} ---");
        return lastResult;
    }

    private async Task<object?> ExecuteTsqlAsync(string code, BusinessRuleContext? context, Action<string>? appendLog)
    {
        appendLog?.Invoke("[SQL] Executing T-SQL script...");
        await using var connection = new SqlConnection(_connectionString);
        
        // Serialize PreviousResult to JSON to allow T-SQL to parse it using OPENJSON
        var parameters = new DynamicParameters();
        
        var jsonOptions = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        
        // Use default empty values if context is null to prevent "Must declare variable" errors
        string previousJson = context != null ? JsonSerializer.Serialize(context.PreviousResult, jsonOptions) : "[]";
        string stepResultsJson = context != null ? JsonSerializer.Serialize(context.StepResults, jsonOptions) : "{}";

        parameters.Add("PreviousResultJson", previousJson);
        parameters.Add("StepResultsJson", stepResultsJson);

        var results = await connection.QueryAsync<dynamic>(code, parameters);
        var resultList = results.ToList();
        
        appendLog?.Invoke($"[SQL] Execution completed. {resultList.Count} rows returned.");
        return resultList;
    }

    private async Task<object?> ExecuteCSharpAsync(string code, object? globals, Action<string>? appendLog)
    {
        appendLog?.Invoke("[CS] Compiling and executing C# script...");
        
        var options = ScriptOptions.Default
            .AddReferences(typeof(System.Linq.Enumerable).Assembly)
            .AddReferences(typeof(BusinessRuleContext).Assembly)
            .AddImports("System", "System.Collections.Generic", "System.Linq", "System.Text", "AutoSysJilBlazor.Models");

        try
        {
            var result = await CSharpScript.EvaluateAsync(code, options, globals, globals?.GetType());
            appendLog?.Invoke("[CS] Execution completed successfully.");
            return result;
        }
        catch (CompilationErrorException ex)
        {
            appendLog?.Invoke($"[ERR] Compilation Error: {string.Join(Environment.NewLine, ex.Diagnostics)}");
            throw;
        }
    }
}

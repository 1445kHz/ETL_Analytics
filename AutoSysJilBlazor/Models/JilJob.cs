namespace AutoSysJilBlazor.Models;

public class JilJob
{
    public string JobName { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty;
    public string Command { get; set; } = string.Empty;
    public string Machine { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string Permission { get; set; } = string.Empty;
    public string DateConditions { get; set; } = string.Empty;
    public string DaysOfWeek { get; set; } = string.Empty;
    public string StartMins { get; set; } = string.Empty;
    public string StartTimes { get; set; } = string.Empty;
    public string Timezone { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StdOutFile { get; set; } = string.Empty;
    public string StdErrFile { get; set; } = string.Empty;
    public string AlarmIfFail { get; set; } = string.Empty;
    public string Application { get; set; } = string.Empty;
    public string RawText { get; set; } = string.Empty;
    public Dictionary<string, string> AdditionalProperties { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}

public class JilImportResult
{
    public IReadOnlyList<JilJob> Jobs { get; set; } = Array.Empty<JilJob>();
    public string SqlTableDefinition { get; set; } = string.Empty;
    public string SourceName { get; set; } = string.Empty;
}

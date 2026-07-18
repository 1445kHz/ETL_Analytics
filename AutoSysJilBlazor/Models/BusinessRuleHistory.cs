namespace AutoSysJilBlazor.Models;

public class BusinessRuleHistory
{
    public int HistoryId { get; set; }
    public int RuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public RuleType RuleType { get; set; }
    public string Code { get; set; } = string.Empty;
    public int Version { get; set; }
    public DateTime ArchivedAt { get; set; }
}

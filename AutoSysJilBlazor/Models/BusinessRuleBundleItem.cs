namespace AutoSysJilBlazor.Models;

public class BusinessRuleBundleItem
{
    public int Id { get; set; }
    public int BundleId { get; set; }
    public int RuleId { get; set; }
    public int SequenceOrder { get; set; }
    
    // Joined data for UI
    public string RuleName { get; set; } = string.Empty;
    public RuleType RuleType { get; set; }
}

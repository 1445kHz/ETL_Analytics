using System;
using System.Collections.Generic;

namespace AutoSysJilBlazor.Models;

public class BusinessRuleBundle
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // UI Helper
    public List<BusinessRuleBundleItem> Items { get; set; } = new();
}

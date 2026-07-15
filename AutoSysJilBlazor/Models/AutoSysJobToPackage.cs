using System;

namespace AutoSysJilBlazor.Models;

public class AutoSysJobToPackage
{
    public string JobName { get; set; } = string.Empty;
    public string PackageName { get; set; } = string.Empty;
    public DateTime ImportedAt { get; set; }
}

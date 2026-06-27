namespace SimpleTxnMonitor.Core.Models;

public class RuleResult
{
    public bool IsSuspicious { get; set; }
    public List<string> FlaggedReasons { get; set; } = [];
}

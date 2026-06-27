using SimpleTxnMonitor.Core.Models;

namespace SimpleTxnMonitor.Core.Interfaces;

public interface ITransactionRuleEngine
{
    Task<RuleResult> EvaluateAsync(Transaction transaction);
}
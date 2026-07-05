using System.Text.Json;
using System.Transactions;
using Microsoft.Extensions.Logging;
using RulesEngine.Models;
using SimpleTxnMonitor.Core.Interfaces;
using SimpleTxnMonitor.Core.Models;

using Transaction = SimpleTxnMonitor.Core.Models.Transaction;

namespace SimpleTxnMonitor.Infrastructure.Services;

public class RulesEngineService : ITransactionRuleEngine
{
    private readonly RulesEngine.RulesEngine _rulesEngine;
    private readonly ILogger<RulesEngineService> _logger;

    public RulesEngineService(ILogger<RulesEngineService> logger)
    {
        _logger = logger;
        var rulesPath = Path.Combine(AppContext.BaseDirectory, "Rules", "TransactionRules.json");
        var rulesJson = File.ReadAllText(rulesPath);

        var wokrflows = JsonSerializer.Deserialize<List<Workflow>>(rulesJson);
        _rulesEngine = new RulesEngine.RulesEngine(wokrflows.ToArray());
    }

    public async Task<RuleResult> EvaluateAsync(Transaction transaction)
    {
        _logger.LogInformation("Evaluating transaction {Id}", transaction.Id);

        var results = await _rulesEngine.ExecuteAllRulesAsync("TransactionMonitoring", transaction);

        var ruleResult = new RuleResult { IsSuspicious = false };

        foreach (var result in results)
        {
            if (result.IsSuccess)
            {
                ruleResult.IsSuspicious = true;
                ruleResult.FlaggedReasons.Add(result.Rule.RuleName);
                _logger.LogWarning("Transaction {Id} flagged by rule: {Rule}", transaction.Id, result.Rule.RuleName);
            }
        }

        return ruleResult;
    }
}
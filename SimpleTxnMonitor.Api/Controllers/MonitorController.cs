using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using SimpleTxnMonitor.Core.Interfaces;
using SimpleTxnMonitor.Core.Models;

using Transaction = SimpleTxnMonitor.Core.Models.Transaction;

namespace SimpleTxnMonitor.Api;

[ApiController]
[Route("api/[controller]")]
public class MonitorController : ControllerBase
{
    private readonly ITransactionRuleEngine _ruleEngine;
    public MonitorController(ITransactionRuleEngine ruleEngine)
    {
        _ruleEngine = ruleEngine;
    }

    public async Task<IActionResult> EvaluateTransactionAsync([FromBody] Transaction transaction)
    {
        var result = await _ruleEngine.EvaluateAsync(transaction);

        if (result.IsSuspicious)
        {
            return BadRequest(new
            {
                Message = "Transaction flagged for review.",
                Reasons = result.FlaggedReasons
            });
        }

        return Ok(new { message = "Transaction Approved" });
    }
}
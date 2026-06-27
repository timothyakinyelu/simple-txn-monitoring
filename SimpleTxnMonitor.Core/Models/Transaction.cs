namespace SimpleTxnMonitor.Core.Models;

public class Transaction
{
    public string Id { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Country { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

using SimpleTxnMonitor.Core.Interfaces;
using SimpleTxnMonitor.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddSingleton<ITransactionRuleEngine, RulesEngineService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();

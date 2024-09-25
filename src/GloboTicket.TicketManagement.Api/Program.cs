using GloboTicket.TicketManagement.Api.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("GloboTicket API starting.");

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console();
}, true);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

// Serilog Middleware
app.UseSerilogRequestLogging();

app.Run();

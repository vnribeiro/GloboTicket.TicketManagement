using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GloboTicket.TicketManagement.Infrastructure.Extensions;

public static class InfrastructureServiceRegistrationExtensions
{
    private const string EmailSettings = "EmailSettings";

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add email settings to the service collection
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings));
        services.AddTransient<IEmailService, EmailService>();
        return services;
    }
}


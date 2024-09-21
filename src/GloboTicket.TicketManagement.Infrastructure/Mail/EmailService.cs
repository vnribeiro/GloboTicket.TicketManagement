using System.Net;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Models.Mail;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace GloboTicket.TicketManagement.Infrastructure.Mail;

public class EmailService : IEmailService
{
    public EmailSettings EmailSettings { get; }

    public EmailService(IOptions<EmailSettings> settings)
    {
        EmailSettings = settings.Value;
    }

    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(EmailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = EmailSettings.FromAddress,
            Name = EmailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        return response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK;
    }
}

using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.EmailService;

public class EmailService : IEmailService
{
    public EmailSettings Settings { get; }
    public EmailService(IOptions<EmailSettings> settings) {
        Settings = settings.Value;
    }

    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(Settings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress(Settings.FromAddress, Settings.FromName);

        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.PlainBody, email.HtmlBody);

        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}

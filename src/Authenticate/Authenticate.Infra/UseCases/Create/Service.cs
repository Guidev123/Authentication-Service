using Authenticate.Domain.Configuration;
using Authenticate.Domain.Entities;
using Authenticate.Domain.UseCases.Create.Contracts;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Authenticate.Infra.UseCases.Create
{
    public class Service : IService
    {
        public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            var client = new SendGridClient(ApiConfiguration.SecurityKeys.ApiKey);
            var from = new EmailAddress(ApiConfiguration.Email.DefaultFromEmail, ApiConfiguration.Email.DefaultFromName);

            const string subject = "Verify account";

            var to = new EmailAddress(user.Email, user.Name);
            var content = $"Code: {user.Email.EmailVerification.Code}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            await client.SendEmailAsync(msg, cancellationToken);
        }
    }
}

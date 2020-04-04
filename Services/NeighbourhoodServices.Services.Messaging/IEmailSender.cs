namespace NeighbourhoodServices.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string htmlContent,
            string from = "test@test.com",
            string fromName = "test",
            IEnumerable<EmailAttachment> attachments = null);
    }
}

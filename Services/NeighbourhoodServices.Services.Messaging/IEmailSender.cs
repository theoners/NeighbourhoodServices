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
            string from = "admin@NeighbourhoodServices.com",
            string fromName = "admin",
            IEnumerable<EmailAttachment> attachments = null);
    }
}

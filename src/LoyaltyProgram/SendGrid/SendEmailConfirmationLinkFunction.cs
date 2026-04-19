using Loyalty.Application.Storage.Dto;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;

namespace LoyaltyProgram.SendGrid
{
    public class SendEmailConfirmationLinkFunction
    {
        private readonly IOptions<EmailSettings> settings;

        public SendEmailConfirmationLinkFunction(
            IOptions<EmailSettings> settings)
        {
            this.settings = settings;
        }

        [FunctionName("SendEmailConfirmationLinkFunction")]
        [return: SendGrid(ApiKey = "SendGridKey", To = "{CustomerEmail}", From = "{SenderEmail}")]
        public SendGridMessage Run([QueueTrigger("invite-mail", Connection = "")]EmailInvitationDto emailInvitation, ILogger log)
        {
            log.LogInformation($"{nameof(SendEmailConfirmationLinkFunction)} processed email: {emailInvitation.CustomerEmail}");

            SendGridMessage message = null;

            if (!string.IsNullOrEmpty(emailInvitation.Link))
            {
                message = new SendGridMessage
                {
                    TemplateId = settings.Value.InviteTemplateId
                };

                var dynamicTemplateData = new 
                {
                    VerifyLink = emailInvitation.Link,
                };
                message.SetTemplateData(dynamicTemplateData);
            }
            else
            {
                log.LogError($"{nameof(SendEmailConfirmationLinkFunction)} Message params are null: {emailInvitation}");
            }

            return message;
        }
    }
}

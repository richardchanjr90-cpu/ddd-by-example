using System;

namespace Loyalty.Application.Storage.Dto
{
    public class EmailInvitationDto
    {
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Link { get; set; }
    }
}

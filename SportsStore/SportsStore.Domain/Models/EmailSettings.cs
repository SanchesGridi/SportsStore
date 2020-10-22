using System.Net.Mail;
using SportsStore.Domain.Processors;

namespace SportsStore.Domain.Models
{
    public class EmailSettings
    {
        public MailAddress RecipientMailAddress { get; set; }
        public SenderMailAddress SenderMailAddress { get; set; }

        public string ServerName { get; set; }
        public int ServerPort { get; set; }
        public bool EnableSsl { get; set; }
        public SmtpDeliveryMethod SmtpDeliveryMethod { get => SmtpDeliveryMethod.Network; }
    }
}

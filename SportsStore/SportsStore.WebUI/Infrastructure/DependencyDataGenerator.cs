using System.Configuration;
using System.Net.Mail;
using SportsStore.Domain.Models;

namespace SportsStore.WebUI.Infrastructure
{
    public static class DependencyDataGenerator
    {
        public static EmailSettings GenerateEmailSettings()
        {
            var emailSettings = new EmailSettings
            {
                RecipientMailAddress = new MailAddress(
                    address: ConfigurationManager.AppSettings["Email.RecipientAddress"],
                    displayName: ConfigurationManager.AppSettings["Email.RecipientDisplayName"]
                ),
                SenderMailAddress = new SenderMailAddress(
                    senderAddress: ConfigurationManager.AppSettings["Email.SenderAddress"],
                    senderDisplayName: ConfigurationManager.AppSettings["Email.SenderDisplayName"],
                    senderPassword: ConfigurationManager.AppSettings["Email.SenderPassword"]
                ),

                ServerName = ConfigurationManager.AppSettings["Email.ServerName"],
                ServerPort = int.Parse(ConfigurationManager.AppSettings["Email.ServerPort"]),
                EnableSsl = bool.Parse(ConfigurationManager.AppSettings["Email.EnableSsl"])
            };

            return emailSettings;
        }
    }
}
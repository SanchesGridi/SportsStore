using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Processors
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private readonly EmailSettings _emailSettings;
        private readonly OrderMessageBuilder _orderMessageBuilder;

        public EmailOrderProcessor(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
            _orderMessageBuilder = new OrderMessageBuilder();
        }

        public async Task ProcessOrderAsync(Cart cart, ShippingDetails shippingDetails)
        {
            using (var mailMessage = new MailMessage(_emailSettings.SenderMailAddress, _emailSettings.RecipientMailAddress)
            {
                Subject = _orderMessageBuilder.GetOrderTitle(),
                Body = await _orderMessageBuilder.BuildOrderMessageAsync(cart, shippingDetails)
            })
            {
                using (var smtpClient = new SmtpClient
                {
                    EnableSsl = _emailSettings.EnableSsl,
                    Host = _emailSettings.ServerName,
                    Port = _emailSettings.ServerPort,
                    DeliveryMethod = _emailSettings.SmtpDeliveryMethod,

                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettings.SenderMailAddress.Address, _emailSettings.SenderMailAddress.Password)
                })
                {
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
    }
}

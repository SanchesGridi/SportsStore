using System.Net.Mail;

namespace SportsStore.Domain.Models
{
    public class SenderMailAddress : MailAddress
    {
        private readonly string _senderPassword;

        public string Password
        {
            get => _senderPassword;
        }
        public new string Address
        {
            get => base.Address;
        }

        public SenderMailAddress(string senderAddress, string senderDisplayName, string senderPassword) : base(senderAddress, senderDisplayName)
        {
            _senderPassword = senderPassword;
        }
    }
}

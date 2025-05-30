

using System.Net;
using System.Net.Mail;
using Tsi.Erp.TestTracker.Abstractions;

namespace Tsi.Erp.TestTracker.Core.Services
{
    public class MailService : IDisposable
    {
        private readonly SmtpClient smtpClient;
        private readonly string _userName;

        public MailService(string host, int port, string userName,string password)
        {
            smtpClient = new SmtpClient(host)
            {
                Port = port,
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = true,
            };
            _userName = userName;
        }

        public void Send(MailMessage message)
        {
            var messageToSend = new System.Net.Mail.MailMessage(message.From, string.Join(';', message.To), message.Subject, message.Body)
            {
                IsBodyHtml = message.IsBodyHtml
            };
            smtpClient.Send(messageToSend);
        }

        public void Send(string subject , string body, string[] recipients)
        {
            var messageToSend = new System.Net.Mail.MailMessage(_userName, string.Join(';', recipients), subject,body)
            {
                IsBodyHtml = true
            };
            smtpClient.Send(messageToSend);
        }

        public void Dispose()
        {
            this.smtpClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class MailMessage
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public string Subject { get; set; }
        public string Body {  get; set; } 
        public bool IsBodyHtml {  get; set; }

    } 
}

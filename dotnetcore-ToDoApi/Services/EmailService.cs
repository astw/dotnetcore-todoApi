using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
namespace ToDoApi.Services
{
    public interface IEmailService
    {
        void SendEmail(string sendTo, string subject, string emailBody);
    }

    public class EmailService : IEmailService
    {
        #region fields

        private readonly ILogger _logger;
        private readonly string _supportEmailAddress;
        private readonly string _senderEmailAddres;
        private readonly string _senderFistName;
        private readonly string _senderLastName;
        private readonly string _emailServer;

        #endregion

        #region constructors and destructor

        public EmailService(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            //_supportEmailAddress = ConfigurationManager<>.AppSettings["supportEmailAddress"];
            //_senderEmailAddres = ConfigurationManager.AppSettings["senderEmailAddress"];
            //_senderFistName = ConfigurationManager.AppSettings["senderFirstName"];
            //_senderLastName = ConfigurationManager.AppSettings["senderLastName"];
            //_emailServer = ConfigurationManager.AppSettings["emailServer"];
        }

        #endregion

        public void SendEmail(string sendTo, string subject, string emailBody)
        {
            var recipients = sendTo.Split(',').ToList();

            try
            {
                using (var mail = new MailMessage())
                {
                    mail.Subject = subject;
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress(_senderEmailAddres, $"{_senderFistName},{_senderLastName}");
                    mail.Bcc.Add(item: new MailAddress(_supportEmailAddress));

                    foreach (var email in recipients)
                    {
                        if (IsValidEmail(email))
                        {
                            mail.To.Add(new MailAddress(email));
                        }
                    }

                    using (var client = new SmtpClient
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = _emailServer,
                    })
                    {
                        client.Send(mail);
                        _logger.LogDebug(
                            message:
                            $"#LogToEmailLogFile -LinkoService EmailService. SendEmail successful. "
                            + $"Recipient User name:{recipients} "
                            + $"Content:{emailBody}");
                    }
                }
            }
            catch (Exception ex)
            {
                var errors = new List<string> {ex.Message};
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    errors.Add(item: ex.Message);
                }

                _logger.LogInformation(
                    message:
                    $"#LogToEmailLogFile-LinkoService EmailService. SendEmail successful. "
                    + $"Recipient User name:{recipients} "
                    + $"Content:{emailBody}");

                _logger.LogError("Error:LinkoService EmailService.SendEmails. {0} ", string.Join(separator: "," + Environment.NewLine, values: errors));
            }
        }

        public static bool IsValidEmail(string email)
        {
            const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" +
                                   @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" +
                                   @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }
    }
}
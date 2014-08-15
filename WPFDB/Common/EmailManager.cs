using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WPFDB.Common
{
    public class EmailManager
    {
        private static EmailManager instance;

        public static EmailManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmailManager();
                }
                return instance;
            }
        }

        private static void SendMail(string smtpServer, int port, string from, string login, string password,
  List<string> mailToList , string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                foreach (var mailTo in mailToList)
                {
                    mail.To.Add(mailTo);
                }
                mail.Subject = caption;
                mail.Body = message;
             //   if (!string.IsNullOrEmpty(attachFile))
             //       mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = port;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(login, password);
                
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
                MessageBox.Show("Mail sended");
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        public void SendMailForAbstract(List<string> emails, string topic, string message, string attachFile = null )
        {
            var mailServer = DefaultManager.Instance.GetMailServer();
            var mailLogin = DefaultManager.Instance.GetMailLogin();
            var mailPassword = DefaultManager.Instance.GetMailPassword();
            var mailFrom = DefaultManager.Instance.GetMailFrom();
            var mailPort = DefaultManager.Instance.GetMailPort();
            SendMail(mailServer, mailPort, mailFrom, mailLogin, mailPassword, emails, topic, message, attachFile);


        }
    }
}

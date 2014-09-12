using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
            get { return instance ?? (instance = new EmailManager()); }
        }

        private static void SendMail(string smtpServer, int port, string from, string login, string password,
  IEnumerable<string> mailToList , string caption, string message, string attachFile = null)
        {
            try
            {
                /* здесь указываете SMTP и Порт, у меня например mail.ru - я 
указал smtp.mail.ru, а порт smtp.mail.ru - 25 или 2525 */
                SmtpClient Smtp = new SmtpClient(smtpServer);
                Smtp.Port = port;
                /* здесь на месте login указываете логин, на месте password - пароль, 
                если у вас example@mail.ru то указываете просто example (без mail.ru) */
                Smtp.Credentials = new NetworkCredential(login, password);
                Smtp.EnableSsl = true;
              //  Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                var Message = new MailMessage();

                /* на месте login@mail.ru указываете свой E-mail, на месте KUDA@rambler.ru 
                указываете куда будет отправлено письмо (это может быть не обязательно rambler)*/
                Message.From = new MailAddress(from);
                foreach (var mail in mailToList)
                {
                    Message.To.Add(mail);    
                }
                
                /*Тема сообщения на месте Theme и текст сообщения на месте Text*/
                Message.Subject = caption;
                Message.Body = message;

                Message.SubjectEncoding = Encoding.UTF8;
                Message.BodyEncoding = Encoding.UTF8;
                Message.IsBodyHtml = true;
 
                var attach = new Attachment(attachFile, MediaTypeNames.Application.Octet);
                Message.Attachments.Add(attach);

                Smtp.Send(Message); //сообщение отправлено

             //   MailMessage mail = new MailMessage();
             //   mail.From = new MailAddress("614614@rambler.ru");
             //   foreach (var mailTo in mailToList)
             //   {
             //       mail.To.Add(mailTo);
             //   }
             //   mail.Subject = caption;
             //   mail.Body = message;
             ////   if (!string.IsNullOrEmpty(attachFile))
             ////       mail.Attachments.Add(new Attachment(attachFile));
             //   SmtpClient client = new SmtpClient();
             //   client.Host = smtpServer;
             //   client.Port = port;
             //   client.EnableSsl = true;
             //   client.Credentials = new NetworkCredential(login, password);
                
             //   client.DeliveryMethod = SmtpDeliveryMethod.Network;
             //   client.Send(mail);
             //   mail.Dispose();
                MessageBox.Show("Mail sended");
            }
            catch (Exception e)
            {
                MessageBox.Show("Mail.Send: " + e.Message);
            }
        }

        public void SendMailForAbstract(List<string> emailsTo,string emailFrom, string topic, string message, string attachFile = null )
        {
            var mailServer = DefaultManager.Instance.GetMailServer();
            var mailLogin = DefaultManager.Instance.GetMailLogin();
            var mailPassword = DefaultManager.Instance.GetMailPassword();
         //   var mailFrom = DefaultManager.Instance.GetMailFrom();
            var mailPort = DefaultManager.Instance.GetMailPort();
            SendMail(mailServer, mailPort, emailFrom, mailLogin, mailPassword, emailsTo, topic, message, attachFile);


        }
    }
}

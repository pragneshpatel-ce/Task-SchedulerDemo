using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TaskSchedulerDemo.Helper
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule(() => SendMail())
                .WithName("SendMailThread").ToRunOnceAt(DateTime.Now.AddMinutes(1)).AndEvery(1).Minutes();
        }

        public void SendMail()
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]),
                    Subject = "Test Schedule Mail",
                    Body = "Hi.. This is test mail for schedule......",
                    IsBodyHtml = true
                };
                mail.To.Add("receive_email_id@domain.com");
                var smtp = new SmtpClient();
                smtp.Send(mail);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmtpClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SmtpClientExample running...");
            Console.WriteLine(System.Environment.NewLine);

            Console.WriteLine("Sending email...");

            if (SendEmail())
            {
                Console.WriteLine("Email sent successfully...");
            }
            else
            {
                Console.WriteLine(System.Environment.NewLine);
                Console.WriteLine("Failed to send email...");
            }

            Console.WriteLine(System.Environment.NewLine);
            Console.WriteLine("Press any key to exit program...");

            Console.ReadKey();
        }

        public static bool SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();

                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailServerPort"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["DefaultEmailAddress"], ConfigurationManager.AppSettings["DefaultEmailAddressPassword"]);
                client.EnableSsl = true;
                client.Host = ConfigurationManager.AppSettings["MailServer"];

                mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["RecipientEmailAddress"]));
                var senderAddress = new MailAddress(ConfigurationManager.AppSettings["DefaultEmailAddress"], "SmtpClientExample-noreply");
                mail.From = senderAddress;
                mail.Subject = "SmtpClientExample - Email Send Test";
                mail.Body = "SmtpClientExample - Email Send Test";

                client.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Environment.NewLine + "Exception: " + ex.Message);
                if (ex.InnerException != null) Console.WriteLine("Inner Exception: " + ex.InnerException);
                return false;
            }
        }
    }
}

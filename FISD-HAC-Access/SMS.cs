using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RRISD_HAC_Access
{
    public enum SMSCarrier
    {
        ATT,
        Sprint,
        Verizon
    }
    class SMS
    {
        public bool sendSMS(String number, String content, SMSCarrier carrier, Tuple<String,String>credentials)
        {
            if (carrier == SMSCarrier.ATT)
            {
                return sendEmail(number + "@mms.att.net", content, credentials.Item1, credentials.Item2);
            }else if (carrier == SMSCarrier.Sprint)
            {
                return sendEmail(number + "@pm.sprint.com", content, credentials.Item1, credentials.Item2);
            }else if (carrier == SMSCarrier.Verizon)
            {
                return sendEmail(number + "@vzwpix.com", content, credentials.Item1, credentials.Item2);
            }
            return false;
        }
        public bool sendEmail(String address,String content, String email, String password)
        {
            try {
                MailAddress fromAddress = new MailAddress(email, "HAC SMS");
                MailAddress toAddress = new MailAddress(address, "Recepient");
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };
                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Automated message from HAC SMS",
                    Body = content
                })
                {
                    smtp.Send(message);
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void retrieveMessages(Tuple<String, String> credentials)
        {
            try
            {
                System.Net.WebClient objClient = new System.Net.WebClient();

                //Creating a new xml document
                XmlDocument doc = new XmlDocument();

                //Logging in Gmail server to get data
                objClient.Credentials = new System.Net.NetworkCredential(credentials.Item1, credentials.Item2);
                //reading data and converting to string
                String response = Encoding.UTF8.GetString(
                           objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

                response = response.Replace(
                     @"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");

                //loading into an XML so we can get information easily
                doc.LoadXml(response);

                //nr of emails
                String nr = doc.SelectSingleNode(@"/feed/fullcount").InnerText;

                //Reading the title and the summary for every email
                foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
                {
                    String title = node.SelectSingleNode("title").InnerText;
                    String summary = node.SelectSingleNode("summary").InnerText;
                    String from = node.SelectSingleNode("author").SelectSingleNode("email").InnerText;
                    Console.WriteLine(from+":"+summary);
                }

            }
            catch
            {

            }
        }
    }
}

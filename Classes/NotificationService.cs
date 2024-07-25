using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Soc_Management_Web.Classes
{
	public  static class NotificationService
	{
       

       

        public static async Task<bool> NotifyAsync(string to, string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAALJbGiN0:APA91bHA8Pt6aM9XcsdBa1F8OvhxLIVgXAbhEeLexkxzjq_P5Ou8jREvfjU8PJoQqNxFMRStiA4qDMq8qGxpgLWMD6B51Fw0jbSLbX5cAg5w0U2_sGaEwEjCnFAPOydxS8p8v-LF3w0f");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "191508154589");



                var data = new
                {
                    to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Exception thrown in Notify Service: {ex}");
            }

            return false;
        }

        public static void SendMail(string msg, string email,string subject)
        {



            // Create an instance of MailMessage and SmtpClient
            MailMessage mymailmsg = new MailMessage();
            SmtpClient Smtp = new SmtpClient();

            // Set the sender's email address and credentials
            string senderEmail = "email2pioneer@gmail.com";
            string senderPassword = "bswslprzydbebgsq"; // Update with your actual password

            // Set up basic authentication information
            NetworkCredential basicAuthenticationInfo = new NetworkCredential(senderEmail, senderPassword);

            // Set up the email message
            mymailmsg.From = new MailAddress(senderEmail);
            mymailmsg.To.Add(new MailAddress(email));
            mymailmsg.Subject = subject;
            mymailmsg.IsBodyHtml = true;
            mymailmsg.Body = msg;

            // Configure the SMTP client
            Smtp.Host = "smtp.gmail.com";
            Smtp.Port = 587;
            Smtp.EnableSsl = true;
            Smtp.Credentials = basicAuthenticationInfo;

            try
            {
                // Send the email
                Smtp.Send(mymailmsg);
                // Email sent successfully
            }
            catch (SmtpException ex)
            {
                // Handle any errors
                Console.WriteLine("Error sending email: " + ex.Message);
            }

        }
    }
}

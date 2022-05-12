using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class SendEmail
    { 
        public IConfiguration _configuration { get; } 
        public SendEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        } 
        public bool SendComplaintMail(string toEmail, string dept)
        {

            string Body = "<p>Dear " + dept.ToUpper() + ",</p>  <p> A customer's complaint has been submitted for your attention. </p> " +   "Please <strong><a href='" + _configuration["EmailSetting:Apphomepage"].ToString()  + "'> Click here </a></strong> to login to the complaints app and resolve issue. <p>Regards.</p> ";


            try
            {
                string FromAddress = _configuration["EmailSetting:FromAddress"].ToString();
              //  string ToAddress = _configuration["EmailSetting:ToAddress"].ToString();
                string ToAddress = toEmail.ToString();
                MailMessage mail = new MailMessage(FromAddress, ToAddress);
                SmtpClient client = new SmtpClient();
                client.Port = int.Parse(_configuration["EmailSetting:SMTPPORT"].ToString());
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = _configuration["EmailSetting:Host"].ToString();
                mail.Subject = _configuration["EmailSetting:Subject"].ToString();
                mail.Body = Body;
                mail.IsBodyHtml = true;
                client.Send(mail);

                return true;

            }
            catch (System.Exception ex)
            {
                return false;
                // LogWriter("Error  | SendMail | " + ex.Message);
            }

        }
    }
}




using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using BusinessLogic.Core;
using BusinessLogic.Model;
using DataMapping.Entities;
using DataMapping.JSONData;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Helpers
{
    public class EmailHelper
    {

        public static void SendEmail(StandUpMeetingDetails meeting)
        {
            
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient client = new SmtpClient();
                mailMessage.Subject = "Stand Up Meeting";
                mailMessage.Body = CreateEmailHeader(meeting);
                mailMessage.From = new MailAddress("noreply@asfartours.com");
                mailMessage.To.Add("ceo@moreholidays.net");// mohyeldeenmohamednaguib@gmail.com
                mailMessage.IsBodyHtml = true;
                client.Host = "email-smtp.us-east-1.amazonaws.com";
                NetworkCredential authenticationinfo = new NetworkCredential("AKIAJK7TYL5LTR7EXAGQ", "AhmWUM2gmBG7jGvjY3WWLpmKjaWGm2mLUdWhAx98pPjU");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = authenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailMessage.BodyEncoding = Encoding.UTF8;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    StoryName = "Fail to send message to "
                });
            }
        }
        public static void SendEmail(StandUpMeetingData meeting)
        {

            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient client = new SmtpClient();
                mailMessage.Subject = "Stand Up Meeting";
                mailMessage.Body = CreateEmailHeader(meeting);
                mailMessage.From = new MailAddress("noreply@asfartours.com");
                mailMessage.To.Add("ceo@moreholidays.net");// mohyeldeenmohamednaguib@gmail.com
                mailMessage.IsBodyHtml = true;
                client.Host = "email-smtp.us-east-1.amazonaws.com";
                NetworkCredential authenticationinfo = new NetworkCredential("AKIAJK7TYL5LTR7EXAGQ", "AhmWUM2gmBG7jGvjY3WWLpmKjaWGm2mLUdWhAx98pPjU");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = authenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailMessage.BodyEncoding = Encoding.UTF8;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    StoryName = "Fail to send message to "
                });
            }
        }
      
      
        public static void MissingSignOutSendEmail(string email,string UserName)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient client = new SmtpClient();
                mailMessage.Subject = " Missing SignOut ";
                mailMessage.Body = SignOutEmailHeader(UserName);
                mailMessage.From = new MailAddress("noreply@asfartours.com");
                mailMessage.To.Add(email);// osamasadek2020@gmail.com
                mailMessage.IsBodyHtml = true;
                client.Host = "email-smtp.us-east-1.amazonaws.com";
                NetworkCredential authenticationinfo = new NetworkCredential("AKIAJK7TYL5LTR7EXAGQ", "AhmWUM2gmBG7jGvjY3WWLpmKjaWGm2mLUdWhAx98pPjU");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = authenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailMessage.BodyEncoding = Encoding.UTF8;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    StoryName = "Fail to send message to "
                });
            }
        }


        public static string CreateEmailHeader(StandUpMeetingDetails meeting)
        {
            string emailFormat = "<div class=\"col-sm-8 col-sm-offset-4 col-lg-10 col-lg-offset-2 main\"><div class=\"email\" style=\" background-color:##D3D3D3; width:70%;min-hight:500px;margin:0px auto; margin-top:30px;-webkit-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-moz-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-o-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);border-radius: 5px;padding: 10px;\"><div style = \" width:80%;margin:0px auto;\"><!-- email header --><h4 style = \" float:left;\"> " + meeting.Name+ " </h4><span style = \" float:right;font-weight: bold;\">" + meeting.Date.Value.ToShortDateString() + "</span></div><br/><br/><div style = \"width:90%;margin:0px auto;\"><!-- email body --><div><br /><br /><label> Yesterday Job : </label> <span> " + meeting.YesterdayJob + "</span><br/><label> Today Job : </label> <span> " + meeting.TodayJob + "</span><br/><label> Problem : </label> <span> " + meeting.YesterdayObstruction + "</span><br/><label> Reading : </label> <span> " + meeting.Reading + "</span><br/><label> Suggestion : </label> <span> " + meeting.Suggestion + "</span></div></div></div></div>	";
            return emailFormat;
        }
        public static string CreateEmailHeader(StandUpMeetingData meeting)
        {
            string emailFormat = "<div class=\"col-sm-8 col-sm-offset-4 col-lg-10 col-lg-offset-2 main\"><div class=\"email\" style=\" background-color:##D3D3D3; width:70%;min-hight:500px;margin:0px auto; margin-top:30px;-webkit-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-moz-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-o-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);border-radius: 5px;padding: 10px;\"><div style = \" width:80%;margin:0px auto;\"><!-- email header --><h4 style = \" float:left;\"> " + meeting.Name + " </h4><span style = \" float:right;font-weight: bold;\">" + meeting.Date.Value.ToShortDateString() + "</span></div><br/><br/><div style = \"width:90%;margin:0px auto;\"><!-- email body --><div><br /><br /><label> Yesterday Job : </label> <span> " + meeting.YesterdayJob + "</span><br/><label> Today Job : </label> <span> " + meeting.TodayJob + "</span><br/><label> Problem : </label> <span> " + meeting.YesterdayObstruction + "</span><br/><label> Reading : </label> <span> " + meeting.Reading + "</span><br/><label> Suggestion : </label> <span> " + meeting.Suggestion + "</span></div></div></div></div>	";
            return emailFormat;
        }
        public static string MangerSignOutEmailHeader(string Name)
        {

            string emailFormat = "<div class=\"col-sm-8 col-sm-offset-4 col-lg-10 col-lg-offset-2 main\"><div class=\"email\" style=\" background-color:##D3D3D3; width:70%;min-hight:500px;margin:0px auto; margin-top:30px;-webkit-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-moz-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-o-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);border-radius: 5px;padding: 10px;\"><div style = \" width:80%;margin:0px auto;\"><!-- email header --> <h3 style = \" float:left;\">  Warning  </h3></div><br/><br/><div style = \"width:90%;margin:0px auto;\"><!-- email body --><div><br /><br /><label> "+Name+" has no email to recieve missing sign out message </label> <br/></div></div></div></div>	";
            return emailFormat;
        }
        public static string SignOutEmailHeader(string Name)
        {

            string emailFormat = "<div class=\"col-sm-8 col-sm-offset-4 col-lg-10 col-lg-offset-2 main\"><div class=\"email\" style=\" background-color:##D3D3D3; width:70%;min-hight:500px;margin:0px auto; margin-top:30px;-webkit-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-moz-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);-o-box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.14);border-radius: 5px;padding: 10px;\"><div style = \" width:80%;margin:0px auto;\"><!-- email header --> <h3 style = \" float:left;\">  Warning  </h3></div><br/><br/><div style = \"width:90%;margin:0px auto;\"><!-- email body --><div><br /><br /><label> " + Name + " ,you didn't sign out ,and your point will getting low with 40 point </label> <br/></div></div></div></div>	";
            return emailFormat;
        }
     

        

        
    }
}

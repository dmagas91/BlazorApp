
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static BlazorApp.Utility.Constants;

namespace BlazorApp.Utility
{
    public interface IMailService
    {
        AResponse SendMail(Mail.MailHost host, Mail.MsgRecipients recipientType, string subject, string body);
        AResponse SendMail(Mail.MailHost host, string toAddress, string subject, string body);
        string[] GetRecipients(Mail.MsgRecipients recipientType);
    }

        
    public class MailOptions
    {        
        public MailOptions() { }

        public string Host { get; set; }
        public string Port { get; set; }
        public string FromAddress { get; set; }
        public string FromPassword { get; set; }
        public string Admins { get; set; }
    }

    public class MailSender : IMailService
    {
        private SmtpClient Client;

        public MailOptions Options { get; set; }

        private Mail.MailHost mailHost;

        public MailSender(IOptions<MailOptions> options)
        {
            this.Options = options.Value;
        }

        //public MailSender(Mail.MailHost mailHost)
        //{                        
        //    switch (mailHost)
        //    {
        //        case Mail.MailHost.Gmail:
        //            Client = new SmtpClient
        //            {
        //                Host = Options.Host,
        //                Port = Convert.ToInt32(Options.Port),
        //                EnableSsl = true,
        //                DeliveryMethod = SmtpDeliveryMethod.Network,
        //                UseDefaultCredentials = false,
        //                Credentials = new NetworkCredential(Options.FromAddress, Options.FromPassword)
        //            };
        //            break;

        //        default:
        //            break;
        //    }
        //}

        private void ParseXml(ref MailAddress fromAddress, ref String fromPassword, ref String host, ref String port, ref String[] admins, string mailHost)
        {
            //var mailConfig = "";
            //var config = "";

            //if (config != null)
            //{
            //    host = config.Host;
            //    port = (config.Port != null) ? config.Port : String.Empty;
            //    fromAddress = new MailAddress(config.Mappings[0].Address, config.Mappings[0].DisplayName);
            //    fromPassword = config.Mappings[0].Password;
            //}
        }

        public AResponse SendMail(Mail.MailHost host, Mail.MsgRecipients recipientType, string subject, string body)
        {
            AResponse response = new AResponse();

            try
            {
                String[] recipients = GetRecipients(recipientType);
                foreach (String rec in recipients)
                {
                    var resp = SendMail(host, rec, subject, body);
                    if (resp.ResponseStatus != Enums.ActionResponse.Success)
                    {
                        throw resp.Exception;
                    }
                }

                response.ResponseStatus = Enums.ActionResponse.Success;
                response.StatusMessage = String.Format("MailHost:{0} - {1}", host.ToString(), Constants.Mail.SuccessSending);
            }
            catch (Exception ex)
            {
                response.StatusMessage = String.Format("MailHost:{0} - {1}", host.ToString(), Constants.Mail.ErrorWhileSendingMail);
                response.ResponseStatus = Enums.ActionResponse.Unknown;
                response.Exception = ex;
            }

            return response;
        }

        public string[] GetRecipients(Mail.MsgRecipients recipientType)
        {
            //var recs = WebConfigurationManager.AppSettings[recipientType.ToString()].ToString().Split(';');
            string[] recs = new string[0];
            return recs ;
        }

        public AResponse SendMail(Mail.MailHost host, string toAddress, string subject, string body)
        {
            AResponse response = new AResponse();
            try
            {
                switch (host)
                {
                    case Mail.MailHost.Gmail:
                        var message = new MailMessage(new MailAddress(Options.FromAddress,""), new MailAddress(toAddress, ""));
                        message.Subject = subject;
                        message.Body = body;

                        Client.Send(message);
                        break;

                    default:
                        throw new Exception("Mail client Unknown!");
                        break;
                }

                response.ResponseStatus = Enums.ActionResponse.Success;
                response.StatusMessage = String.Format("MailHost:{0} - {1}", host.ToString(), Constants.Mail.SuccessSending);
            }
            catch (Exception ex)
            {
                response.StatusMessage = String.Format("MailHost:{0} - {1}", host.ToString(), Constants.Mail.ErrorWhileSendingMail);
                response.ResponseStatus = Enums.ActionResponse.Unknown;
                response.Exception = ex;
            }

            return response;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Utility
{
    public class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

        public abstract class LogRecords
        {
            public static string SuccessInsert = "Record was successfully inserted!";
            public static string ErrorInsert = "Error occured while inserting logrecord!";
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract class Mail
        {
            public static string ErrorWhileSendingMail = "Error occured while sending mail";
            public static string SuccessSending = "Message was sent successfully to the recipients";

            /// <summary>
            /// 
            /// </summary>
            public enum MsgRecipients
            {
                Admins,
                Officers,
                Testers
            }
            /// 
            /// <summary>
            /// 
            /// </summary>
            public enum MailHost
            {
                Metronet,
                Gmail
            }

            public enum MailMessageType
            {
                Error,
                Information
            }
        }        
    }
}

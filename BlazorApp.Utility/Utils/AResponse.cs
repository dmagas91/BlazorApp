using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Utility
{
    /// <summary>
    /// Holds response enum (Action Stats Report - Success, Failed..) and friendly message for all methods for database communication
    /// </summary>
    public class AResponse
    {
        #region properties
        public Enums.ActionResponse ResponseStatus { get; set; }

        public object Data { get; set; }
        public string StatusMessage { get; set; }
        public int StatusCode { get; set; }
        public Exception Exception { get; set; }
        #endregion
    }

    public class Enums
    {
        #region Status
        public enum Status
        {
            Allowed = 0,
            Denied = 1
        }
        #endregion

        #region ActionResponse
        /// <summary>
        /// Save response values
        /// </summary>
        public enum ActionResponse
        {
            Success = 0,
            NoMandatoryValues = 1,
            DBUpdateFailed = 2,
            DBGetDataFailed = 3,
            DBInsertFailed = 4,
            InvalidValues = 5,
            Unknown = 6
        }
        #endregion
    }
}

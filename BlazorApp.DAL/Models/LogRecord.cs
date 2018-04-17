using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.DAL.Models
{
    public class LogRecord
    {
        public LogRecord() { }

        public LogRecord(System system, Level level, string username, string message, string exception)
        {
            this.LogTime = DateTime.Now;
            this.System = system.ToString();
            this.Level = level.ToString();
            this.Username = username;
            this.Message = message;
            this.Exception = exception;
        }

        [Key]
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string System { get; set; }
        public string Level { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

    }

    public enum System
    {
        ROOT
    }

    public enum Level
    {
        Information,
        Notification,
        Warning,
        Error
    }
}

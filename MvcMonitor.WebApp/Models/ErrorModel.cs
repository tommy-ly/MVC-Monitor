using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using MvcMonitor.StackTrace;

namespace MvcMonitor.Models
{
    [Serializable]
    public class ErrorModel
    {
        public virtual Guid Id { get; set; }

        public virtual Guid ErrorId { get; set; }

        public virtual string Application { get; set; }

        public virtual DateTime Time { get; set; }

        public virtual string Username { get; set; }

        public virtual string Host { get; set; }

        public virtual string Url { get; set; }

        public virtual string QueryString { get; set; }

        public virtual HttpStatusCode StatusCode { get; set; }

        public virtual string RequestMethod { get; set; }

        public virtual string UserAgent { get; set; }


        public virtual string ExceptionType { get; set; }

        public virtual string ExceptionMessage { get; set; }

        public virtual string ExceptionSource { get; set; }

        public virtual string ExceptionStackTrace { get; set; }


        public virtual string ServerName { get; set; }

        public virtual int ServerPort { get; set; }

        public virtual string ServerPortSecure { get; set; }

        public virtual string ServerApplicationPath { get; set; }

        public virtual string ServerApplicationPathTranslated { get; set; }

        [XmlIgnore]
        public virtual List<string> ExceptionLocations { get; set; }

        public virtual void PopulateCalculatedFields()
        {
            var exceptionLocations = new StackTraceProcessor().GetLocalLocations(ExceptionStackTrace).Locations;
            ExceptionLocations = exceptionLocations.Select(location => location.Split(new[] {'\\'}).Last()).ToList();
        }
    }
}
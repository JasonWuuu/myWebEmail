using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;

namespace MyWebEmail.Models
{
    public class Mail
    {
        public string From { get; set; }
        public List<string> To { get; set; }

        public string Subject { get; set; }

        public Encoding SubjectEncoding { get; set; }

        public string Body { get; set; }
        public Encoding BodyEncoding { get; set; }

        public bool IsBodyHtml { get; set; }

        public MailPriority Priority { get; set; }

        public List<Attachment> Attachments { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public Mail() { }

        public Mail(string from, List<string> to, string subject, string body, MailPriority priority, string status, DateTime createdDate)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            Priority = priority;
            Status = status;
            CreatedDateTime = createdDate;
        }
    }
}
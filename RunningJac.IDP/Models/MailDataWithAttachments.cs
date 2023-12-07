#nullable enable
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningJac.IDP.Models
{
    public class MailDataWithAttachments
    {
        public List<string>? To { get; }
        public List<string>? Bcc { get; }
        public List<string>? Cc { get; }
        public string? From { get; set; }
        public string? DisplayName { get; set; }
        public string? ReplyTo { get; set; }
        public string? ReplyToName { get; set; }

        // Content
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public IFormFileCollection? Attachments { get; set; }
    }
}

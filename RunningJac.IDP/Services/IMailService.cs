using RunningJac.IDP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RunningJac.IDP.Services
{
    public interface IMailService
    {
        Task SendAsync(MailData data, CancellationToken ct = default);
        //Task SendWithAttachmentsAsync(MailDataWithAttachments mailData, CancellationToken ct);
        string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
    }
}

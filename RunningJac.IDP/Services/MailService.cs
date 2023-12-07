using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;
using RunningJac.IDP.Configuration;
using System.Threading;
using RunningJac.IDP.Models;
using RazorEngineCore;
using System.IO;
using MailKit;

namespace RunningJac.IDP.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(IOptions<MailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendAsync(MailData mailData, CancellationToken ct = default)
        {
            try
            {
                var mail = new MimeMessage();

                #region Sender / Receiver
                mail.From.Add(new MailboxAddress(_settings.DisplayName, mailData.From ?? _settings.From));
                mail.Sender = new MailboxAddress(mailData.DisplayName ?? _settings.DisplayName, mailData.From ?? _settings.From);

                foreach (string mailAddress in mailData.To)
                    mail.To.Add(MailboxAddress.Parse(mailAddress));

                if (!string.IsNullOrEmpty(mailData.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

                if (mailData.Bcc != null)
                {
                    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                if (mailData.Cc != null)
                {
                    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
                #endregion

                #region Content

                var body = new BodyBuilder();
                mail.Subject = mailData.Subject;
                body.HtmlBody = mailData.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Send Mail

                using var smtp = new SmtpClient();

                if (_settings.UseSSL)
                {
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_settings.UseStartTls)
                {
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
                }
                await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                #endregion
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public string GetEmailTemplate<T>(string emailTemplate, T emailViewModel)
        {
            string mailTemplate = LoadTemplate(emailTemplate);

            return mailTemplate;
            //IRazorEngine razorEngine = new RazorEngine();
            //IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate, builderAction => 
            //{
            //    builderAction.AddUsing("RunningJac.IDP.Models");
            //});

            //return modifiedMailTemplate.Run(emailViewModel);
        }

        public string LoadTemplate(string emailTemplate)
        {
            string baseDir = Directory.GetCurrentDirectory();
            string templateDir = Path.Combine(baseDir, "Files/MailTemplates");
            string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");

            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);

            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }
    }
}

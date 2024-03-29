﻿
using IdentityApp.OptionsModels;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace IdentityApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendPasswordResetEmail(string resetPasswordEmailLink, string ToEmail)
        {
            var smptClient = new SmtpClient
            {
                //Host
                Host = _emailSettings.Host,
                //Delivery Method
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //useDefaultCredentials
                UseDefaultCredentials = false,
                //port
                Port = 587,
                //credentials
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                //enable Ssl
                EnableSsl = true
            };


            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email)
            };
            mailMessage.To.Add(ToEmail);

            //define mail subject
            mailMessage.Subject = "Localhost | Reset Password Link";

            //define mail body
            mailMessage.Body = @$"
                    <h4> Click Link Below to Reset Your Password.</h4>
                    <p><a href = '{resetPasswordEmailLink}'>Password Reset Link</a></p>";

            mailMessage.IsBodyHtml = true;

            await smptClient.SendMailAsync(mailMessage); 
    
        }
    }
}

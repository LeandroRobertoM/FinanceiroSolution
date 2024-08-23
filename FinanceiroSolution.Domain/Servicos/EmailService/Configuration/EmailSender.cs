
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos.EmailService.Configuration
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Seu Nome", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var confirmationLink = message.Content; // Supondo que message.Content contenha o link de confirmação
            var htmlContent = $@"
        <div style='font-family: Arial, sans-serif;'>
            <img src='https://techserra.com.br/email/confirmacao/imagem_logo.png' alt='NDD Logo' style='display:block; margin: 0 auto;'/>
            <h2 style='color:#000000;'>Olá! Desejamos boas-vindas!</h2>
            <p>A partir de agora, você pode utilizar nosso sistema <strong>Fintech</strong>.
            Para ativar sua conta, clique no botão abaixo:</p>
            <a href='{confirmationLink}' style='text-decoration:none;'>
                <button style='background-color:#00b894; color:white; border:none; padding:15px 30px; text-align:center; display:block; margin: 20px auto; cursor:pointer;'>
                    Ativar conta
                </button>
            </a>
            <p>Você tem 24h para ativar sua conta, ok? Depois desse período, solicite um novo acesso à Fintech.
            Caso já tenha ativado, você pode <a href='https://techserra.com.br'>Teste</a>.</p>
            <p>Até breve!<br>Equipe Fintech</p>
            <img src='https://techserra.com.br/email/confirmacao/imagem_footer.png' alt='Footer Image' style='display:block; margin: 0 auto;'/>
        </div>";

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlContent };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }


        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}



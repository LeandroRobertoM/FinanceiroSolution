using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            emailMessage.From.Add(new MailboxAddress("Fintech - Controle de Gastos", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

          
            var htmlContent = GetEmailContent(message.Subject, message.Content);

            // Monta o corpo do email com HTML e anexos
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlContent };
            AddAttachments(bodyBuilder, message.Attachments);

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private string GetEmailContent(string subject, string confirmationLink)
        {
            if (subject.Equals("reiniciar a senha do usuario"))
            {
                return GetPasswordResetEmail(confirmationLink);
            }

            return GetAccountActivationEmail(confirmationLink);
        }

        private string GetPasswordResetEmail(string confirmationLink)
        {
            return $@"<div style='font-family: Arial, sans-serif;'>
                <div style=""background-color: #000000; padding: 15px; text-align: center; color: white;"">
                    <p style=""margin: 0;"">Fintech IA</p>
                </div>
                <h2 style='color:#000000;'>Família Fintech!</h2>
                <p>Recebemos uma solicitação para reinicializar a senha da sua conta no nosso sistema de controle financeiro,se você não fez essa solicitação, por favor ignore este e-mail. Caso contrário, clique no link abaixo para redefinir sua senha:</p>
                <a href='{confirmationLink}' style='text-decoration:none;'>
                    <button style='background-color:#3f22d1; color:white; border:none; padding:15px 30px; text-align:center; display:block; margin: 20px auto; cursor:pointer;'>Redefinir Senha!</button>
                </a>
               <p>Você tem 24h para ativar sua conta, ok? Depois desse período, solicite um novo acesso à Fintech. Caso já tenha ativado, você pode <a href='https://techserra.com.br'>acessar o sistema</a>.</p>
                <p>Até breve!<br> Fintech</p>
                <div style=""background-color: #000000; padding: 20px; text-align: center; color: white;"">
                    <p style=""margin: 0;"">© 2024 Fintech. Todos os direitos reservados.</p>
                </div>
            </div>";
        }

        private string GetAccountActivationEmail(string confirmationLink)
        {
            return $@"<div style='font-family: Arial, sans-serif;'>
                <div style=""background-color: #000000; padding: 15px; text-align: center; color: white;"">
                    <p style=""margin: 0;"">Fintech IA</p>
                </div>
                <h2 style='color:#000000;'>Olá! Seja Bem Vindo!</h2>
                <p>A partir de agora, você pode utilizar nosso sistema de controle de gastos <strong>Fintech</strong>. Para ativar sua conta, clique no botão abaixo:</p>
                <a href='{confirmationLink}' style='text-decoration:none;'>
                    <button style='background-color:#3f22d1; color:white; border:none; padding:15px 30px; text-align:center; display:block; margin: 20px auto; cursor:pointer;'>Ativar conta</button>
                </a>
                <p>Você tem 24h para ativar sua conta, ok? Depois desse período, solicite um novo acesso à Fintech. Caso já tenha ativado, você pode <a href='https://techserra.com.br'>acessar o sistema</a>.</p>
                <p>Até breve!<br> Fintech</p>
                <div style=""background-color: #000000; padding: 20px; text-align: center; color: white;"">
                    <p style=""margin: 0;"">© 2024 Fintech. Todos os direitos reservados.</p>
                </div>
            </div>";
        }

        // Método ajustado para IFormFileCollection
        private void AddAttachments(BodyBuilder bodyBuilder, IFormFileCollection attachments)
        {
            if (attachments != null && attachments.Any())
            {
                foreach (var file in attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        bodyBuilder.Attachments.Add(file.FileName, ms.ToArray(), ContentType.Parse(file.ContentType));
                    }
                }
            }
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

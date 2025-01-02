using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos.EmailService.Configuration
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly OAuthService _oauthService;

        public EmailSender(EmailConfiguration emailConfig, OAuthService oauthService)
        {
            _emailConfig = emailConfig;
            _oauthService = oauthService;
        }

        public async Task SendEmailAsync(Message message) // Aqui utilizamos a classe Message da sua aplicação
        {
            // Obtém o AccessToken atualizado via OAuth2
            var accessToken = await _oauthService.GetAccessTokenAsync();

            // Cria a mensagem de e-mail
            var emailMessage = CreateEmailMessage(message);

            // Envia o e-mail com a API do Gmail
            await SendEmailViaGmailApi(emailMessage, accessToken);
        }

        private MimeMessage CreateEmailMessage(Message message) // Aqui ainda usamos a classe Message da sua aplicação
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Fintech - Controle de Gastos", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            // Define o conteúdo do e-mail com HTML e anexos
            var htmlContent = GetEmailContent(message.Subject, message.Content);
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlContent };
            AddAttachments(bodyBuilder, message.Attachments);

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendEmailViaGmailApi(MimeMessage message, string accessToken)
        {
            var service = new GmailService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromAccessToken(accessToken),
                ApplicationName = "Fintech Email Service",
            });

            // Converte o MimeMessage para base64 antes de enviar pela API do Gmail
            var messageStr = ConvertMimeMessageToBase64(message);
            var gmailMessage = new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = messageStr
            };

            try
            {
                // Envia o e-mail via Gmail API
                var result = await service.Users.Messages.Send(gmailMessage, "me").ExecuteAsync();
                Console.WriteLine($"E-mail enviado com sucesso! ID: {result.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o e-mail via API do Gmail: {ex.Message}");
                throw new Exception("Erro ao enviar o e-mail via API do Gmail.", ex);
            }
        }

        private string ConvertMimeMessageToBase64(MimeMessage message)
        {
            using (var stream = new MemoryStream())
            {
                // Escreve a mensagem MIME para o stream
                message.WriteTo(stream);

                // Converte para Base64
                return Convert.ToBase64String(stream.ToArray())
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Replace("=", string.Empty); // URL-safe base64 encoding
            }
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
                <p>Recebemos uma solicitação para reinicializar a senha da sua conta no nosso sistema de controle financeiro, se você não fez essa solicitação, por favor ignore este e-mail. Caso contrário, clique no link abaixo para redefinir sua senha:</p>
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

        public void SendEmail(Message message)
        {
            throw new NotImplementedException();
        }
    }
}

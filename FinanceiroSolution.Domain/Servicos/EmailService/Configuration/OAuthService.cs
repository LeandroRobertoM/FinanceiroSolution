using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Financeiro.Solution.Infra.ExternalServices.DTO.OAuth;

namespace FinanceiroSolution.Domain.Servicos.EmailService.Configuration
{
    public class OAuthService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _refreshToken;
        private readonly string _tokenUrl;
        private readonly string _redirectUri;

        // O construtor agora recebe a configuração do appsettings.json
        public OAuthService(IConfiguration configuration)
        {
            _clientId = configuration["OAuthSettings:client_id"];
            _clientSecret = configuration["OAuthSettings:client_secret"];
            _refreshToken = configuration["OAuthSettings:refresh_token"];
            _tokenUrl = configuration["OAuthSettings:token_url"];  
            _redirectUri = configuration["OAuthSettings:redirect_uri"];  
        }

        // Método para atualizar o access token usando o refresh token
        public async Task<string> RefreshAccessToken()
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("refresh_token", _refreshToken),
                    new KeyValuePair<string, string>("grant_type", "refresh_token")
                });

                var response = await client.PostAsync(_tokenUrl, content);
                response.EnsureSuccessStatusCode(); // Lança exceção em caso de falha

                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<GoogleOAuthTokenResponse>(responseBody);

                return tokenResponse.AccessToken;
            }
        }

        // Método para obter o access token
        internal async Task<string> GetAccessTokenAsync()
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
        {
            { "client_id", _clientId },
            { "client_secret", _clientSecret },
            { "refresh_token", _refreshToken },
            { "grant_type", "refresh_token" },
            { "redirect_uri", _redirectUri }
        };

                var content = new FormUrlEncodedContent(parameters);

                // Logar os parâmetros enviados em formato JSON
                var jsonParameters = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                Console.WriteLine("Enviando os seguintes parâmetros para o token URL (JSON formatado):");
                Console.WriteLine(jsonParameters);

                HttpResponseMessage response = null;

                try
                {
           
                    response = await client.PostAsync(_tokenUrl, content);

                   
                    Console.WriteLine($"Status da requisição: {response.StatusCode} - {response.ReasonPhrase}");

                 
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Falha ao obter o Access Token. Status: {response.StatusCode}, Motivo: {response.ReasonPhrase}, Resposta: {errorContent}");
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"Conteúdo da resposta: {responseContent}");

           
                    var tokenData = JsonConvert.DeserializeObject<GoogleOAuthTokenResponse>(responseContent);

                    if (tokenData == null || string.IsNullOrEmpty(tokenData.AccessToken))
                    {
                        throw new Exception("Falha ao deserializar o token ou token ausente.");
                    }

                    return tokenData.AccessToken;
                }
                catch (HttpRequestException httpEx)
                {

                    throw new Exception("Erro na comunicação com o servidor: " + httpEx.Message, httpEx);
                }
                catch (JsonSerializationException jsonEx)
                {

                    throw new Exception("Erro ao processar o JSON da resposta: " + jsonEx.Message, jsonEx);
                }
                catch (Exception ex)
                {

                    if (response != null)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Erro inesperado. Status: {response.StatusCode}, Resposta: {errorContent}", ex);
                    }

                    throw new Exception("Erro inesperado ao tentar obter o token.", ex);
                }
            }
        }


        public class TokenResponse
        {
            public string AccessToken { get; set; }
            public string TokenType { get; set; }
            public int ExpiresIn { get; set; }
            public string RefreshToken { get; set; } // Pode ser nulo, se não for renovado
        }
    }
}

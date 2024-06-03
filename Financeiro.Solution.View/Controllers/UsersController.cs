using AutoMapper;
using Financeiro.Solution.View.DTO.Login;
using Financeiro.Solution.View.DTO.User;
using Financeiro.Solution.View.Models;
using Financeiro.Solution.View.Token;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using FinanceiroSolution.Domain.Servicos.EmailService;
using FinanceiroSolution.Domain.Servicos.EmailService.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Financeiro.Solution.View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<User> _userManagers;



        private readonly InterfaceUsuarioCreate _InterfaceUsuarioCreate;
        private readonly IUsuarioCreateServico _IUsuarioCreateServico;
        private readonly ILogger<CategoriaController> _logger;
        private readonly IMapper _mapper;
        private readonly TokenJWTBuilder _jwtHandler;
        private readonly IEmailSender _emailSender;



        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, InterfaceUsuarioCreate interfaceUsuarioCreate, IUsuarioCreateServico IUsuarioCreateServico,IMapper mapper, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _InterfaceUsuarioCreate = interfaceUsuarioCreate;
            _IUsuarioCreateServico = IUsuarioCreateServico;
            _emailSender = emailSender;
            _mapper = mapper;
        }



   
        [Produces("application/json")]
        [HttpPost("/api/UsuarioLogin2")]
        public async Task<IActionResult> Login2([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null)
                return BadRequest("Invalid Request");
            if (!await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Email is not confirmed" });
            if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            
            return Ok(new AuthResponseDto { IsAuthSuccessful = true });
           
        }


        [HttpPost("/api/UsuarioLogin")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Email is not confirmed" });

            if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {

            }
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            /*
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            Ajustar var claims = await _jwtHandler.GetClaims(user);
          /Ajustar  var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            */
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuario")]

        public async Task<IActionResult> AdicionaUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) ||
                string.IsNullOrWhiteSpace(login.senha) ||
                string.IsNullOrWhiteSpace(login.cpf))
            {
                return Ok("Falta alguns do usuario dados");
            }

            var user = new ApplicationUser
            {
                Email = login.email,
                UserName = login.email,
                CPF = login.cpf
            };

            var result = await _userManager.CreateAsync(user, login.senha);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            // Geração de confirmação caso precise 
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno do email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var respose_Retorn = await _userManager.ConfirmEmailAsync(user, code);

            if (respose_Retorn.Succeeded)
            {
                return Ok("Usuario Adionado com sucesso!");
            }
            else
            {
                return Ok("erro ao confirmar cadastro de usuário!");
            }

        }

        [HttpPut("/api/AtualizarUsuario")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(ApplicationUser updatedUser)
        {
            // Encontra o usuário com base no ID
            var user = await _userManager.FindByIdAsync(updatedUser.Id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            // Atualiza as propriedades do usuário
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            // Adicione outras propriedades que você deseja atualizar

            // Salva as mudanças no banco de dados
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Se ocorrer algum erro durante a atualização, retorne uma mensagem de erro
                return BadRequest(result.Errors);
            }

            // Retorna uma resposta de sucesso
            return Ok("Usuário atualizado com sucesso");
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuarioCreate")]
        public async Task<IActionResult> AdicionaUsuarioCreate([FromBody] LoginUserCreate login)
        {
            if (string.IsNullOrWhiteSpace(login.email) ||
                string.IsNullOrWhiteSpace(login.senha) ||
                string.IsNullOrWhiteSpace(login.IdUsuarioLogado) ||
                string.IsNullOrWhiteSpace(login.cpf))
            {
                return Ok("Falta alguns dados do usuário");
            }

            var user = new ApplicationUser
            {
                Email = login.email,
                UserName = login.email,
                CPF = login.cpf
            };

            var result = await _userManager.CreateAsync(user, login.senha);

            if (result.Succeeded)
            {
                // Se a criação do usuário foi bem-sucedida, o ID estará disponível em user.Id
                var userId = user.Id;

                // Geração de confirmação caso precise 
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

                var respose_Retorn = await _userManager.ConfirmEmailAsync(user, code);




                if (respose_Retorn.Succeeded)
                {

                    if (login.IdUsuarioLogado != null)
                    {

                        // Associa o ID do usuário logado ao novo usuário criado
                        await _IUsuarioCreateServico.AdicionarUsuario(
                            new UsuarioCreate
                            {
                                UsuarioCriadoId = userId,
                                UsuarioCriadorId = login.IdUsuarioLogado,
                                DataCadastro = DateTime.UtcNow
                            });
                    }
                    return Ok("Usuário adicionado com sucesso!");
                }
                else
                {
                    return Ok("Erro ao confirmar cadastro de usuário!");
                }
            }
            else
            {
                return Ok(result.Errors);
            }
        }

        [HttpGet("/api/ObterUserID")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetUserIdByEmail(string email)
        {
            // Encontra o usuário com base no email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            // Retorna o ID do usuário encontrado
            return Ok(user);
        }

        [HttpGet("/api/AllByEmail")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetUserAllByEmail(string email)
        {
            // Encontra o usuário com base no email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            // Retorna o ID do usuário encontrado
            return Ok(user);
        }

        [HttpGet("/api/ObterUserVinculados")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmaillinked(string email)
        {
            try
            {
                // Encontra o usuário com base no email
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Obter lista de sistemas vinculados ao usuário
                var usuarioVinculados = await _IUsuarioCreateServico.ListaSistemasUsuario(user.Id);
                if (usuarioVinculados == null || usuarioVinculados.Count == 0)
                {
                    return NotFound("Nenhum sistema vinculado encontrado para este usuário");
                }

                // Lista para armazenar os usuários encontrados
                var usuarios = new List<ApplicationUser>();

                // Para cada usuário encontrado, buscar o usuário completo e adicioná-lo à lista
                foreach (var usuario in usuarioVinculados)
                {
                    var usuarioCompleto = await _userManager.FindByIdAsync(usuario.UsuarioCriadoId);
                    if (usuarioCompleto != null)
                    {
                        usuarios.Add(usuarioCompleto); // Adiciona o usuário completo à lista
                    }
                }

                // Retorna a lista de usuários encontrados
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter usuários vinculados: {ex.Message}");
            }
        }

        [HttpGet("/api/GetUserid")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetUserid(string IdUser)
        {
            try
            {
                // Encontra o usuário com base no email
                var user = await _userManager.FindByIdAsync(IdUser);
                if (user == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Retorna a lista de usuários encontrados
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter usuários vinculados: {ex.Message}");
            }
        }

        [HttpGet("/api/getIDUserSistemasVinculados")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetIDUserByEmaillinked(string email)
        {
            try
            {
                // Encontra o usuário com base no email
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Obter lista de sistemas vinculados ao usuário
                var usuarioVinculados = await _IUsuarioCreateServico.ListaSistemasUsuario(user.Id);
                if (usuarioVinculados == null || usuarioVinculados.Count == 0)
                {
                    return NotFound("Nenhum sistema vinculado encontrado para este usuário");
                }

                // Lista para armazenar os usuários encontrados
                var usuarios = new List<ApplicationUser>();

                // Para cada usuário encontrado, buscar o usuário completo e adicioná-lo à lista
                foreach (var usuario in usuarioVinculados)
                {
                    var usuarioCompleto = await _userManager.FindByIdAsync(usuario.UsuarioCriadoId);
                    if (usuarioCompleto != null)
                    {
                        usuarios.Add(usuarioCompleto); // Adiciona o usuário completo à lista
                    }
                }

                // Retorna a lista de usuários encontrados
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter usuários vinculados: {ex.Message}");
            }

        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<ApplicationUser>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
       

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno do email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var param = new Dictionary<string, string?>
            {
                    {"token", code },
                    {"email", user.Email }
                };

           // var respose_Retorn = await _userManager.ConfirmEmailAsync(user, code);

            var callback = QueryHelpers.AddQueryString(userForRegistration.ClientURI, param);

            var message = new FinanceiroSolution.Domain.Servicos.EmailService.Message(
                new string[] { user.Email }, "Email Confirmation token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return Ok(new Resposta(200, "Criado com sucesso!"));
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            return Ok();
        }

        [HttpGet("EmailConfirmation")]
     
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");

            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            return Ok();
        }
    }
}

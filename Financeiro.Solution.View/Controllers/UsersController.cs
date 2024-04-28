using Financeiro.Solution.View.Models;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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




        private readonly InterfaceUsuarioCreate _InterfaceUsuarioCreate;
        private readonly IUsuarioCreateServico _IUsuarioCreateServico;
        private readonly ILogger<CategoriaController> _logger;

 
        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, InterfaceUsuarioCreate interfaceUsuarioCreate, IUsuarioCreateServico IUsuarioCreateServico)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _InterfaceUsuarioCreate = interfaceUsuarioCreate;
            _IUsuarioCreateServico = IUsuarioCreateServico;
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

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuarioCreate")]
        public async Task<IActionResult> AdicionaUsuarioCreate([FromBody] Login login, string idUsuario)
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
                                DataCriacao = DateTime.UtcNow
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
            return Ok(user.Id);
        }
    }
}

using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.Solution.View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioSistemaFinanceiroController : ControllerBase
    {
        private readonly InterfaceUserSistemaFinanceiro _InterfaceUserSistemaFinanceiro;
        private readonly IUsuarioSistemaFinanceiroServico _IUsuarioSistemasFinanceiroServico;

        public UsuarioSistemaFinanceiroController(InterfaceUserSistemaFinanceiro interfaceUserSistemaFinanceiro, IUsuarioSistemaFinanceiroServico iUsuarioSistemasFinanceiroServico)
        {
            _InterfaceUserSistemaFinanceiro = interfaceUserSistemaFinanceiro;
            _IUsuarioSistemasFinanceiroServico = iUsuarioSistemasFinanceiroServico;
        }

        [HttpGet("/api/ListaSistemasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListaSistemasUsuario(int IdSistema)
        {
            return await _InterfaceUserSistemaFinanceiro.ListarUsuariosSistema(IdSistema);
        }

        [HttpPost("/api/CadastrarUsuarioNoSistema")]
        [Produces("application/json")]
        public async Task<object> CadastrarUsuarioNoSistema(int IdSistema, string emailUsuario)
        {
            try
            {
                await _IUsuarioSistemasFinanceiroServico.CadastrarUsuarioNoSistema(
                new UsuarioSistemaFinanceiro
                {
                    IdSistema = IdSistema,
                    EmailUsuario = emailUsuario,
                    Administrador = false,
                    SistemaAtual = true
                });
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        [HttpPost("/api/CadastrarUsuarioListaSistemas")]
        [Produces("application/json")]
        public async Task<object> CadastrarUsuarioListaSistemas(int[] idsSistemas, string emailUsuario)
        {
            try
            {
                var usuarioSistemas = new List<UsuarioSistemaFinanceiro>();

                // Para cada ID de sistema na lista, criar um objeto UsuarioSistemaFinanceiro
                foreach (var idSistema in idsSistemas)
                {
                    var usuarioSistema = new UsuarioSistemaFinanceiro
                    {
                        IdSistema = idSistema,
                        EmailUsuario = emailUsuario,
                        Administrador = false, // Você pode definir como necessário
                        SistemaAtual = true // Você pode definir como necessário
                    };

                    usuarioSistemas.Add(usuarioSistema);
                }

                // Chamar o método da camada de serviço para adicionar a lista de sistemas do usuário
                await _IUsuarioSistemasFinanceiroServico.AdicionarListaSistemasUsuario(usuarioSistemas);

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        [HttpDelete("/api/DeletarUsuarioNoSistema")]
        [Produces("application/json")]
        public async Task<object> DeletarUsuarioNoSistema(int Id)
        {
            try
            {
                var usuarioSistemaFinanceiro = await _InterfaceUserSistemaFinanceiro.GetById(Id);
                await _InterfaceUserSistemaFinanceiro.Delete(usuarioSistemaFinanceiro);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}



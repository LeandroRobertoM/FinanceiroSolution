using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace Financeiro.Solution.View.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly InterfaceCategoria _InterfaceCategoria;
        private readonly ICategoriaServico _ICategoriaServico;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(InterfaceCategoria InterfaceCategoria, ICategoriaServico ICategoriaServico, ILogger<CategoriaController> logger)
        {
            _InterfaceCategoria = InterfaceCategoria;
            _ICategoriaServico = ICategoriaServico;
            _logger = logger;
        }

        [HttpGet("/api/ListarCategoriasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarCategoriasUsuario(string emailUsuario)
        {
            try
            {
                _logger.LogInformation("Executando o método ListarCategoriasUsuario");

                var result = await _InterfaceCategoria.ListarCategoriasUsuario(emailUsuario);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao executar o método ListarCategoriasUsuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor");
            }
        }

        [HttpPost("/api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Categoria categoria)
        {
            try
            {
                _logger.LogInformation("Executando o método AdicionarCategoria");

                await _ICategoriaServico.AdicionarCategoria(categoria);

                return categoria;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao executar o método AdicionarCategoria");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor");
            }
        }
    }
}
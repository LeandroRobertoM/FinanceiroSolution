using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Financeiro.Solution.View.Models;

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
                _logger.LogInformation("Executando o método Listar Categoria por Conta de Email:{emailUsuario}",emailUsuario);

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
        public async Task<object> AdicionarCategoria(CategoriaViewModel categoriaViewModel)
        {
            try
            {
                _logger.LogInformation("Processo de pegar a variavel do ID tipo sistema{SistemaID}", categoriaViewModel.IdSistemaFinanceiro);
                _logger.LogInformation("Envelope dos campos: Nome: {Nome}, Descrição: {SistemaID}", categoriaViewModel.Nome);
                _logger.LogInformation("Envelope processado: {Envelope}", JsonConvert.SerializeObject(categoriaViewModel));

                Categoria Novacategoria = new Categoria
                {
                    Nome = categoriaViewModel.Nome,
                    IdSistema = categoriaViewModel.IdSistemaFinanceiro
                };

                Novacategoria.SistemaFinanceiro = null;

                _logger.LogInformation("Depois de alterar Novacategoria: {Envelope}", JsonConvert.SerializeObject(Novacategoria));

                await _ICategoriaServico.AdicionarCategoria(Novacategoria);

                

                return Novacategoria;

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao executar o método AdicionarCategoria");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor");
            }
        }
    }
}
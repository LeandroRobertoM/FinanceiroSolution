using Financeiro.Solution.View.Models;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Financeiro.Solution.View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly InterfaceDespesa _InterfaceDespesa;
        private readonly IDespesaServico _IDespesaService;
        private readonly ILogger<DespesaController> _logger;

        public DespesaController(InterfaceDespesa InterfaceDespesa, IDespesaServico IDespesaServico,
            ILogger<DespesaController> logger)
        {
            _InterfaceDespesa = InterfaceDespesa;
            _IDespesaService = IDespesaServico;
            _logger = logger;
        }

        [HttpGet("/api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string emailUsuario)
        {
            return _InterfaceDespesa.ListarDespesasUsuario(emailUsuario);
        }



        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(DespesaViewModel despesaViewModel)
        {
            
            _logger.LogInformation("Envelope processado: {Envelope}", JsonConvert.SerializeObject(despesaViewModel));

            Despesa Novadespesa = new Despesa
            { 
                IdUser= despesaViewModel.IdUser,
                Nome = despesaViewModel.Nome,
                Valor = despesaViewModel.Valor,
                Mes = despesaViewModel.Mes,
                Ano = despesaViewModel.Ano,
                TipoDespesa= despesaViewModel.TipoDespesa,
                DataCadastro= despesaViewModel.DataCadastro,
                DataAlteracao= despesaViewModel.DataAlteracao,
                DataPagamento= despesaViewModel.DataPagamento,
                DataVencimento= despesaViewModel.DataVencimento,
                Pago= despesaViewModel.Pago,
                DespesaAtrasada= despesaViewModel.DespesaAtrasada,
                categoriaId = despesaViewModel.Categoria.IdCategoria
            };

            try
            {
                
                _logger.LogInformation("Depois de alterar Novacategoria: {Envelope}", JsonConvert.SerializeObject(Novadespesa));
                bool operacaoSucesso = await _IDespesaService.AdicionarDespesa(Novadespesa);

                if (operacaoSucesso)
                {
                    return Ok(new Resposta(200, "Criado com sucesso!"));
                }
                else
                {
                    return StatusCode(500, new Resposta(500, "Falha ao adicionar a categoria."));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro: " + ex.Message);
                return StatusCode(500, new Resposta(500, ex.Message));
            }
         
        }


        [HttpPut("/api/AtualizarDespesa")]
        [Produces("application/json")]
        public async Task<object> AtualizarDespesa(Despesa despesa)
        {
            await _IDespesaService.AtualizarDespesa(despesa);

            return Task.FromResult(despesa);
        }


        [HttpGet("/api/ObterDespesa")]
        [Produces("application/json")]
        public async Task<object> ObterDespesa(int id)
        {
            return await _InterfaceDespesa.GetById(id);
        }

        [HttpDelete("/api/DeletarDespesa")]
        [Produces("application/json")]
        public async Task<object> DeletarDespesa(int id)
        {
            try
            {
                var despesa = await _InterfaceDespesa.GetById(id);
                await _InterfaceDespesa.Delete(despesa);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        [HttpPut("/api/CarregaGraficos")]
        [Produces("application/json")]
        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            return await _IDespesaService.CarregaGraficos(emailUsuario);
        }
    }
}

using Financeiro.Solution.View.DTO;
using Financeiro.Solution.View.Models;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IPagamento;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Financeiro.Solution.View.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly InterfacePagamento _InterfacePagamento;
        private readonly IPagamentoServico _IPagamentoServico;
        private readonly ILogger<PagamentoController> _logger;

        public PagamentoController(InterfacePagamento InterfacePagamento, IPagamentoServico IPagamentoServico,
            ILogger<PagamentoController> logger)
        {
            _InterfacePagamento = InterfacePagamento;
            _IPagamentoServico = IPagamentoServico;
            _logger = logger;
        }

     

        [HttpPost("/api/AdicionarPagamento")]
        [Produces("application/json")]
        public async Task<object> AdicionarPagamento(PagamentoDTO pagamento)
        {
           
            Pagamento NovaPagamento = new Pagamento
            {
                Nome = pagamento.Nome,
                Valor = pagamento.Valor,
                TipoPagamento = pagamento.TipoPagamento,
                DataCadastro=pagamento.DataCadastro,
                DataPagamento=pagamento.DataPagamento,
                DespesaAtrasada=pagamento.DespesaAtrasada,
                IdDespesa =  pagamento.IdDespesa,

            };

            try
            {
               
                _logger.LogInformation("Depois de alterar Pagamento: {Envelope}", JsonConvert.SerializeObject(NovaPagamento));
                bool operacaoSucesso = await _IPagamentoServico.AdicionarPagamento(NovaPagamento);

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
    }
}

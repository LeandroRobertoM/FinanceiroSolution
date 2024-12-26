using Financeiro.Solution.View.DTO;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.Solution.View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class DespesaController : ControllerBase
    {
        private readonly InterfaceDespesa _InterfaceDespesa;
        private readonly IDespesaServico _IDespesaService;
        private readonly IDespesaRecorrenteServico _IDespesaRecorrenteService;
        private readonly ILogger<DespesaController> _logger;

        public DespesaController(InterfaceDespesa InterfaceDespesa, IDespesaServico IDespesaServico,IDespesaRecorrenteServico IDespesaRecorrente, ILogger<DespesaController> logger)
        {
            _InterfaceDespesa = InterfaceDespesa;
            _IDespesaService = IDespesaServico;
            _IDespesaRecorrenteService = IDespesaRecorrente;
            _logger = logger;
        }

        [HttpGet("/api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string emailUsuario)
        {
            try
            {
                _logger.LogInformation("Executados Listar erros para aparecer azure Despesa por Conta de Email:{emailUsuario}", emailUsuario);

                var result = await _InterfaceDespesa.ListarDespesasUsuario(emailUsuario);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao executar o método ListarDespesasUsuario TESTESDADA");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor");
            }
        }



        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(DespesaDTO despesaDTO)
        {
            var despesa = new Despesa
            {
                Nome = despesaDTO.Nome,
                Valor = despesaDTO.Valor,
                Mes = despesaDTO.Mes,
                Ano = despesaDTO.Ano,
                TipoDespesa = despesaDTO.TipoDespesa,
                DataVencimento = despesaDTO.DataVencimento,
                Pago = despesaDTO.Pago,
                categoriaId = despesaDTO.categoriaId

            };

            await _IDespesaService.AdicionarDespesa(despesa);

            return despesa;
        }
        [HttpPost("/api/AdicionarDespesaRecorrente")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesaRecorrente(AdicionarDespesaRecorrenteDTO adicionarDespesaRecorrenteDTO) 
        {
            try
            {
            
                var despesa = new Despesa
                {
                    Nome = adicionarDespesaRecorrenteDTO.Despesa.Nome,
                    Valor = adicionarDespesaRecorrenteDTO.Despesa.Valor,
                    Mes = adicionarDespesaRecorrenteDTO.Despesa.Mes,
                    Ano = adicionarDespesaRecorrenteDTO.Despesa.Ano,
                    TipoDespesa = adicionarDespesaRecorrenteDTO.Despesa.TipoDespesa,
                    DataVencimento = adicionarDespesaRecorrenteDTO.Despesa.DataVencimento,
                    Pago = adicionarDespesaRecorrenteDTO.Despesa.Pago,
                    categoriaId = adicionarDespesaRecorrenteDTO.Despesa.categoriaId
                };

                var resultadoDespesa = await _IDespesaService.AdicionarDespesa(despesa);

                if (resultadoDespesa == false)
                {
                    return StatusCode(500, new Resposta(500, "Erro ao adicionar a despesa."));
                }

             
                if (adicionarDespesaRecorrenteDTO.DespesaRecorrente != null)
                {
                    var despesaRecorrencia = new DespesaRecorrencia
                    {
                        DespesaId = despesa.Id,
                        ValorRecorrente = adicionarDespesaRecorrenteDTO.DespesaRecorrente.ValorRecorrente,
                        DataVencimento = adicionarDespesaRecorrenteDTO.DespesaRecorrente.DataVencimento,
                        NumeroParcelas = adicionarDespesaRecorrenteDTO.DespesaRecorrente.NumeroParcelas,
                        Status = adicionarDespesaRecorrenteDTO.DespesaRecorrente.Status
                    };

                    var operacaoSucesso = await _IDespesaRecorrenteService.AdicionarDespesaRecorrente(despesaRecorrencia);

                    if (!operacaoSucesso)
                    {
                        return StatusCode(500, new Resposta(500, "Falha ao criar Despesas Recorrentes."));
                    }
                }

      
                return Ok(new Resposta(200, "Despesa e Despesa Recorrente criadas com sucesso!"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro: " + ex.Message);
                return StatusCode(500, new Resposta(500, "Erro interno no servidor."));
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


        [HttpGet("/api/CarregaGraficos")]
        [Produces("application/json")]
        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            return await _IDespesaService.CarregaGraficos(emailUsuario);
        }
    }
}

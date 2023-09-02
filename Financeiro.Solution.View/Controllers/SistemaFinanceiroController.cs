using Financeiro.Solution.View.Models;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Financeiro.Solution.View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SistemaFinanceiroController : ControllerBase
    {
        private readonly InterfaceSistemaFinanceiro _InterfacesistemaFinanceiro;
        private readonly ISistemaFinanceiroServico _ISistemaFinanceiroServico;
        private readonly ILogger<SistemaFinanceiroController> _logger;

        public SistemaFinanceiroController(InterfaceSistemaFinanceiro InterfaceSistemaFinanceiro,
            ISistemaFinanceiroServico ISistemaFinanceiroService, ILogger<SistemaFinanceiroController> logger)
        {
            _InterfacesistemaFinanceiro = InterfaceSistemaFinanceiro;
            _ISistemaFinanceiroServico = ISistemaFinanceiroService;
            _logger = logger;
        }


        [HttpGet("/api/ListaSistemaUsuario")]
        [Produces("application/json")]
        public async Task<object> ListaSistemaUsuario(string emailUsuario)
        {
            return await _InterfacesistemaFinanceiro.ListaSistemasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AdicionarSistemaFinanceiro(SistemaFinanceiroViewModel sistemaFinanceiroViewModel)
        {
            _logger.LogInformation("Processo de pegar a variavel do ID tipo sistema{SistemaID}", sistemaFinanceiroViewModel.Id);
            _logger.LogInformation("Envelope dos campos: Nome: {Nome}, Descrição: {SistemaID}", sistemaFinanceiroViewModel.Nome);
            _logger.LogInformation("Envelope processado: {Envelope}", JsonConvert.SerializeObject(sistemaFinanceiroViewModel));

            SistemaFinanceiro NovosistemaFinanceiro = new SistemaFinanceiro
            {
                Nome = sistemaFinanceiroViewModel.Nome,
                Mes = sistemaFinanceiroViewModel.Mes,
                Ano = sistemaFinanceiroViewModel.Ano,
                DiaFechamento = sistemaFinanceiroViewModel.DiaFechamento,
                GerarCopiaDespesa = sistemaFinanceiroViewModel.GerarCopiaDespesa,
                MesCopia = sistemaFinanceiroViewModel.MesCopia,
                AnoCopia = sistemaFinanceiroViewModel.AnoCopia

            };


          try
            {

                _logger.LogInformation("Depois de alterar Novacategoria: {Envelope}", JsonConvert.SerializeObject(NovosistemaFinanceiro));
                bool operacaoSucesso = await _ISistemaFinanceiroServico.AdicionarSistemaFinanceiro(NovosistemaFinanceiro);

                if (operacaoSucesso)
                {
                    return Ok(new Resposta(200, "Criado com sucesso! o sistema Financeiro"));
                }
                else
                {
                    return StatusCode(500, new Resposta(500, "Falha ao adicionar a Sistema Financeiro."));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro: " + ex.Message);
                return StatusCode(500, new Resposta(500, ex.Message));
            }

        }

        [HttpPut("/api/AtualizarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _ISistemaFinanceiroServico.AtualizarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
        }

        [HttpPut("/api/ObterSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> ObterSistemaFinanceiro(int id)
        {
            return await _InterfacesistemaFinanceiro.GetById(id);
        }

        [HttpDelete("/api/DeletarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeletarSistemaFinanceiro(int id)
        {
            try
            {
                var sistemaFinanceiro = await _InterfacesistemaFinanceiro.GetById(id);
                await _InterfacesistemaFinanceiro.Delete(sistemaFinanceiro);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
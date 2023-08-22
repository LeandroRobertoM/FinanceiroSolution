using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.Solution.View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SistemaFinanceiroController : ControllerBase
    {
        private readonly InterfaceSistemaFinanceiro _InterfacesistemaFinanceiro;
        private readonly ISistemaFinanceiroServico _ISistemaFinanceiroServico;

        public SistemaFinanceiroController(InterfaceSistemaFinanceiro InterfaceSistemaFinanceiro, ISistemaFinanceiroServico ISistemaFinanceiroService)
        {
            _InterfacesistemaFinanceiro = InterfaceSistemaFinanceiro;
            _ISistemaFinanceiroServico = ISistemaFinanceiroService;
        }


        [HttpGet("/api/ListaSistemaUsuario")]
        [Produces("application/json")]
        public async Task<object> ListaSistemaUsuario(string emailUsuario)
        {
            return await _InterfacesistemaFinanceiro.ListaSistemasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
           //Ajustar Log

            await _ISistemaFinanceiroServico.AdicionarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
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
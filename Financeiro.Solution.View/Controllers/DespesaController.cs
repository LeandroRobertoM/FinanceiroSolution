using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.Solution.View.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly InterfaceDespesa _InterfaceDespesa;
        private readonly IDespesaServico _IDespesaService;

        public DespesaController(InterfaceDespesa InterfaceDespesa, IDespesaServico IDespesaServico)
        {
            _InterfaceDespesa = InterfaceDespesa;
            _IDespesaService = IDespesaServico;

        }

        [HttpGet("/api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string emailUsuario)
        {
            return _InterfaceDespesa.ListarDespesasUsuario(emailUsuario);
        }



        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(Despesa despesa)
        {
            await _IDespesaService.AdicionarDespesa(despesa);

            return despesa;
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

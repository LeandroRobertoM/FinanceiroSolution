using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class SistemaFinanceiroServico : ISistemaFinanceiroServico
    {
        private readonly InterfaceSistemaFinanceiro _interfaceSistemaFinanceiro;

        public SistemaFinanceiroServico(InterfaceSistemaFinanceiro interfaceSistemaFinanceiro)
        {
            _interfaceSistemaFinanceiro = interfaceSistemaFinanceiro;
        }

        public async Task AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.validarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (valido) 
            {
                var data = DateTime.Now;
                sistemaFinanceiro.DiaFechamento = 1;
                sistemaFinanceiro.Ano = data.Year;
                sistemaFinanceiro.Mes = data.Month;
                sistemaFinanceiro.AnoCopia= data.Year;
                sistemaFinanceiro.MesCopia=data.Month;
                sistemaFinanceiro.GerarCopiaDespesa = true;
                await _interfaceSistemaFinanceiro.Add(sistemaFinanceiro);
            }
        }

        public Task AtualizarDespesa(SistemaFinanceiro sistemaFinanceiro)
        {
            throw new NotImplementedException();
        }

        public async Task AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.validarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (valido)
            {
                sistemaFinanceiro.DiaFechamento = 1;
                await _interfaceSistemaFinanceiro.Update(sistemaFinanceiro);
            }
        }

        public Task<object> CarregaGraficos(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}

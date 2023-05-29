using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class DespesaServico : IDespesaServico
    {
        private readonly InterfaceDespesa _interfaceDespesa;

        public DespesaServico(InterfaceDespesa interfaceDespesa)
        {
            _interfaceDespesa=interfaceDespesa;
        }

        public async Task AdicionarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataCadastro = data;
            despesa.Ano = data.Year;
            despesa.Mes = data.Month;
            var valido = despesa.validarPropriedadeString(despesa.Nome, "Nome");
            if (valido)
                await _interfaceDespesa.Add(despesa);
        }

        public async Task AtualizarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataAlteracao = data;

            if (despesa.Pago) 
            {
                despesa.DataPagamento = data;
            }
            var valido = despesa.validarPropriedadeString(despesa.Nome, "Nome");
            if (valido)
                await _interfaceDespesa.Update(despesa);
        }

        public Task<object> CarregaGraficos(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}

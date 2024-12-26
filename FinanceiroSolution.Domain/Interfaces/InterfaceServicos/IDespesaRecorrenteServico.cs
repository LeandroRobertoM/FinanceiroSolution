using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.InterfaceServicos
{
    public interface IDespesaRecorrenteServico
    {
            Task<bool> AdicionarDespesaRecorrente(DespesaRecorrencia despesaRecorrencia);
            Task AtualizarDespesaRecorrente(DespesaRecorrencia despesaRecorrencia);
            Task<object> CarregaGraficos(string emailUsuario);
    }
}

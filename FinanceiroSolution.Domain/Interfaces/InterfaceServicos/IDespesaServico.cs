using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.Servicos
{
    public interface IDespesaServico
    {
        Task<bool> AdicionarDespesa(Despesa despesa);
        Task AtualizarDespesa(Despesa despesa);
        Task<object> CarregaGraficos(string emailUsuario);
    }
}

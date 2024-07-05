using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.InterfaceServicos
{
    public interface IPagamentoServico
    {
        Task<bool> AdicionarPagamento(Pagamento pagamento);
        Task AtualizarPagamento(Pagamento pagamento);
        Task<object> CarregaGraficos(string emailUsuario);
    }
   
}

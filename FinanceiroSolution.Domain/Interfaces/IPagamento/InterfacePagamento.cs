using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Generics;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.IPagamento
{
    public interface InterfacePagamento : InterfaceGeneric<Pagamento>
    {
        Task<IResposta<bool>> AdicionarPagamento(Pagamento pagamento);
        Task<IList<Despesa>> ListarPagamentosUsuario(string emailUsuario);

        Task<IList<Despesa>> ListarPagamentosNaoPagasMesesAnterior(string emailUsuario);

    }
}

using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Generics;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.IDespesa.IDespesaRecorrente
{
    public interface InterfaceDespesaRecorrente : InterfaceGeneric<DespesaRecorrencia>
    {
        Task<IResposta<bool>> AdicionarDespesaRecorrente(DespesaRecorrencia despesaRecorrencia);
        Task<IList<DespesaRecorrencia>> ListarDespesasUsuario(string emailUsuario);

        Task<IList<DespesaRecorrencia>> ListarDespesasNaoPagasMesesAnterior(string emailUsuario);

    }
}

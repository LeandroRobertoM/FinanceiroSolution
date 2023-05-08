using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.IDespesa
{
    public interface IDespesaRepository 
    {
        void CadastrarDespesa(Despesa despesa);
        void EditarDespesa(Despesa despesa);
        List<Despesa> BuscarTodos();
        Despesa BuscarPorID(int idDespesa);
        Despesa BuscarNome(string Nome);
        void DeletarDespesa(int idDespesa);
    }
}

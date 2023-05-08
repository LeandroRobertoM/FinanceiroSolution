using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.Generics
{
    public interface InterfaceGeneric<T> where T : class
    {
        Task Adicionar(T Objeto);
        Task Editar(T Objeto);
        Task Excluir(T Objeto);
        Task<T> BuscarPorID(int Id);
        Task<List<T>> BuscarTodos();
    }
}

using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Generics
{
    public interface InterfaceGeneric<T> where T : class
    {
        Task<IResposta<bool>> Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetById(int Id);
        Task<List<T>> GetAll();
    }
}

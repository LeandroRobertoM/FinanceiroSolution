using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Generics;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.ICategoria
{
    public interface InterfaceCategoria : InterfaceGeneric<Categoria>
    {
        Task<IList<Categoria>> ListarCategoriasUsuario(string emailUsuario);
        Task<IResposta<bool>> Adicionar(Categoria categoria);
    }
}

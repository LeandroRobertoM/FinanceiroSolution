using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.Servicos
{
   public interface ICategoriaServico
    {
        Task<bool> AdicionarCategoria(Categoria categoria);
        Task AtualizarCategoria(Categoria categoria);


    }
}


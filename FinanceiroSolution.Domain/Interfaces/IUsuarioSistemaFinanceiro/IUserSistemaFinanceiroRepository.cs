using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro
{
    public interface IUserSistemaFinanceiroRepository
    {
        void CadastrarUserFinanceiro(UsuarioSistemaFinanceiro userSistemaFinanceiro);
        void EditarUserFinanceiro(UsuarioSistemaFinanceiro userSistemaFinanceiro);
        List<UsuarioSistemaFinanceiro> BuscarTodos();
        Categoria BuscarPorID(int idCategoria);
        Categoria BuscarNome(string Nome);
        void DeletarUserSistemaFinanceiro(int idCategoria);

    }
}

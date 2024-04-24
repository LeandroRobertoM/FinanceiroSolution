using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Generics;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro
{
    public interface InterfaceUserSistemaFinanceiro : InterfaceGeneric<UsuarioSistemaFinanceiro>
    {

        Task<IResposta<bool>> AdicionarListaSistemaFinanceiro(List<UsuarioSistemaFinanceiro> usuarioSistemaFinanceiro);
        Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistema(int IdSistema);
        Task<IList<UsuarioSistemaFinanceiro>> ObterUsuarioPorEmail(string emailUsuario);
        Task RemoveUsuarios(List<UsuarioSistemaFinanceiro> usuarios); 
    

    }
}

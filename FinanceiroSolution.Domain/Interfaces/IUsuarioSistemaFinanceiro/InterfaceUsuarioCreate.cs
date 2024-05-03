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
    public interface InterfaceUsuarioCreate : InterfaceGeneric<UsuarioCreate>
    {
        Task<IResposta<bool>> Adicionar(UsuarioCreate usuarioCreateUsuario);

        Task<IList<UsuarioCreate>> ListaSistemasUsuario(string usuarioCriadorId);
    }
}

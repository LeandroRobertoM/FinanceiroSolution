using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Generics;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.IApplicationUser
{


    public interface InterfaceApplicationUser : InterfaceGeneric<ApplicationUser>
    {
        Task<IList<ApplicationUser>> ListarUsuarioCpf(string CpfUsuario);

        Task<IList<ApplicationUser>> ListarUsuarioEmail(string EmailUsuario);

    }
}

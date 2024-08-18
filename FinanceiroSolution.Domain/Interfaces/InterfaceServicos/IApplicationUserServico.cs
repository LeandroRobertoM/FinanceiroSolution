using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.InterfaceServicos
{
    public interface IApplicationUserServico
    {
        Task<IList<ApplicationUser>> ListarUsuarioCpf(string CpfUsuario);

        Task<IList<ApplicationUser>> ListarUsuarioEmail(string EmailUsuario);
    }
}

using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.InterfaceServicos
{
    public interface ISistemaFinanceiroServico
    {
        Task<bool>  AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        Task<bool> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        Task<object> CarregaGraficos(string emailUsuario);
    }
}

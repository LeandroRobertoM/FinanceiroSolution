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
        Task AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        Task AtualizarDespesa(SistemaFinanceiro sistemaFinanceiro);
        Task<object> CarregaGraficos(string emailUsuario);
    }
}

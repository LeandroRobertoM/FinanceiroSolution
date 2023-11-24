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
        /// <summary>
        /// Estou verificando se retornamos o objeto inteiro. 
        /// </summary>
        /// <param name="sistemaFinanceiro"></param>
        /// <returns></returns>
        Task<(bool success, int IdSistemaFinanceiro, SistemaFinanceiro sistemaFianceiroObject)> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        Task<bool> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        Task<object> CarregaGraficos(string emailUsuario);
    }
}

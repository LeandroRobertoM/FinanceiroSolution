using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Generics;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro
{
    public interface InterfaceSistemaFinanceiro : InterfaceGeneric<SistemaFinanceiro>
    {
        /// <summary>
        /// Estou verificando se retornamos o objeto inteiro. 
        /// </summary>
        /// <param name="sistemaFinanceiro"></param>
        /// <returns></returns>
        Task<IResposta<(bool success, int IdSistemaFinanceiro, SistemaFinanceiro sistemaFianceiroObject)>> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);

        Task<IList<SistemaFinanceiro>> ListaSistemasUsuario(string emailUsuario);
    }
}

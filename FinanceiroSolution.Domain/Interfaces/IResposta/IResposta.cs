using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.IResposta
{
   public interface IResposta<T>
{
    bool OperacaoSucesso { get; set; }
    string MensagemErro { get; set; }
    T Dados { get; set; }
}
}

using FinanceiroSolution.Domain.Interfaces.IResposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Response
{
    public class Resposta<T> : IResposta<T>
    {
        public bool OperacaoSucesso { get; set; }
        public string MensagemErro { get; set; }
        public T Dados { get; set; }

        public Resposta(bool operacaoSucesso, string mensagemErro)
        {
            OperacaoSucesso = operacaoSucesso;
            MensagemErro = mensagemErro;
        }
    }
}

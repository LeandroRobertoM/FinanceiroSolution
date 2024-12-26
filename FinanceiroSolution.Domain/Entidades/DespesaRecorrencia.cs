using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class DespesaRecorrencia : Base
    {

        public int IdRecorrencia { get; set; }
        public int DespesaId { get; set; }
        public Despesa Despesa { get; set; }
        public decimal ValorRecorrente { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Frequencia { get; set; }     
        public int NumeroParcelas { get; set; }
        public bool Notificacao {get; set; }
        public string Status { get; set; }


        public DespesaRecorrencia(int despesaId,decimal valorRecorrente,DateTime dataVencimento, DateTime dataCriacao, string frequencia, int numeroParcelas, bool notificacao,string status) 
        {
            DespesaId = despesaId;
            ValorRecorrente = valorRecorrente;
            DataVencimento = dataVencimento;
            DataCriacao = dataCriacao;
            Frequencia = frequencia;
            NumeroParcelas = numeroParcelas;
            Notificacao = notificacao;
            Status = status;
        }

        public DespesaRecorrencia() { }


    }

}

using FinanceiroSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class Pagamento
    {
        public int IdPagamento { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public EnumTipoPagamento TipoPagamento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool DespesaAtrasada { get; set; }
        public Despesa Despesa { get; set;}

        public int IdDespesa{ get; set; }

        public Pagamento(int idPagamento, string nome, decimal valor, EnumTipoPagamento tipoPagamento, DateTime dataCadastro, DateTime dataPagamento, bool despesaAtrasada,Despesa despesa)
        {
            IdPagamento = idPagamento;
            Nome = nome;
            Valor = valor;
            TipoPagamento = tipoPagamento;
            DataCadastro = dataCadastro;
            DataPagamento = dataPagamento;
            DespesaAtrasada = despesaAtrasada;
            IdDespesa = despesa.IdDespesa;
        }


        public Pagamento() { }
    }

}

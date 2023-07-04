using FinanceiroSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class Despesa : Base
    {
        public int IdUser { get; set; }

        public int IdDespesa { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public EnumTipoDespesa TipoDespesa { get; set; }
        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public bool DespesaAtrasada { get; set; }
        public Categoria Categoria { get; set; }
        public Despesa(Categoria categoria, int idUser, string nome, decimal valor, int mes, int ano, EnumTipoDespesa tipoDespesa, DateTime dataCadastro,
                       DateTime DataPagamento, DateTime DataVencimento, bool pago, bool despesaAtrasada)
        {
            this.Categoria = categoria;
            this.IdUser = idUser;
            this.Nome = nome;
            this.Valor = valor;
            this.Mes = mes;
            this.Ano = ano;
            this.TipoDespesa = tipoDespesa;
            this.DataCadastro = dataCadastro;
            this.DataPagamento = DataPagamento;
            this.DataVencimento = DataVencimento;
            this.Pago = pago;
            this.DespesaAtrasada = despesaAtrasada;


        }
        public Despesa() { }



    }
}
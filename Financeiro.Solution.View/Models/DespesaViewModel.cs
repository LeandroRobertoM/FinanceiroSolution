using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Enums;

namespace Financeiro.Solution.View.Models
{
    public class DespesaViewModel
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
        public int categoriaId { get; set; }
    }
}

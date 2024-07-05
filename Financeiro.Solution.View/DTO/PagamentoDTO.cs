using FinanceiroSolution.Domain.Enums;

namespace Financeiro.Solution.View.DTO
{
    public class PagamentoDTO
    {

        /// <summary>
        /// Esta classe foi criada para melhorar o retorno da Api Vou replicar para demais classe.
        /// </summary>

        public int IdPagamento { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public EnumTipoPagamento TipoPagamento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool DespesaAtrasada { get; set; }
        public int IdDespesa { get; set; }
    }
}

using FinanceiroSolution.Domain.Enums;

namespace Financeiro.Solution.View.DTO
{
    public class DespesaDTO
    {

        /// <summary>
        /// Esta classe foi criada para melhorar o retorno da Api Vou replicar para demais classe.
        /// </summary>

        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public bool DespesaAtrasada { get; set; }
        public EnumTipoDespesa TipoDespesa { get; set; }
        public int categoriaId { get; set; }
    }
}

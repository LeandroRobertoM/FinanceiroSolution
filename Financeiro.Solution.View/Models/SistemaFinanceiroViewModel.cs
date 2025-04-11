namespace Financeiro.Solution.View.Models
{
    public class SistemaFinanceiroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime AnoBase { get; set; }

        public DateTime DataCadastro { get; set; }

        public decimal MetaAnual { get; set; }

        public bool Ativo { get; set; }

    }
}

using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Enums;

namespace Financeiro.Solution.View.DTO
{
    public class DespesaRecorrenteDTO
    {

        public decimal ValorRecorrente { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Frequencia { get; set; } 
        public int NumeroParcelas { get; set; }
        public bool Notificacao { get; set; }
        public string Status { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class SistemaFinanceiro : Base
    {
        public int IdSistemaFinanceiro { get; set; }
        public string Nome { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public int DiaFechamento { get; set; }
        public bool GerarCopiaDespesa { get; set; }
        public int MesCopia { get; set; }
        public int AnoCopia { get; set; }

        public SistemaFinanceiro(int IdSistemaFinanceiro, string nome, int mes, int ano, int diaFechamento,
            bool gerarCopiaDespesa, int mesCopia, int anoCopia)
        {
            this.IdSistemaFinanceiro = IdSistemaFinanceiro;
            this.Nome = nome;
            this.Mes = mes;
            this.Ano = ano;
            this.DiaFechamento = diaFechamento;
            this.GerarCopiaDespesa = gerarCopiaDespesa;
            this.MesCopia = mesCopia;
            this.AnoCopia = anoCopia;

        }
        public SistemaFinanceiro() { }
    }
}

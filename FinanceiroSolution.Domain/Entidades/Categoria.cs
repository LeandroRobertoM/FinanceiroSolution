using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        public string Nome { get; set; }

        public SistemaFinanceiro SistemaFinanceiro { get; set; }

        public Categoria(SistemaFinanceiro sistemaFinanceiro, int IdCategoria, string nome)
        {
            this.SistemaFinanceiro = sistemaFinanceiro;
            this.IdCategoria = IdCategoria;
            this.Nome = nome;
        }

        public Categoria() { }
    }

}


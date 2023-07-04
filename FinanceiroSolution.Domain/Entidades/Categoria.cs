using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class Categoria:Base
    {
        public int IdCategoria { get; set; }

        public string Nome { get; set; }

        public SistemaFinanceiro SistemaFinanceiro { get; set; }

        public int IdSistema { get; set; }

        public Categoria(int IdCategoria, string nome, SistemaFinanceiro sistemaFinanceiro)
        {
            this.SistemaFinanceiro = sistemaFinanceiro;
            this.IdCategoria = IdCategoria;
            this.Nome = nome;
        }

        public Categoria(int IdCategoria, string nome, int IdSistema)
        {
            
            this.IdCategoria = IdCategoria;
            this.Nome = nome;
            this.IdSistema = IdSistema;
        }

        public Categoria() { }
    }

}


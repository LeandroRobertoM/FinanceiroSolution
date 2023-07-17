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

        public Categoria(int idCategoria, string nome, SistemaFinanceiro sistemaFinanceiro)
        {
            if (sistemaFinanceiro == null)
            {
                throw new ArgumentNullException(nameof(sistemaFinanceiro), "SistemaFinanceiro inválido");
            }

            this.SistemaFinanceiro = sistemaFinanceiro;
            this.IdCategoria = idCategoria;
            this.Nome = nome;
            this.IdSistema = sistemaFinanceiro.Id;
        }

        public Categoria(int idCategoria, string nome, int idSistema)
        {
            if (idSistema <= 0)
            {
                throw new ArgumentException("IdSistema inválido", nameof(idSistema));
            }

            this.IdCategoria = idCategoria;
            this.Nome = nome;
            this.IdSistema = idSistema;
        }

        public Categoria() { }
    }

}


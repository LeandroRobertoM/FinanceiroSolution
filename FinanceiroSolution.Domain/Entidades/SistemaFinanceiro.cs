using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class SistemaFinanceiro : Base
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime AnoBase { get; set; }
        public decimal? MetaAnual { get; set; }
        public bool? Ativo { get; set; }
 

    
        public SistemaFinanceiro(int Id, string nome, DateTime dataCadastro, DateTime anoBase, decimal metaAnual,bool ativo) 
        {
            this.Id = Id;
            this.Nome = nome;
            this.DataCadastro = dataCadastro;
            this.AnoBase = anoBase;
            this.MetaAnual = metaAnual;
            this.Ativo = ativo;
        }

        public SistemaFinanceiro() { }
    }
}

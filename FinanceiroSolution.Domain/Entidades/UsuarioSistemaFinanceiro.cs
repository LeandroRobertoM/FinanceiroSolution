using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class UsuarioSistemaFinanceiro
    {
        public int Id { get; set; }

        public string EmailUsuario { get; set; }

        public bool Administrador { get; set; }

        public bool SistemaAtual { get; set; }
        public int IdSistema { get; set; }

        public virtual SistemaFinanceiro SistemaFinanceiro { get; set; }


        public UsuarioSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro, int id, string emailUsuario, bool administrador, bool sistemaAtual)
        {
            this.SistemaFinanceiro = sistemaFinanceiro;
            this.EmailUsuario = emailUsuario;
            this.Id = id;
            this.Administrador = administrador;
            this.SistemaAtual = sistemaAtual;
            this.IdSistema = SistemaFinanceiro.Id;

        }
       public UsuarioSistemaFinanceiro() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Notificacoes
{
    public class Notifica
    {
        public Notifica()
        {
            notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string mensagem { get; set; }

        [NotMapped]
        public List<Notifica> notificacoes;


        public bool validarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {

                notificacoes.Add(new Notifica
                {
                    mensagem = "Campo Obrigatorio",
                    NomePropriedade = nomePropriedade

                });

                return false;

            }
            return true;
        }


        public bool validarPropriedadeInteiro(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {

                notificacoes.Add(new Notifica
                {
                    mensagem = "Campo Obrigatorio",
                    NomePropriedade = nomePropriedade

                });

                return false;

            }
            return true;
        }

    }
}

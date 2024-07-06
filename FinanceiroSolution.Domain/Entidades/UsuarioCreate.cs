using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class UsuarioCreate
    {
        public int Id { get; set; }

        public DateTime DataCadastro { get; set; }

        public string UsuarioCriadorId { get; set; }

        public string UsuarioCriadoId { get; set; }
    }
}

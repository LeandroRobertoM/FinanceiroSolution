using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Entidades
{
    public class ApplicationUser : IdentityUser
    {

        [Column("USR_CPF")]
        public string CPF { get; set; }


        /* public int IdUser { get; set; }
         public string Nome { get; set; }
         public string Cpf { get; set; }

         public ApplicationUser(int idUser, string nome, string cpf)
         {
             IdUser = idUser;
             Nome = nome;
             Cpf = cpf;
         }

         public ApplicationUser() { }*/
    }
}

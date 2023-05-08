using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FinanceiroSolution.Domain
{
    public class Base
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }


    }
}
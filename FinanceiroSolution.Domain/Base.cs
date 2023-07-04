using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FinanceiroSolution.Domain
{
    public class Base
    {
        [JsonIgnore]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [JsonIgnore]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


    }
}
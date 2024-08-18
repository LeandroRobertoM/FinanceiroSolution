namespace Financeiro.Solution.View.Models
{
    public class LoginUserCreate
    {
        public string email { get; set; }
        public string senha { get; set; }
        public string cpf { get; set; }
        public string IdUsuarioLogado { get; set; }

        public string? ClientURI { get; set; }
    }
}

namespace Financeiro.Solution.View.Controllers
{
    public class RespostaCustomDados<T>
    {
        public RespostaCustomDados(int status, string mensagem)
        {
            this.Status = status;
            this.Mensagem = mensagem;

        }

        public RespostaCustomDados(int status, string mensagem, T dados)
        {
            this.Status = status;
            this.Mensagem = mensagem;
            this.Dados = dados;
        }

        public int Status { get; set; }
        public string Mensagem { get; set; }
        public T Dados { get; set; }

    }
}

using FinanceiroSolution.Domain.Entidades;

namespace Financeiro.Solution.Dominio.Tests
{

    public class CategoriaTests
    {
        [Fact]
        public void Categoria_DeveSerCriadaComParametrosValidos()
        {
            // Arrange
            int idCategoria = 1;
            string nome = "Categoria 1";
            int idSistema = 1;

            // Act
            var categoria = new Categoria(idCategoria, nome, idSistema);

            // Assert
            Assert.Equal(idCategoria, categoria.IdCategoria);
            Assert.Equal(nome, categoria.Nome);
            Assert.Equal(idSistema, categoria.IdSistema);
        }

        [Fact]
        public void Categoria_DeveSerCriadaComParametrosValidos_SistemaFinanceiro()
        {
            // Arrange
            int idCategoria = 1;
            string nome = "Categoria 1";
            var sistemaFinanceiro = new SistemaFinanceiro();

            // Act
            var categoria = new Categoria(idCategoria, nome, sistemaFinanceiro);

            // Assert
            Assert.Equal(idCategoria, categoria.IdCategoria);
            Assert.Equal(nome, categoria.Nome);
            Assert.Equal(sistemaFinanceiro, categoria.SistemaFinanceiro);
        }

  
       [Fact]
       public void Categoria_DeveRetornarErroQuandoNomeInvalido()
       {
                  // Arrange
           int idCategoria = 1;
           string nome = string.Empty;
           int idSistema = 1;

                  // Act & Assert
           Assert.Throws<ArgumentException>(() => new Categoria(idCategoria, nome, idSistema));
       }

      [Fact]
      public void Categoria_DeveRetornarErroQuandoIdSistemaInvalido()
      {
                  // Arrange
           int idCategoria = 1;
           string nome = "Categoria 1";
           int idSistema = 0;

                  // Act & Assert
           var exception = Assert.Throws<ArgumentException>(() => new Categoria(idCategoria, nome, idSistema));
          Assert.Equal("Mensagem de erro esperada", exception.Message);
      } 
   }
}


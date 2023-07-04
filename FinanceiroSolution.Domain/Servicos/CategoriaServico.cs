using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class CategoriaServico : ICategoriaServico
    {

        private readonly InterfaceCategoria _interfaceCategoria;
        public CategoriaServico(InterfaceCategoria interfaceCategoria)
        {
            _interfaceCategoria = interfaceCategoria;
        }

        public async Task AdicionarCategoria(Categoria categoria)
        {
            try
            {
                IResposta<bool> resposta = await _interfaceCategoria.Adicionar(categoria);

                if (!resposta.OperacaoSucesso)
                {
                    Console.WriteLine("Falha ao adicionar a categoria: " + resposta.MensagemErro);
                    // Ou utilize sua biblioteca de log preferida para registrar o erro

                    // Trate o erro aqui
                    // ...
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao adicionar a categoria: " + ex.Message);
                // Ou utilize sua biblioteca de log preferida para registrar o erro

                // Trate o erro aqui
                // ...
            }
        }
        public async Task AtualizarCategoria(Categoria catagoria)
        {
            
 
                await _interfaceCategoria.Update(catagoria);
        }
    }
}

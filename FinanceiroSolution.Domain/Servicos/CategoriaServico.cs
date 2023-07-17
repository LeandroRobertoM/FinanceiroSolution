using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
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
        private readonly ILogger<CategoriaServico> _logger;
        public CategoriaServico(InterfaceCategoria interfaceCategoria, ILogger<CategoriaServico> logger)
        {
            _interfaceCategoria = interfaceCategoria;
            _logger = logger;
        }

        public async Task<bool> AdicionarCategoria(Categoria categoria)
        {
            try
            {
                IResposta<bool> resposta = await _interfaceCategoria.Adicionar(categoria);

                if (resposta.OperacaoSucesso == false)
                {
                    Console.WriteLine("Falha ao adicionar a categoria: " + resposta.MensagemErro);
                    _logger.LogInformation("Falha ao adicionar a categoria: " + resposta.MensagemErro);
                    return false;
                }
                else
                {
                    _logger.LogInformation("Categoria adicionada com sucesso!");
                    // Faça algo se a operação for bem-sucedida
                    // ...

                    return true;
                }
            }
            catch (Exception ex)
            {
                
                
                Console.WriteLine("Ocorreu um erro ao adicionar a categoria: " + ex.Message);
                // Ou utilize sua biblioteca de log preferida para registrar o erro

                // Trate o erro aqui
                // ...
                
            }
            return false;

        }
        public async Task AtualizarCategoria(Categoria catagoria)
        {
            
 
                await _interfaceCategoria.Update(catagoria);
        }
    }
}

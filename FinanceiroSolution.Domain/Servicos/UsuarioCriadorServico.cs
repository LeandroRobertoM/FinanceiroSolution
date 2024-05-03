using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class UsuarioCreateServico : IUsuarioCreateServico
    {
        private readonly InterfaceUsuarioCreate _interfaceUsuarioCriador;
        private readonly ILogger<SistemaFinanceiroServico> _logger;

        public UsuarioCreateServico(InterfaceUsuarioCreate interfaceUsuarioCriador, ILogger<UsuarioCreate> logger)
        {
            _interfaceUsuarioCriador = interfaceUsuarioCriador;


        }

        public async Task<bool> AdicionarUsuario(UsuarioCreate usuarioCreate)
        {
            try
            {
                IResposta<bool> resposta = await _interfaceUsuarioCriador.Adicionar(usuarioCreate);

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

        public async Task<IList<UsuarioCreate>> ListaSistemasUsuario(string usuarioCriadorId)
        {
            try
            {
                // Chame o método do repositório para obter a lista de sistemas do usuário
                return await _interfaceUsuarioCriador.ListaSistemasUsuario(usuarioCriadorId);
            }
            catch (Exception ex)
            {
                // Trate qualquer exceção e retorne null ou uma lista vazia, conforme necessário
                Console.WriteLine($"Erro ao listar sistemas do usuário: {ex.Message}");
                return null;
            }
        }
    }
    
}

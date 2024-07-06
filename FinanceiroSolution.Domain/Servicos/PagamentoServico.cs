using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IPagamento;
using Microsoft.Extensions.Logging;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class PagamentoServico : IPagamentoServico
    {
        private readonly InterfacePagamento _interfacePagamento;
        private readonly ILogger<PagamentoServico> _logger;
        public PagamentoServico(InterfacePagamento _interfacePagamento, ILogger<PagamentoServico> logger)
        {
            _interfacePagamento = _interfacePagamento;
            _logger = logger;
        }

        public async Task<bool> AdicionarPagamento(Pagamento pagamento)
        {
            try
            {
                IResposta<bool> resposta = await _interfacePagamento.AdicionarPagamento(pagamento);

                if (resposta.OperacaoSucesso == false)
                {
                    Console.WriteLine("Falha ao adicionar a Pagamento: " + resposta.MensagemErro);
                    _logger.LogInformation("Falha ao adicionar a Pagamento: " + resposta.MensagemErro);
                    return false;
                }
                else
                {
                    _logger.LogInformation("Pagamento adicionada com sucesso!");
                    // Faça algo se a operação for bem-sucedida
                    // ...

                    return true;
                }
            }
            catch (Exception ex)
            {


                Console.WriteLine("Ocorreu um erro ao adicionar a Pagamento: " + ex.Message);
                // Ou utilize sua biblioteca de log preferida para registrar o erro

                // Trate o erro aqui
                // ...

            }

            return false;
        }

        public Task AtualizarPagamento(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }

        public Task<object> CarregaGraficos(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}


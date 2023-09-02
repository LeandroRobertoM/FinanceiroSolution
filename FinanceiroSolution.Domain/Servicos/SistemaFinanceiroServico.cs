using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class SistemaFinanceiroServico : ISistemaFinanceiroServico
    {
        private readonly InterfaceSistemaFinanceiro _interfaceSistemaFinanceiro;
        private readonly ILogger<SistemaFinanceiroServico> _logger;

        public SistemaFinanceiroServico(InterfaceSistemaFinanceiro interfaceSistemaFinanceiro,ILogger<SistemaFinanceiroServico> logger)
        {
            _interfaceSistemaFinanceiro = interfaceSistemaFinanceiro;
            _logger = logger;

        }

        public async Task<bool> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.validarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (valido)
            {
                var data = DateTime.Now;
                sistemaFinanceiro.DiaFechamento = 1;
                sistemaFinanceiro.Ano = data.Year;
                sistemaFinanceiro.Mes = data.Month;
                sistemaFinanceiro.AnoCopia = data.Year;
                sistemaFinanceiro.MesCopia = data.Month;
                sistemaFinanceiro.GerarCopiaDespesa = true;
                try
                {
                    IResposta<bool> resposta = await _interfaceSistemaFinanceiro.Add(sistemaFinanceiro);


                    if (resposta.OperacaoSucesso == false)
                    {
                        Console.WriteLine("Falha ao adicionar a Sistema Financeiro: " + resposta.MensagemErro);
                        _logger.LogInformation("Falha ao adicionar a categoria: " + resposta.MensagemErro);
                        return false;
                    }
                    else
                    {
                        _logger.LogInformation("Sistema Financeiro adicionada com sucesso!");
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
            return false;
        }

        public Task AtualizarDespesa(SistemaFinanceiro sistemaFinanceiro)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.validarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (valido)
            {
                sistemaFinanceiro.DiaFechamento = 1;
                await _interfaceSistemaFinanceiro.Update(sistemaFinanceiro);
            }
            return false;
        }

        public Task<object> CarregaGraficos(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}

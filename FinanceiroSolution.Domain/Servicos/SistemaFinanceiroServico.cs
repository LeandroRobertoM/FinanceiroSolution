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
        /// <summary>
        /// Estou verificando se retornamos o objeto inteiro se mudar este serviço terei que mudar 
        // Repositorio interface Controllador
           
        /// </summary>
        /// <param name="sistemaFinanceiro"></param>
        /// <returns></returns>

        public async Task<(bool success, int IdSistemaFinanceiro, SistemaFinanceiro sistemaFianceiroObject)> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            var valido = sistemaFinanceiro.validarPropriedadeString(sistemaFinanceiro.Nome, "Nome");
            if (!valido)
            {
                return (false, 0,null);
            }

            var data = DateTime.Now;
            sistemaFinanceiro.DiaFechamento = 1;
            sistemaFinanceiro.Ano = data.Year;
            sistemaFinanceiro.Mes = data.Month;
            sistemaFinanceiro.AnoCopia = data.Year;
            sistemaFinanceiro.MesCopia = data.Month;
            sistemaFinanceiro.GerarCopiaDespesa = true;

            try
            {
                IResposta<(bool success, int IdSistemaFinanceiro,SistemaFinanceiro sistemaFianceiroObject)> resposta = await _interfaceSistemaFinanceiro.AdicionarSistemaFinanceiro(sistemaFinanceiro);

                if (!resposta.OperacaoSucesso)
                {
                    Console.WriteLine("Falha ao adicionar o Sistema Financeiro: " + resposta.MensagemErro);
                    _logger.LogInformation("Falha ao adicionar o Sistema Financeiro: " + resposta.MensagemErro);
                    return (false, 0, null);
                }
                else
                {
                    int IdSistemaFinanceiro = resposta.Dados.IdSistemaFinanceiro;
                    SistemaFinanceiro sistemaFianceiroObject = resposta.Dados.sistemaFianceiroObject;
                    _logger.LogInformation("Sistema Financeiro adicionado com sucesso!");


                    return (true, IdSistemaFinanceiro, sistemaFianceiroObject);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao adicionar o Sistema Financeiro: " + ex.Message);
       

                return (false, 00, null);
            }
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

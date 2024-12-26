using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.IDespesa.IDespesaRecorrente;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class DespesaRecorrenteServico : IDespesaRecorrenteServico
    {
        private readonly InterfaceDespesaRecorrente _interfaceDespesaRecorrente;
        private readonly ILogger<DespesaRecorrenteServico> _logger;

        public DespesaRecorrenteServico(InterfaceDespesaRecorrente interfaceDespesaRecorrente, ILogger<DespesaRecorrenteServico> logger)
        {
            _interfaceDespesaRecorrente = interfaceDespesaRecorrente;
            _logger = logger;
        }

        public async Task<bool> AdicionarDespesaRecorrente(DespesaRecorrencia despesaRecorrencia)
        {
            try
            {
                IResposta<bool> resposta = await _interfaceDespesaRecorrente.AdicionarDespesaRecorrente(despesaRecorrencia);
  

                if (resposta.OperacaoSucesso == false)
                {
                    Console.WriteLine("Falha ao adicionar a Despesa: " + resposta.MensagemErro);
                    _logger.LogInformation("Falha ao adicionar a Despesa: " + resposta.MensagemErro);
                    return false;
                }
                else
                {
                    _logger.LogInformation("Categoria Despesa com sucesso!");
                }
            }
            catch (Exception ex)
            {


                Console.WriteLine("Ocorreu um erro ao adicionar a Despesa: " + ex.Message);


            }
            return false;


        }
    

        public async Task AtualizarDespesaRecorrente(DespesaRecorrencia despesaRecorrencia)
        {
            await _interfaceDespesaRecorrente.Update(despesaRecorrencia);
        }

        public Task<object> CarregaGraficos(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}

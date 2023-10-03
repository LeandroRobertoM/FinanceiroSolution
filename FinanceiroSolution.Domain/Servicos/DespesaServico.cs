using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
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
    public class DespesaServico : IDespesaServico
    {
        private readonly InterfaceDespesa _interfaceDespesa;
        private readonly ILogger<DespesaServico> _logger;

        public DespesaServico(InterfaceDespesa interfaceDespesa, ILogger<DespesaServico> logger)
        {
            _interfaceDespesa=interfaceDespesa;
            _logger=logger;
        }

        public async Task<bool> AdicionarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataCadastro = data;
            despesa.Ano = data.Year;
            despesa.Mes = data.Month;
            var valido = despesa.validarPropriedadeString(despesa.Nome, "Nome");
            if (valido)

                try
                {
                    IResposta<bool> resposta = await _interfaceDespesa.AdicionarDespesa(despesa);

                    if (resposta.OperacaoSucesso == false) 
                    {
                        Console.WriteLine("Falha ao adicionar a Despesa: " + resposta.MensagemErro);
                        _logger.LogInformation("Falha ao adicionar a Despesa: " + resposta.MensagemErro);
                        return false;
                    }
                    else
                    {
                        if (resposta.OperacaoSucesso == true)
                        {
                            _logger.LogInformation("Despesa adicionado com sucesso!");
                        }
                    }
                }
                catch (Exception ex)
                {


                    Console.WriteLine("Ocorreu um erro ao adicionar a Despesa: " + ex.Message);
                    // Ou utilize sua biblioteca de log preferida para registrar o erro

                    // Trate o erro aqui
                    // ...

                }
                return true;


        }

        public async Task AtualizarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataAlteracao = data;

            if (despesa.Pago) 
            {
                despesa.DataPagamento = data;
            }
            var valido = despesa.validarPropriedadeString(despesa.Nome, "Nome");
            if (valido)
                await _interfaceDespesa.Update(despesa);
        }

        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            var despesasUsuario = await _interfaceDespesa.ListarDespesasUsuario(emailUsuario);
            var despesasAnterior = await _interfaceDespesa.ListarDespesasNaoPagasMesesAnterior(emailUsuario);
            
            var desspesas_naoPagasMeses = despesasAnterior.Any() ?
            despesasAnterior.ToList().Sum(x => x.Valor) : 0;

            var despesas_pagas = despesasUsuario.Where(d => d.Pago && d.TipoDespesa == Enums.EnumTipoDespesa.Contas)
                .Sum(x => x.Valor);

            var despesas_pendentes = despesasUsuario.Where(d => !d.Pago && d.TipoDespesa == Enums.EnumTipoDespesa.Contas)
                .Sum(x => x.Valor);

            var investimentos = despesasUsuario.Where(d => d.TipoDespesa == Enums.EnumTipoDespesa.Contas)
                .Sum(x => x.Valor);

            return new
            {
                sucesso = "OK",
                despesas_pagas= despesas_pagas,
                despesas_pendentes= despesas_pendentes,
                desspesas_naoPagasMeses= desspesas_naoPagasMeses,
                investimentos= investimentos
            };

        }
    }
}

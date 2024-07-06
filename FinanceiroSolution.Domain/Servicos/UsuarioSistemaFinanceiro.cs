using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Servicos
{
    public class UsuarioSistemaFinanceiroServico : IUsuarioSistemaFinanceiroServico
    {

        private readonly InterfaceUserSistemaFinanceiro _interfaceUserSistemaFinanceiro;

        public UsuarioSistemaFinanceiroServico(InterfaceUserSistemaFinanceiro interfaceUserSistemaFinanceiro)
        {
            _interfaceUserSistemaFinanceiro = interfaceUserSistemaFinanceiro;
        }

        public async Task CadastrarUsuarioNoSistema(UsuarioSistemaFinanceiro usuarioSistemaFinanceiro)
        {
            await _interfaceUserSistemaFinanceiro.Add(usuarioSistemaFinanceiro);
        }

        public async Task AdicionarListaSistemasUsuario(List<UsuarioSistemaFinanceiro> usuarioSistemaFinanceiro)
        {
            try
            {
                IResposta<bool> resposta = await _interfaceUserSistemaFinanceiro.AdicionarListaSistemaFinanceiro(usuarioSistemaFinanceiro);

                if (resposta.OperacaoSucesso == false)
                {
                    Console.WriteLine("Falha ao adicionar a categoria: " + resposta.MensagemErro);
                    // Faça algo se a operação falhar
                }
                else
                {
                    // Faça algo se a operação for bem-sucedida
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
    }

}

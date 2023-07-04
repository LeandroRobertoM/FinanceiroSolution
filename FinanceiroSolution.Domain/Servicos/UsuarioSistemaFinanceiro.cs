using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
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
    }
}

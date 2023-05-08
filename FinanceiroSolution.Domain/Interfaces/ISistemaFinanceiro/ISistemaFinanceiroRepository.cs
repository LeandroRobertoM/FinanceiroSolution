using FinanceiroSolution.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro
{
    public interface ISistemaFinanceiroRepository
    {
        void CadastrarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        void EditarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro);
        List<SistemaFinanceiro> BuscarTodos();
        SistemaFinanceiro BuscarPorID(int idSistemaFinanceiro);
        SistemaFinanceiro BuscarNome(string Nome);
        void DeletarSistemaFinanceiro(int idSistemaFinanceiro);
    }
}

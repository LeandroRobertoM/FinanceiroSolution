using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.Servicos;
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
        public CategoriaServico(InterfaceCategoria interfaceCategoria)
        {
            _interfaceCategoria = interfaceCategoria;
        }

        public async Task AdicionarCategoria(Categoria catagoria)
        {
            
                await _interfaceCategoria.Adicionar(catagoria);
        }

        public async Task AtualizarCategoria(Categoria catagoria)
        {
            
 
                await _interfaceCategoria.Update(catagoria);
        }
    }
}

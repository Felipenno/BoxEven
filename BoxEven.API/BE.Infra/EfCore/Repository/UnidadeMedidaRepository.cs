using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Infra.EfCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.EfCore.Repository;

public class UnidadeMedidaRepository : IUnidadeMedidaRepository
{
    private readonly BoxEvenContext _boxEvenContext;

    public UnidadeMedidaRepository(BoxEvenContext boxEvenContext)
    {
        _boxEvenContext = boxEvenContext;
    }

    public Task<bool> AdicionarAsync(UnidadeMedida unidadeMedida)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarAsync(UnidadeMedida unidadeMedida)
    {
        throw new NotImplementedException();
    }

    public Task<List<UnidadeMedida>> ListarTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int unidadeMedidaId)
    {
        throw new NotImplementedException();
    }
}

    


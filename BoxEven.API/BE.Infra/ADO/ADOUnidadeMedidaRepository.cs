using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.ADO;

public class ADOUnidadeMedidaRepository : ADOHelper, IUnidadeMedidaRepository
{
    private readonly IProvedorDados _provedorDados;

    public ADOUnidadeMedidaRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
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
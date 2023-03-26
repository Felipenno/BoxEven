using BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Repository;

public interface IUnidadeMedidaRepository
{
    Task<bool> AdicionarAsync(UnidadeMedida unidadeMedida);
    Task<bool> AtualizarAsync(UnidadeMedida unidadeMedida);
    Task<bool> RemoveAsync(int unidadeMedidaId);
    Task<List<UnidadeMedida>> ListarTodosAsync();
}
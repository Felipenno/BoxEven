using BE.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface IUnidadeMedidaService
{
    Task<bool> CriarUnidadeMedidarAsync(UnidadeMedidaDto unidadeMedida);
    Task<bool> EditarUnidadeMedidaAsync(UnidadeMedidaDto unidadeMedida);
    Task<bool> RemoveUnidadeMedidaAsync(int unidadeMedidaId);
    Task<List<UnidadeMedidaDto>> ListarUnidadeMedidaAsync();
}


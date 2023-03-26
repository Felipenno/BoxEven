using BE.Domain.Entities;
using BE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Repository;

public interface IVendasInfraServices
{
    Task<List<Pedido>> ListarPedidos();
    Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao);
    Task AlterarStatusPedidoAsync(string id, StatusPedido status);
}

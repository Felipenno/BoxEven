using BEV.Domain.Entities;
using BEV.Domain.Enum;

namespace BEV.Domain.Interfaces.Repository;

public interface IPedidoRepository
{
    Task<Produto[]> ListarProdutosAsync();
    Task<List<Pedido>> ListarNovosPedidosAsync();
    Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao);
    Task InserirPedidoAsync(Pedido pedido);
    Task AlterarStatusPedidoAsync(string id, StatusPedido status);
}
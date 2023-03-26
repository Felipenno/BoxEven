using BEV.Domain.Entities;
using BEV.Domain.Enum;
using System.Timers;

namespace BEV.Domain.Interfaces.Service
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ListarNovosPedidosAsync();
        Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao);
        Task GerarPedidosAsync(object source, ElapsedEventArgs e);
        Task AlterarStatusPedidoAsync(string id, StatusPedido status);
        
    }
}
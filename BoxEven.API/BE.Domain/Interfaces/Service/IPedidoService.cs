using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Enum;

namespace BE.Domain.Interfaces.Service;

public interface IPedidoService
{
    Task<ResultadoModel> ListasPedidosSeparacao();
    Task<ResultadoModel> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao);
    Task<ResultadoModel> AtualizarStatus(PedidoAlterarStatusDto pedido);
}

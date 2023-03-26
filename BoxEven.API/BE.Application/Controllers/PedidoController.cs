using BE.Domain.Dtos;
using BE.Domain.Enum;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _PedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _PedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListasPedidos()
    {
        var resultado = await _PedidoService.ListasPedidosSeparacao();
        if(!resultado.Sucesso)
        {
            return NotFound(resultado);
        }

        return Ok(resultado);
    }

    [HttpGet("filtro")]
    public async Task<IActionResult> ListasPedidosFiltro([FromQuery] StatusPedido status, [FromQuery] DateTime conclusao)
    {
        var resultado = await _PedidoService.ListarPedidosPorFiltroAsync(status, conclusao);
        if (!resultado.Sucesso)
        {
            return NotFound(resultado);
        }

        return Ok(resultado);
    }

    [HttpPatch]
    public async Task<IActionResult> AlterarStatusPedido([FromBody] PedidoAlterarStatusDto pedido)
    {
        var resultado = await _PedidoService.AtualizarStatus(pedido);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }
}

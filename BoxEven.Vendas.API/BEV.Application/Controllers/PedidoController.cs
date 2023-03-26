using BEV.Application.Attributes;
using BEV.Domain.Enum;
using BEV.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BEV.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiKey]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private static System.Timers.Timer _timer;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;

        GerarPedidoPeriodicamente(_pedidoService);
    }

    [HttpGet]
    public async Task<IActionResult> ListarPedidos()
    {
        try
        {
            var pedidos = await _pedidoService.ListarNovosPedidosAsync();

            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar pedidos" + ex);
        }
    }

    [HttpGet("filtro")]
    public async Task<IActionResult> ListarPedidosPorFiltroAsync([FromQuery] StatusPedido status,[FromQuery] DateTime conclusao)
    {
        try
        {
            var pedidos = await _pedidoService.ListarPedidosPorFiltroAsync(status, conclusao);

            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar pedidos" + ex);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> AlterarStatusPedidoAsync([FromRoute] string id, [FromBody] StatusPedido status)
    {
        try
        {
            if(string.IsNullOrEmpty(id) || status < 0)
            {
                return BadRequest("Dados Inválidos");
            } 

            await _pedidoService.AlterarStatusPedidoAsync(id, status);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao alterar pedido" + ex);
        }
    }

    private static void GerarPedidoPeriodicamente(IPedidoService pedidoService)
    {
        _timer = new System.Timers.Timer();
        _timer.Elapsed += async (source, e) => await pedidoService.GerarPedidosAsync(source, e);
        _timer.Interval = 1800000;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }
}
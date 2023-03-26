using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadeMedidaController : ControllerBase
{
    private readonly IUnidadeMedidaService _unidadeMedidaService;

    public UnidadeMedidaController(IUnidadeMedidaService unidadeMedidaService)
    {
        _unidadeMedidaService = unidadeMedidaService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarUnidadeMedida()
    {
        var unidadeMedida = await _unidadeMedidaService.ListarUnidadeMedidaAsync();
        if (unidadeMedida == null)
        {
            return NoContent();
        }

        return Ok(unidadeMedida);
    }

    [HttpPost]
    public async Task<IActionResult> CriarUnidadeMedida(UnidadeMedidaDto unidadeMedidaDto)
    {
        var sucesso = await _unidadeMedidaService.CriarUnidadeMedidarAsync(unidadeMedidaDto);
        if (!sucesso)
        {
            return BadRequest();
        }

        return Created("AdicionarUnidadeMedida", unidadeMedidaDto);
    }

    [HttpPut]
    public async Task<IActionResult> EditarUnidadeMedida(UnidadeMedidaDto unidadeMedidaDto)
    {
        var sucesso = await _unidadeMedidaService.EditarUnidadeMedidaAsync(unidadeMedidaDto);
        if (!sucesso)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{unidadeMedidaId}")]
    public async Task<IActionResult> RemoverUnidadeMedida(int unidadeMedidaId)
    {
        var sucesso = await _unidadeMedidaService.RemoveUnidadeMedidaAsync(unidadeMedidaId);

        return Ok(sucesso);
    }
}
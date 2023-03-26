using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacaoController : ControllerBase
{
    private readonly IMovimentacaoService _movimentacaoService;

    public MovimentacaoController(IMovimentacaoService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarMovimentacaoes()
    {
        var resultado = await _movimentacaoService.ListarMovimentacaoesAsync();
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProduto([FromBody] MovimentacaoCriarDto movimentacaoDto)
    {
        var resultado = await _movimentacaoService.CriarMovimentacaoAsync(movimentacaoDto);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }

        return Created("AdicionarProduto", resultado);

    }
}
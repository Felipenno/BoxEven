using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarProdutos([FromQuery(Name = "itensPagina")] int itensPagina, [FromQuery(Name = "pagina")] int pagina, [FromQuery(Name = "status")] bool? status = null, [FromQuery(Name = "id")] int? id = 0, [FromQuery(Name = "descricao")] string? descricao = null)
    {
        if (id < 0 || (!string.IsNullOrWhiteSpace(descricao) && descricao.Length < 3))
        {
            return BadRequest("Dados Inválidos, id deve se válido e descrição deve ter no minímo 3 caracteres");
        }

        if(itensPagina <= 0 || itensPagina > 50 || pagina <= 0 || pagina >= 200)
        {
            return BadRequest("Parâmetros de páginação invalidos");
        }

        var resultado = await _produtoService.ListarProdutosAsync(itensPagina, pagina, status, id, descricao);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }
        return Ok(resultado);
    }

    [HttpGet("desalocado")]
    public async Task<IActionResult> ListarProdutosDesalocados()
    {
        var resultado = await _produtoService.ListarProdutosDesalocadosAsync();
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }
        return Ok(resultado);
    }

    [HttpGet("{produtoId}")]
    public async Task<IActionResult> ListarProdutoPorId([FromRoute] int produtoId)
    {
        if (produtoId < 1)
        {
            return BadRequest();
        }

        var resultado = await _produtoService.ListarPorIdAsync(produtoId);
        if (!resultado.Sucesso)
        {
            return NotFound(resultado);
        }
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProduto([FromBody] ProdutoCriarDto produtoDto)
    {
        var resultado = await _produtoService.CriarProdutoAsync(produtoDto);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }

        return Created("AdicionarProduto", resultado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoEditarDto produtoDto, [FromRoute] int id)
    {
        var resultado = await _produtoService.AtualizarProdutoAsync(produtoDto, id);
        if (!resultado.Sucesso)
        {
            return NotFound(resultado);
        }
        return Ok(resultado);
    }
}

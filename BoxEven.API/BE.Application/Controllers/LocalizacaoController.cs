using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalizacaoController : ControllerBase
{
    private readonly ILocalizacaoService _localizacaoService;

    public LocalizacaoController(ILocalizacaoService localizacaoService)
    {
        _localizacaoService = localizacaoService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarLocalizacoes([FromQuery(Name = "ProdutoId")] int? produtoId, [FromQuery(Name = "Andar")] string? andar, [FromQuery(Name = "Corredor")] int? corredor, [FromQuery(Name = "Lado")] char? lado, [FromQuery(Name = "Prateleira")] int? prateleira)
    {
        var resultado = await _localizacaoService.ListarLocalizacaoAsync(produtoId, andar, corredor, lado, prateleira);
        if (resultado.Objeto == null)
        {
            return NoContent();
        }

        return Ok(resultado);
    }

    [HttpGet("LocalizacoesDisponiveis")]
    public async Task<IActionResult> ListarLocalizacoesDisponiveis()
    {
        var resultado = await _localizacaoService.ListarLocalizacoesDisponiveisAsync();
        if (resultado.Objeto == null)
        {
            return NoContent();
        }

        return Ok(resultado);
    }

    [HttpGet("EnderecoDisponivel/{localizacaoId}")]
    public async Task<IActionResult> EnderecoDisponivel([FromRoute] int localizacaoId)
    {
        var resultado = await _localizacaoService.VerificarEnderecoDisponivelAsync(localizacaoId);

        return Ok(resultado);
    }

    [HttpGet("{localizacaoId}")]
    public async Task<IActionResult> ListarLocalizacaoPorId([FromRoute] int localizacaoId)
    {
        var resultado = await _localizacaoService.ListarLocalizacaoPorIdAsync(localizacaoId);
        if (resultado.Objeto == null)
        {
            return NotFound(resultado);
        }

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarLocalizacao(LocalizacaoCriarDto localizacaoDto)
    {
        var resultado = await _localizacaoService.CriarLocalizacaoAsync(localizacaoDto);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }

        return Created("AdicionarLocalizacao", resultado);
    }

    [HttpPut("{localizacaoId}")]
    public async Task<IActionResult> AtualizarLocalizacao([FromBody] LocalizacaoEditarDto localizacaoDto, [FromRoute] int localizacaoId)
    {
        var resultado = await _localizacaoService.EditarLocalizacaoAsync(localizacaoId, localizacaoDto);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }

    [HttpDelete("{localizacaoId}")]
    public async Task<IActionResult> RemoverLocalizacao([FromRoute] int localizacaoId)
    {
        var sucesso = await _localizacaoService.RemoverLocalizacaoAsync(localizacaoId);

        return Ok(sucesso);
    }
}
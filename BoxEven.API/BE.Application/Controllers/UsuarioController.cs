using BE.Domain.Dtos;
using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [AllowAnonymous]
    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarUsuario(UsuarioRegistroDto usuario)
    {
        var resultado = await _usuarioService.CadastrarUsuarioAsync(usuario);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }

        return Created("RegistrarUsuario", resultado);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioLoginDto login)
    {
        var resultado = await _usuarioService.LoginAsync(login.Apelido, login.Senha);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }
}

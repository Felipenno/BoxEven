using BE.Domain.Dtos;
using BE.Domain.Entities;

namespace BE.Domain.Interfaces.Service;

public interface IUsuarioService
{
    Task<ResultadoModel> CadastrarUsuarioAsync(UsuarioRegistroDto usuario);
    Task<ResultadoModel> LoginAsync(string apelido, string senha);
    Task<ResultadoModel> ConfirmarCadastro();
}
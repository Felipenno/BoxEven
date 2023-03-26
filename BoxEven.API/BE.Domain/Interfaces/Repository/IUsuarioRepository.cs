using BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Repository;

public interface IUsuarioRepository
{
    Task<bool> AdicionarAsync(Usuario usuario);
    Task<bool> AtualizarAsync(Usuario usuario);
    Task<Usuario> ListarPorIdAsync(Guid id);
    Task<Usuario> Login(string apelido, string senha);
    Task<bool> VerificarCodigo(string codigo);
    Task<bool> AlterarSenha(string senha);
    Task<bool> ConfirmarCadastro();

}
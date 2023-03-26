using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Infra.EfCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.EfCore.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BoxEvenContext _boxEvenContext;

    public UsuarioRepository(BoxEvenContext boxEvenContext)
    {
        _boxEvenContext = boxEvenContext;
    }

    public Task<bool> AdicionarAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AlterarSenha(string senha)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ConfirmarCadastro()
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> ListarPorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> Login(string apelido, string senha)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerificarCodigo(string codigo)
    {
        throw new NotImplementedException();
    }
}
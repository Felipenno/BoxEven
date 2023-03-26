using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Infra.EfCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.EfCore.Repository;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly BoxEvenContext _boxEvenContext;

    public MovimentacaoRepository(BoxEvenContext boxEvenContext)
    {
        _boxEvenContext = boxEvenContext;
    }

    public Task<bool> AdicionarAsync(Movimentacao movimentacao)
    {
        throw new NotImplementedException();
    }

    public Task<Movimentacao> ListarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Movimentacao>> ListarTodosAsync()
    {
        throw new NotImplementedException();
    }
}


using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.ADO;

public class ADOMovimentacaoRepository : ADOHelper, IMovimentacaoRepository
{
    private readonly IProvedorDados _provedorDados;

    public ADOMovimentacaoRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
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


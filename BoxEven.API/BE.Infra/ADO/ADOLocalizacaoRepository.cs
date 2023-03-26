using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.ADO;

public class ADOLocalizacaoRepository : ADOHelper, ILocalizacaoRepository
{
    private readonly IProvedorDados _provedorDados;

    public Task<bool> AdicionarAsync(Localizacao localizacao)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AlocarProdutoAsync(List<int> localizacoesId, int produtoId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarAsync(Localizacao localizacao)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DesalocarProdutoAsync(List<int> localizacoesId)
    {
        throw new NotImplementedException();
    }

    public Task<Localizacao> ListarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Localizacao>> ListarTodosAsync(int? produtoId, string? andar, int? corredor, char? lado, int? prateleira)
    {
        throw new NotImplementedException();
    }

    public Task<List<Localizacao>> ListarTodosDisponiveisAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ProdutoLimiteAlocacaoAtingidoAsync(int? produtoId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int localizacaoId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerificarDisponibilidadeAsync(int localizacaoId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerificarExistenciaAsync(Localizacao localizacao)
    {
        throw new NotImplementedException();
    }
}

    


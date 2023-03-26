using BE.Domain.Entities;

namespace BE.Domain.Interfaces.Repository;

public interface ILocalizacaoRepository
{
    Task<bool> AdicionarAsync(Localizacao localizacao);
    Task<bool> VerificarDisponibilidadeAsync(int localizacaoId);
    Task<bool> VerificarExistenciaAsync(Localizacao localizacao);
    Task<bool> AtualizarAsync(Localizacao localizacao);
    Task<bool> AlocarProdutoAsync(List<int> localizacoesId, int produtoId);
    Task<bool> DesalocarProdutoAsync(List<int> localizacoesId);
    Task<bool> RemoveAsync(int localizacaoId);
    Task<bool> ProdutoLimiteAlocacaoAtingidoAsync(int? produtoId);
    Task<Localizacao> ListarPorIdAsync(int id);
    Task<List<Localizacao>> ListarTodosDisponiveisAsync();
    Task<List<Localizacao>> ListarTodosAsync(int? produtoId,string? andar, int? corredor, char? lado, int? prateleira);
}
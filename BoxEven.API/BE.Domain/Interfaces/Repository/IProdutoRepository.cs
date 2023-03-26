using BE.Domain.Entities;

namespace BE.Domain.Interfaces.Repository;

public interface IProdutoRepository
{
    Task<bool> AdicionarAsync(Produto produto);
    Task<bool> AtualizarAsync(Produto produto);
    Task<Produto> ListarPorIdAsync(int id);
    Task<List<Produto>> ListarTodosAsync(int itensPagina, int pagina, bool? status, int? id, string? nome);
    Task<List<Produto>> ListarTodosDesalocadosAsync();
    Task<bool> AlterarQuantidade(int quantidade, int id, DateTime dataAtualizacao);
}
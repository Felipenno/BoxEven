using BE.Domain.Dtos;
using BE.Domain.Entities;

namespace BE.Domain.Interfaces.Service;

public interface IProdutoService
{
    Task<ResultadoModel> CriarProdutoAsync(ProdutoCriarDto produtoDto);
    Task<ResultadoModel> AtualizarProdutoAsync(ProdutoEditarDto produtoDto, int id);
    Task<ResultadoModel> ListarProdutosAsync(int itensPagina, int pagina, bool? status, int? id, string? nome);
    Task<ResultadoModel> ListarProdutosDesalocadosAsync();
    Task<ResultadoModel> ListarPorIdAsync(int produtoId);
}
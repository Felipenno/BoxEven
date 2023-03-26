using BE.Domain.Dtos;
using BE.Domain.Entities;

namespace BE.Domain.Interfaces.Service;

public interface ILocalizacaoService
{
    Task<ResultadoModel> CriarLocalizacaoAsync(LocalizacaoCriarDto localizacaoDto);
    Task<ResultadoModel> VerificarEnderecoDisponivelAsync(int localizacaoId);
    Task<ResultadoModel> EditarLocalizacaoAsync(int localizacaoId, LocalizacaoEditarDto localizacaoDto);
    Task<ResultadoModel> RemoverLocalizacaoAsync(int localizacaoId);
    Task<ResultadoModel> ListarLocalizacaoPorIdAsync(int localizacaoId);
    Task<ResultadoModel> ListarLocalizacoesDisponiveisAsync();
    Task<ResultadoModel> ListarLocalizacaoAsync(int? produtoId, string? andar, int? corredor, char? lado, int? prateleira);
}
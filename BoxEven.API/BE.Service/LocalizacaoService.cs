using AutoMapper;
using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service;

public class LocalizacaoService : ILocalizacaoService
{
    private readonly ILocalizacaoRepository _localizacaoRepository;
    private readonly IMapper _mapper;

    public LocalizacaoService(ILocalizacaoRepository localizacaRepository, IMapper mapper)
    {
        _localizacaoRepository = localizacaRepository;
        _mapper = mapper;
    }

    public async Task<ResultadoModel> CriarLocalizacaoAsync(LocalizacaoCriarDto localizacaoDto)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var localizacao = _mapper.Map<Localizacao>(localizacaoDto);

        var localizacaoExiste = await _localizacaoRepository.VerificarExistenciaAsync(localizacao);
        if (localizacaoExiste)
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Localização já existe.";
            return resultadoModel;
        }

        if(localizacao.ProdutoId != null && localizacao.ProdutoId > 0)
        {
            var produtoLimite = await _localizacaoRepository.ProdutoLimiteAlocacaoAtingidoAsync(localizacao.ProdutoId);
            if (produtoLimite)
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Um produto não pode ter mais que 3 locais.";
                return resultadoModel;
            }
        }

        var concluido = await _localizacaoRepository.AdicionarAsync(localizacao);
        if (concluido)
        {
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Localização criada.";
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Não foi possível criar essa localização.";
            return resultadoModel;
        }
    }

    public async Task<ResultadoModel> EditarLocalizacaoAsync(int localizacaoId, LocalizacaoEditarDto localizacaoDto)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var localizacaoExistente = await _localizacaoRepository.ListarPorIdAsync(localizacaoId);
        if(localizacaoExistente == null || localizacaoExistente.LocalizacaoId != localizacaoDto.LocalizacaoId)
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Localização não existe.";
            return resultadoModel;
        }

        var localizacao = _mapper.Map<Localizacao>(localizacaoDto);

        if (!localizacaoExistente.EnderecoIgual(localizacao))
        {
            var localizacaoExiste = await _localizacaoRepository.VerificarExistenciaAsync(localizacao);
            if (localizacaoExiste)
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Localização já existe.";
                return resultadoModel;
            }
        }

        if (localizacao.ProdutoId != null && localizacao.ProdutoId > 0)
        {
            var produtoLimite = await _localizacaoRepository.ProdutoLimiteAlocacaoAtingidoAsync(localizacao.ProdutoId);
            if (produtoLimite)
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Um produto não pode ter mais que 3 locais.";
                return resultadoModel;
            }
        }

        var concluido =  await _localizacaoRepository.AtualizarAsync(localizacao);
        if (concluido)
        {
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Localização alterada.";
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Não foi possível editar essa localização.";
            return resultadoModel;
        }
    }

    public async Task<ResultadoModel> ListarLocalizacaoAsync(int? produtoId, string? andar, int? corredor, char? lado, int? prateleira)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var localizacao = await _localizacaoRepository.ListarTodosAsync(produtoId, andar, corredor, lado, prateleira);
        List<LocalizacaoDto> resultado = _mapper.Map<List<LocalizacaoDto>>(localizacao);

        if(resultado != null)
        {
            if(resultado.Count <= 0)
            {
                resultadoModel.Mensagem = "Nenhuma localização encontrada";
            }

            resultadoModel.Sucesso = true;
            resultadoModel.Objeto = resultado;
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Não foi possível listar as localizações.";
            return resultadoModel;
        }
    }

    public async Task<ResultadoModel> ListarLocalizacaoPorIdAsync(int localizacaoId)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var localizacao = await _localizacaoRepository.ListarPorIdAsync(localizacaoId);
        LocalizacaoDto resultado = _mapper.Map<LocalizacaoDto>(localizacao);

        if (resultado != null)
        {
            resultadoModel.Sucesso = true;
            resultadoModel.Objeto = resultado;
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Não foi possível obter a localização.";
            return resultadoModel;
        }
    }

    public async Task<ResultadoModel> ListarLocalizacoesDisponiveisAsync()
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        List<LocalizacaoEnderecoMontadoDto> localizacoesDto = new List<LocalizacaoEnderecoMontadoDto>();

        var localizacoes = await _localizacaoRepository.ListarTodosDisponiveisAsync();
        foreach(var item in localizacoes)
        {
            localizacoesDto.Add(new LocalizacaoEnderecoMontadoDto { LocalizacaoId = item.LocalizacaoId, Endereco = item.MontarEndereco() });
        }

        if(localizacoesDto != null)
        {
            if (localizacoesDto.Count <= 0)
            {
                resultadoModel.Mensagem = "Nenhuma localização disponível encontrada";
            }

            resultadoModel.Sucesso = true;
            resultadoModel.Objeto = localizacoesDto;
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Não foi possível listar as localições disponíveis";
            return resultadoModel;
        }
    }

    public async Task<ResultadoModel> RemoverLocalizacaoAsync(int localizacaoId)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var concluido = await _localizacaoRepository.RemoveAsync(localizacaoId);
        if (concluido)
        {            
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Localizacão removida.";
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Não foi possível excluir essa localização.";
            return resultadoModel;
        }
    }

    public async Task<ResultadoModel> VerificarEnderecoDisponivelAsync(int localizacaoId)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        bool disponivel = await _localizacaoRepository.VerificarDisponibilidadeAsync(localizacaoId);

        if (disponivel)
        {
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Endereço disponível.";
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Endereço não disponível.";
            return resultadoModel;
        }
    }

}
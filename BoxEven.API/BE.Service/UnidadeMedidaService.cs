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

public class UnidadeMedidaService : IUnidadeMedidaService
{
    private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;
    private readonly IMapper _mapper;

    public UnidadeMedidaService(IUnidadeMedidaRepository unidadeMedidaRepository, IMapper mapper)
    {
        _unidadeMedidaRepository = unidadeMedidaRepository;
        _mapper = mapper;
    }

    public async Task<bool> EditarUnidadeMedidaAsync(UnidadeMedidaDto unidadeMedidaDto)
    {
        var unidadeMedida = _mapper.Map<UnidadeMedida>(unidadeMedidaDto);
        var sucesso = await _unidadeMedidaRepository.AtualizarAsync(unidadeMedida);
        return sucesso;
    }

    public async Task<List<UnidadeMedidaDto>> ListarUnidadeMedidaAsync()
    {
        var unidadeMedida = await _unidadeMedidaRepository.ListarTodosAsync();
        return _mapper.Map<List<UnidadeMedidaDto>>(unidadeMedida);
    }

    public async Task<bool> CriarUnidadeMedidarAsync(UnidadeMedidaDto unidadeMedidaDto)
    {
        var unidadeMedida = _mapper.Map<UnidadeMedida>(unidadeMedidaDto);
        var sucesso = await _unidadeMedidaRepository.AdicionarAsync(unidadeMedida);
        return sucesso;
    }

    public async Task<bool> RemoveUnidadeMedidaAsync(int unidadeMedidaId)
    {
        var sucesso = await _unidadeMedidaRepository.RemoveAsync(unidadeMedidaId);
        return sucesso;
    }
}
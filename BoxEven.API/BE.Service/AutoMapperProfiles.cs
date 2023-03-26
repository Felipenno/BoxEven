using AutoMapper;
using BE.Domain.Dtos;
using BE.Domain.Entities;

namespace BE.Service.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Produto, ProdutoListarDto>().ReverseMap();
        CreateMap<Produto, ProdutoEditarDto>().ReverseMap();
        CreateMap<Produto, ProdutoCriarDto>().ReverseMap();

        CreateMap<Localizacao, LocalizacaoDto>().ReverseMap();
        CreateMap<LocalizacaoCriarDto, Localizacao>();
        CreateMap<LocalizacaoEditarDto, Localizacao>();

        CreateMap<Movimentacao, MovimentacaoDto>().ReverseMap();
        CreateMap<Movimentacao, MovimentacaoCriarDto>().ReverseMap();

        CreateMap<Pedido, PedidoDto>();
        CreateMap<PedidoDto, Pedido>();

        CreateMap<UnidadeMedida, UnidadeMedidaDto>();
        CreateMap<UnidadeMedidaDto, UnidadeMedida>();

        CreateMap<Usuario, UsuarioDto>();
        CreateMap<UsuarioDto, Usuario>();
    }
}
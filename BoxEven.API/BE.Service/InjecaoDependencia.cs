using BE.Domain.Interfaces;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;
using BE.Infra;
using BE.Infra.ADO;
using BE.Infra.Dapper;
using BE.Infra.EfCore.Context;
using BE.Infra.EfCore.Repository;
using BE.Service.AutoMapper;
using BE.Service.Cryptography;
using Microsoft.Extensions.DependencyInjection;

namespace BE.Service;

public class InjecaoDependencia
{
    public static void Configurar(IServiceCollection services)
    {
        services.AddAutoMapper(x => x.AddProfile(new AutoMapperProfiles()));

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICryptographyService, CryptographyService>();
        services.AddScoped<ILocalizacaoService, LocalizacaoService>();
        services.AddScoped<IMovimentacaoService, MovimentacaoService>();
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IVendasInfraServices, VendasInfraServices>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IUnidadeMedidaService, UnidadeMedidaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IArquivosService, ArquivosService>();

        services.AddDbContext<BoxEvenContext>();
        services.AddSingleton<IProvedorDados, ProvedorDados>();

        //ADO
        //services.AddScoped<ILocalizacaoRepository, ADOLocalizacaoRepository>();
        //services.AddScoped<IMovimentacaoRepository, ADOMovimentacaoRepository>();
        //services.AddScoped<IProdutoRepository, ADOProdutoRepository>();
        //services.AddScoped<IUnidadeMedidaRepository, ADOUnidadeMedidaRepository>();
        //services.AddScoped<IUsuarioRepository, ADOUsuarioRepository>();

        //DAPPER
        services.AddScoped<ILocalizacaoRepository, DPLocalizacaoRepository>();
        services.AddScoped<IMovimentacaoRepository, DPMovimentacaoRepository>();
        services.AddScoped<IProdutoRepository, DPProdutoRepository>();
        services.AddScoped<IUnidadeMedidaRepository, DPUnidadeMedidaRepository>();
        services.AddScoped<IUsuarioRepository, DPUsuarioRepository>();

        //EF
        //services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
        //services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
        //services.AddScoped<IProdutoRepository, ProdutoRepository>();
        //services.AddScoped<IUnidadeMedidaRepository, UnidadeMedidaRepository>();
        //services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}
using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Infra.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BE.Infra.EfCore.Context;

public class BoxEvenContext : DbContext
{
    private readonly IProvedorDados _provedorDados;

    public BoxEvenContext(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
    }

    public DbSet<Localizacao> Localizacoes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }
    //public DbSet<Movimentacao> Movimentacoes { get; set; }
    //public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_provedorDados.StringConexao());
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProdutoMapping());
        modelBuilder.ApplyConfiguration(new LocalizacaoMapping());
        //modelBuilder.ApplyConfiguration(new MovimentacaoMapping());
        modelBuilder.ApplyConfiguration(new UnidadeMedidaMapping());
        //modelBuilder.ApplyConfiguration(new UsuarioMapping());
    }
}
using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BE.Infra.EfCore.Mapping;

internal class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("TB_PRODUTO");

        builder.HasKey(pr => pr.ProdutoId);

        builder.Property(pr => pr.ProdutoId).HasColumnName("ID_PRODUTO");
        builder.Property(pr => pr.Ativo).HasColumnName("ATIVO");
        builder.Property(pr => pr.UnidadeMedidaId).HasColumnName("FK_IDUNIDADE_MEDIDA");
        builder.Property(pr => pr.Quantidade).HasColumnName("QUANTIDADE");
        builder.Property(pr => pr.Preco).HasColumnName("PRECO");
        builder.Property(pr => pr.Nome).HasColumnName("NOME");
        builder.Property(pr => pr.ImagemTipo).HasColumnName("IMAGEM_TIPO");
        builder.Property(pr => pr.ImagemNome).HasColumnName("IMAGEM_NOME");
        builder.Property(pr => pr.CodigoBarras).HasColumnName("CODIGO_BARRAS");
        builder.Property(pr => pr.Criacao).HasColumnName("DATA_CRIACAO");
        builder.Property(pr => pr.Atualizacao).HasColumnName("DATA_ATUALIZACAO");
    }
}
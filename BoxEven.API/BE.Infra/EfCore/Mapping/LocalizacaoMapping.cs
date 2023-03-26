using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.EfCore.Mapping;

internal class LocalizacaoMapping : IEntityTypeConfiguration<Localizacao>
{
    public void Configure(EntityTypeBuilder<Localizacao> builder)
    {
        builder.ToTable("TB_LOCALIZACAO");

        builder.HasKey(ll => ll.LocalizacaoId);

        builder.Property(ll => ll.LocalizacaoId).HasColumnName("ID_LOCALIZACAO");
        builder.Property(ll => ll.ProdutoId).HasColumnName("FK_IDPRODUTO");
        builder.Property(ll => ll.Andar).HasColumnName("ANDAR");
        builder.Property(ll => ll.Corredor).HasColumnName("CORREDOR");
        builder.Property(ll => ll.Lado).HasColumnName("LADO");
        builder.Property(ll => ll.Prateleira).HasColumnName("PRATELEIRA");
        builder.Property(ll => ll.Vao).HasColumnName("VAO");

        builder.HasOne(ll => ll.Produto).WithMany(x => x.Localizacoes).HasForeignKey(x => x.ProdutoId);
    }
}


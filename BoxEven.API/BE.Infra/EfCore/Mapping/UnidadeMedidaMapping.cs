using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.EfCore.Mapping;

internal class UnidadeMedidaMapping : IEntityTypeConfiguration<UnidadeMedida>
{
    public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
    {
        builder.ToTable("TB_UNIDADE_MEDIDA");

        builder.HasKey(um => um.UnidadeMedidaId);

        builder.Property(um => um.UnidadeMedidaId).HasColumnName("ID_UNIDADE_MEDIDA");
        builder.Property(um => um.Descricao).HasColumnName("DESCRICAO");

        builder.HasOne(um => um.Produto).WithOne(x => x.UnidadeMedida).HasForeignKey<Produto>(x => x.UnidadeMedidaId);
    }
}


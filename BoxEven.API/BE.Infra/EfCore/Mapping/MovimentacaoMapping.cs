using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.EfCore.Mapping;

internal class MovimentacaoMapping : IEntityTypeConfiguration<Movimentacao>
{
    public void Configure(EntityTypeBuilder<Movimentacao> builder)
    {
        //builder.ToTable("TB_MOVIMENTACAO");
        //builder.HasKey(x => x.MovimentacaoId).HasName("ID_MOVIMENTACAO");
        //builder.Property(x => x.Quantidade).HasColumnName("QUANTIDADE");
        //builder.Property(x => x.Justificativa).HasColumnName("JUSTIFICATIVA");
        //builder.Property(x => x.DataOperacao).HasColumnName("DATA_OPERACAO");
        //builder.Property(x => x.Tipo).HasColumnName("TIPO");
        //builder.HasOne(x => x.Produto);
        //builder.HasOne(x => x.Usuario);
    }
}
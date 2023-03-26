using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.EfCore.Mapping;

internal class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TB_USUARIO");
        builder.HasKey(x => x.UsuarioId).HasName("ID_USUARIO");
        builder.Property(x => x.Apelido).HasColumnName("APELIDO");
        builder.Property(x => x.Nome).HasColumnName("NOME_COMPLETO");
        builder.Property(x => x.Email).HasColumnName("EMAIL");
        builder.Property(x => x.Senha).HasColumnName("SENHA");
    }
}


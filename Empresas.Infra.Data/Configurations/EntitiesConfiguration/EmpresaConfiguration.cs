using Empresas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Infra.Data.Configurations.EntitiesConfiguration
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            builder.Property(e => e.CNPJ).IsRequired().HasMaxLength(14);
            builder.Property(e => e.DataCriacao).IsRequired();
            builder.Property(e => e.Endereco).HasMaxLength(200);
            builder.Property(e => e.Telefone).HasMaxLength(15);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Site).HasMaxLength(100);
            builder.Property(e => e.Descricao).HasMaxLength(500);
        }
    }
}

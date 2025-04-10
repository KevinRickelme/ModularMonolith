using EmpresasFuncionarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpresasFuncionarios.Infra.Data.Configurations.EntitiesConfiguration
{
    public class EmpresaFuncionarioConfiguration : IEntityTypeConfiguration<EmpresaFuncionario>
    {
        public void Configure(EntityTypeBuilder<EmpresaFuncionario> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Cargo).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Departamento).IsRequired().HasMaxLength(50);
            builder.Property(e => e.DataAdmissao).IsRequired();
            builder.Property(e => e.FuncionarioId).IsRequired();
            builder.Property(e => e.EmpresaId).IsRequired();

        }
    }
}

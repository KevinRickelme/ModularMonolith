using EmpresasFuncionarios.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Infra.Data.Configurations.EntitiesConfiguration
{
    public class EmpresaFuncionarioEventConfiguration : IEntityTypeConfiguration<EmpresaFuncionarioEvent>
    {
        public void Configure(EntityTypeBuilder<EmpresaFuncionarioEvent> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

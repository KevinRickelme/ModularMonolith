using EmpresasFuncionarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Infra.Data.Context
{
    public class EmpresaFuncionarioDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<EmpresaFuncionario> EmpresasFuncionarios { get; set; }
        public DbSet<EmpresaFuncionarioEvent> EventosEmpresaFuncionario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmpresaFuncionarioDbContext).Assembly);
        }
    }
}

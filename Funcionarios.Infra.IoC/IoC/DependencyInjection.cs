using Funcionarios.Application.Mappings;
using Funcionarios.Domain.Abstractions;
using Funcionarios.Infra.Data.Repositories;
using Funcionarios.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Infra.IoC.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<FuncionarioDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly(typeof(FuncionarioDbContext)
                        .Assembly.FullName)));
            services.AddDbContext<FuncionarioDbContext>(options =>

            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();



            services.Scan(select =>
                          select.FromAssemblies(Application.AssemblyReferences.AssemblyReference.Assembly,
                                              Domain.AssemblyReferences.AssemblyReference.Assembly,
                                              Data.AssemblyReferences.AssemblyReference.Assembly)
                          .AddClasses(false)
                          .AsImplementedInterfaces()
                          .WithScopedLifetime());
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            return services;
        }
    }
}

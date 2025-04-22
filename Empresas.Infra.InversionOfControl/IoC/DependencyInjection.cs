using Empresas.Application.Mappings;
using Empresas.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Empresas.Infra.InversionOfControl.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureEmpresas(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<EmpresaDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly(typeof(EmpresaDbContext)
                        .Assembly.FullName)));

            services.Scan(select =>
                          select.FromAssemblies(Application.AssemblyReferences.AssemblyReference.Assembly,
                                              Domain.AssemblyReferences.AssemblyReference.Assembly,
                                              Data.AssemblyReferences.AssemblyReference.Assembly)
                          .AddClasses(classes => classes.Where(type =>
                                        type.IsClass &&
                                        !type.IsAbstract &&
                                        !type.Name.EndsWith("Command") &&
                                        !type.Name.EndsWith("Query") &&
                                        !type.Name.EndsWith("DTO")
                                        ))
                          .AsImplementedInterfaces()
                          .WithScopedLifetime());
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            services.AddValidatorsFromAssembly(Application.AssemblyReferences.AssemblyReference.Assembly, includeInternalTypes: true);
            return services;
        }
    }
}

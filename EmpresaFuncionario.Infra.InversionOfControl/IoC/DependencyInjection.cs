using EmpresasFuncionarios.Application.Mappings;
using EmpresasFuncionarios.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace EmpresasFuncionarios.Infra.InversionOfControl.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureEmpresasFuncionarios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<EmpresaFuncionarioDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly(typeof(EmpresaFuncionarioDbContext)
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
                                        !type.Name.EndsWith("DTO") &&
                                        !type.Name.EndsWith("Event")))
                          .AsImplementedInterfaces()
                          .WithScopedLifetime());
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            
            services.AddValidatorsFromAssembly(Application.AssemblyReferences.AssemblyReference.Assembly, includeInternalTypes: true); 
            return services;
        }
    }
}

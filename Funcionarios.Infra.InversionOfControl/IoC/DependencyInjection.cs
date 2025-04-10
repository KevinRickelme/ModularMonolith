using Funcionarios.Application.Mappings;
using Funcionarios.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Funcionarios.Infra.InversionOfControl.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureFuncionarios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<FuncionarioDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly(typeof(FuncionarioDbContext)
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
            return services;
        }
    }
}

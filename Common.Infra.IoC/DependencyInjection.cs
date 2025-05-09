using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using Scrutor;
using Microsoft.Extensions.Configuration;
namespace Common.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureCommon(this IServiceCollection services, IConfiguration configuration)
        {

            services.Scan(select =>
                          select.FromAssemblies(Data.AssemblyReferences.AssemblyReference.Assembly)
                          .AddClasses(classes => classes.Where(type =>
                                        type.IsClass &&
                                        !type.IsAbstract &&
                                        !type.Name.EndsWith("Command") &&
                                        !type.Name.EndsWith("Query") &&
                                        !type.Name.EndsWith("DTO")
                                        ))
                          .AsImplementedInterfaces()
                          .WithScopedLifetime());
            return services;
        }
    }
}

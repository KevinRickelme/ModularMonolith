﻿using EmpresasFuncionarios.Application.Mappings;
using EmpresasFuncionarios.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                                        !type.Name.EndsWith("DTO")
                                        ))
                          .AsImplementedInterfaces()
                          .WithScopedLifetime());
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            return services;
        }
    }
}

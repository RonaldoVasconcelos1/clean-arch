using System.Reflection;
using Amazon.SimpleSystemsManagement;
using Application.Interfaces;
using Application.Mappings;
using Application.Products.Commands;
using Application.Products.Handlers;
using Application.Products.Queries;
using Application.UseCases;
using Core.Command;
using Core.Handler;
using Core.Query;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories;
using Infra.services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryService, CategoryUseCase>();
        services.AddScoped<IProductService, ProductUseCase>();

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddAWSService<IAmazonSimpleSystemsManagement>();
        services.AddScoped<IParameterStoreService, ParameterStoreService>();

        services.AddDbContext<ApplicationDbContext>(async (provider, options) =>
        {
            string connectionString;

#if DEBUG
            // Usar a string de conexão local durante o desenvolvimento
            connectionString = configuration.GetConnectionString("DefaultConnection");
#else
                // Obter a string de conexão do AWS Parameter Store em produção
                var parameterStoreService = provider.GetRequiredService<IParameterStoreService>();

                var host = await parameterStoreService.GetParameterAsync("/myapp/rds/host");
                var username = await parameterStoreService.GetParameterAsync("/myapp/rds/username");
                var password = await parameterStoreService.GetParameterAsync("/myapp/rds/password");
                var database = await parameterStoreService.GetParameterAsync("/myapp/rds/database");

                connectionString = $"Host={host};Username={username};Password={password};Database={database}";
#endif
            options.UseNpgsql(connectionString);
        });
        return services;
    }
}
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

        var myhandlers = AppDomain.CurrentDomain.Load("Application");
        services.AddMediatR(myhandlers);

        return services;
    }
}
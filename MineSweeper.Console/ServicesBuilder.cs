using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MineSweeper.Domain.Contracts;
using MineSweeper.Domain.Entities;
using MineSweeper.Rules;
using MineSweeper.Rules.Services;
using MineSweeper.Rules.UseCases;

namespace GIC_Minesweeper;

public static class ServicesBuilder
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services
            .AddScoped<GIC_Minesweeper.MineSweeper>()
            .AddScoped<IMineGridService, MineGridService>()
            .AddScoped<InitializeGridUseCase>()
            .AddScoped<RevealSquareUseCase>()
            .Configure<GridConstraints>(configuration.GetSection("GridConstraints"));
        return services;
    }
}
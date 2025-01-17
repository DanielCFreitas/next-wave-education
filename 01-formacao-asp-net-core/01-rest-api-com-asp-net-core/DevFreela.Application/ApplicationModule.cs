using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddServices()
            .AddHandlers();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<InsertCommentHandler>());
        
        return services;
    }
}
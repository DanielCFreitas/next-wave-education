using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddValidations()
            .AddHandlers();
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<InsertCommentHandler>());

        services
            .AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>,
                ValidadeInsertProjectCommandBehavior>();

        return services;
    }

    private static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
        
        return services;
    }
}
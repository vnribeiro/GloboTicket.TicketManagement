using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GloboTicket.TicketManagement.Application.Extensions;

public static class ApplicationServicesRegistrationExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper Assemblies
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Register MediatR Assemblies
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        // Register Fluent Validation Assemblies
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}

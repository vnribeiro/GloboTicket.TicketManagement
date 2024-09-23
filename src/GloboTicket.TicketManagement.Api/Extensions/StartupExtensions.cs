using Asp.Versioning.ApiExplorer;
using GloboTicket.TicketManagement.Api.Services;
using GloboTicket.TicketManagement.Application.Contracts;
using GloboTicket.TicketManagement.Application.Extensions;
using GloboTicket.TicketManagement.Infrastructure.Extensions;
using GloboTicket.TicketManagement.Persistence.Data;
using GloboTicket.TicketManagement.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Api.Extensions;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add Services
        builder
            .AddPresentationServices();

        builder.Services
        .AddApplicationServices()
        .AddPersistenceServices(builder.Configuration)
        .AddInfrastructureServices(builder.Configuration);

        // Add controllers
        builder.Services.AddControllers();

        // Log
        builder.Services
            .AddScoped<ILoggedInUserService, LoggedInUserService>();

        builder.Services
            .AddHttpContextAccessor();

        // Add CORS
        builder.Services
            .AddCors(options =>
                options.AddPolicy("open", policy =>
                    policy.WithOrigins([
                            builder.Configuration["ApiUrl"] ??
                            "https://localhost:7020",
                            builder.Configuration["BlazorUrl"] ?? 
                            "https://localhost:7080"
                        ])
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(pol => true)
                        .AllowAnyHeader()
                        .AllowCredentials()));

        // Enable SwaggerGen
        builder.Services
            .AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Enable CORS
        app.UseCors("open");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            // Configure SwaggerUI to show all APIS versions
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services
                    .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        $"Globo Ticket API {description.GroupName.ToUpperInvariant()}");
                }
            });
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}

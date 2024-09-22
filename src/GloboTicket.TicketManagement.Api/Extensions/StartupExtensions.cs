using Asp.Versioning.ApiExplorer;
using GloboTicket.TicketManagement.Application.Extensions;
using GloboTicket.TicketManagement.Infrastructure.Extensions;
using GloboTicket.TicketManagement.Persistence.Extensions;

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


        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
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

        app.UseCors("open");
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}

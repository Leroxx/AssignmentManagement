using AssignmentManagement.Api.Persistance;
using AssignmentManagement.Api.Persistance.Common;
using AssignmentManagement.Api.Services.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AssignmentManagement.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Assignment Management API", Version = "v1"});

            var filePath = Path.Combine(AppContext.BaseDirectory, "AssignmentManagement.Api.xml");

            s.IncludeXmlComments(filePath);
        });

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions));
        });

        services.AddDbContext<AssignmentManagementDbContext>(options =>
            options.UseSqlite("Data Source = AssignmentManagement.db"));

        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
    

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;
using TwoSchools.App.Services;

namespace TwoSchools.App.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register all application services
        services.AddScoped<EnrollmentService>();
        services.AddScoped<StudentService>();
        services.AddScoped<SubjectService>();
        services.AddScoped<SchoolService>();
        services.AddScoped<SchoolYearService>();
        services.AddScoped<TeacherService>();
        services.AddScoped<TermService>();

        return services;
    }
}

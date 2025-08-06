using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;
using TwoSchools.Infra.Repositories;
using TwoSchools.Infra.Seeders;

namespace TwoSchools.Infra.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<SchoolDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TwoSchoolsDB")));
        // Register all repositories
        services.AddScoped<ISchoolRepository, SchoolRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ISchoolYearRepository, SchoolYearRepository>();
        services.AddScoped<ITermRepository, TermRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        // Register seeders
        services.AddScoped<IOrganizationSeeder, OrganizationSeeder>();
        return services;
    }

    public static async Task<IServiceProvider> SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SchoolDBContext>();
        var seeder = scope.ServiceProvider.GetRequiredService<IOrganizationSeeder>();

        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Run seeder
        await seeder.Seed();

        return serviceProvider;
    }
}

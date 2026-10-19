using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccessExtensions(this IServiceCollection services,IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("default");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ITrackRepo, TrackRepo>();
        services.AddScoped<IIntakeRepo, IntakeRepo>();
        services.AddScoped<IBranchRepo, BranchRepo>();
        services.AddScoped<ICourseRepo, CourseRepo>();
        services.AddScoped<ITrackCourseRepo, TrackCourseRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Seeding Data
        // 1. Seed Training Programs
        modelBuilder.Entity<TrainingProgram>().HasData(
            new TrainingProgram { Id = 1, Name = "Full Stack Development", Description = "Web development using .NET and Angular", DurationInMonth = 4 },
            new TrainingProgram { Id = 2, Name = "AI and Data Science", Description = "Artificial intelligence and machine learning track", DurationInMonth = 3 }
        );

        // 2. Seed Branches
        modelBuilder.Entity<Branch>().HasData(
            new Branch { Id = 1, Name = "Smart Village", City = "Giza", Adress = "KM 28 Cairo-Alexandria Desert Road" },
            new Branch { Id = 2, Name = "National Telecommunication Institute", City = "Cairo", Adress = "Nasr City" }
        );

        // 3. Seed Intakes
        modelBuilder.Entity<Intake>().HasData(
            new Intake { Id = 1, Name = "Intake 44", StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2026, 2, 1), TrainingProgramId = 1 },
            new Intake { Id = 2, Name = "Intake 45", StartDate = new DateTime(2026, 3, 1), EndDate = new DateTime(2026, 7, 1), TrainingProgramId = 2 }
        );

        // 4. Seed Tracks
        modelBuilder.Entity<Track>().HasData(
            new Track { Id = 1, Name = ".NET Web Developer", Description = "C#, EF Core, ASP.NET Core", IntakeId = 1, BranchId = 1 },
            new Track { Id = 2, Name = "Machine Learning", Description = "Python, Pandas, TensorFlow", IntakeId = 2, BranchId = 2 }
        );

        // 5. Seed Courses
        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "C# Programming", Description = "Fundamentals of C#" },
            new Course { Id = 2, Name = "Database Fundamentals", Description = "SQL Server and EF Core" },
            new Course { Id = 3, Name = "Python for AI", Description = "Python basics for data science" }
        );

        // 6. Seed TrackCourses (Many-to-Many Relationship)
        modelBuilder.Entity<TrackCourse>().HasData(
            new TrackCourse { TrackId = 1, CourseId = 1, Hours = 60 },
            new TrackCourse { TrackId = 1, CourseId = 2, Hours = 45 },
            new TrackCourse { TrackId = 2, CourseId = 3, Hours = 50 }
        );

        // 7. Seed Students
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                Fname = "Ahmed",
                Mname = "Ali",
                Lname = "Mohamed",
                Email = "ahmed.ali@test.com",
                Phone = "01012345678",
                NationalId = "30101010101011",
                DateOfBirth = new DateTime(2001, 5, 15),
                TrackId = 1
            },
            new Student
            {
                Id = 2,
                Fname = "Sara",
                Mname = "Mahmoud",
                Lname = "Hassan",
                Email = "sara.m@test.com",
                Phone = "01112345678",
                NationalId = "30202020202022",
                DateOfBirth = new DateTime(2002, 8, 20),
                TrackId = 2
            }
        );
        // Seeding Data

    }

    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Intake> Intake => Set<Intake>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<TrainingProgram> TrainingPrograms => Set<TrainingProgram>();
    public DbSet<TrackCourse> TrackCourses => Set<TrackCourse>();
}

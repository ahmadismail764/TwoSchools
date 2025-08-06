using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Infra.Persistence;
namespace TwoSchools.Infra.Seeders;

internal class OrganizationSeeder // : IOrganizationSeeder
{
    private readonly SchoolDBContext _context;

    internal DbSet<School> Schools { get; set; }
    internal DbSet<SchoolYear> SchoolYears { get; set; }
    internal DbSet<Student> Students { get; set; }
    internal DbSet<Subject> Subjects { get; set; }
    internal DbSet<Teacher> Teachers { get; set; }
    internal DbSet<Term> Terms { get; set; }
    internal DbSet<Enrollment> Enrollments { get; set; }

    public OrganizationSeeder(SchoolDBContext context)
    {
        _context = context;
        Schools = context.Schools;
        SchoolYears = context.SchoolYears;
        Students = context.Students;
        Subjects = context.Subjects;
        Teachers = context.Teachers;
        Terms = context.Terms;
        Enrollments = context.Enrollments;
    }

    public async Task SeedAsync()
    {
        await SeedSchoolsAsync();
        await SeedSchoolYearsAsync();
        await SeedTermsAsync();
        await SeedTeachersAsync();
        await SeedStudentsAsync();
        await SeedSubjectsAsync();
        await _context.SaveChangesAsync();
    }

    private async Task SeedSchoolsAsync()
    {
        if (!await Schools.AnyAsync())
        {
            var schools = new List<School>
            {
                new School { Id = 1, Name = "Lincoln High School", Address = "123 Main St", City = "Springfield", State = "IL" },
                new School { Id = 2, Name = "Washington Elementary", Address = "456 Oak Ave", City = "Springfield", State = "IL" }
            };

            await Schools.AddRangeAsync(schools);
        }
    }

    private async Task SeedSchoolYearsAsync()
    {
        if (!await SchoolYears.AnyAsync())
        {
            var schoolYears = new List<SchoolYear>
            {
                new SchoolYear { Id = 1, Name = "2024-2025", StartDate = new DateTime(2024, 8, 15), EndDate = new DateTime(2025, 6, 15) },
                new SchoolYear { Id = 2, Name = "2025-2026", StartDate = new DateTime(2025, 8, 15), EndDate = new DateTime(2026, 6, 15) }
            };

            await SchoolYears.AddRangeAsync(schoolYears);
        }
    }

    private async Task SeedTermsAsync()
    {
        if (!await Terms.AnyAsync())
        {
            var terms = new List<Term>
            {
                new Term { Id = 1, Name = "Fall 2024", StartDate = new DateTime(2024, 8, 15), EndDate = new DateTime(2024, 12, 20), SchoolYearId = 1 },
                new Term { Id = 2, Name = "Spring 2025", StartDate = new DateTime(2025, 1, 15), EndDate = new DateTime(2025, 6, 15), SchoolYearId = 1 },
                new Term { Id = 3, Name = "Fall 2025", StartDate = new DateTime(2025, 8, 15), EndDate = new DateTime(2025, 12, 20), SchoolYearId = 2 },
                new Term { Id = 4, Name = "Spring 2026", StartDate = new DateTime(2026, 1, 15), EndDate = new DateTime(2026, 6, 15), SchoolYearId = 2 }
            };

            await Terms.AddRangeAsync(terms);
        }
    }

    private async Task SeedTeachersAsync()
    {
        if (!await Teachers.AnyAsync())
        {
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, FullName = "John Smith", Email = "john.smith@lincoln.edu", SchoolId = 1},
                new Teacher { Id = 2, FullName = "Jane Doe", Email = "jane.doe@lincoln.edu", SchoolId = 1},
                new Teacher { Id = 3, FullName = "Bob Johnson", Email = "bob.johnson@washington.edu", SchoolId = 2 },
                new Teacher { Id = 4, FullName = "Alice Brown", Email = "alice.brown@washington.edu", SchoolId = 2 }
            };

            await Teachers.AddRangeAsync(teachers);
        }
    }

    private async Task SeedStudentsAsync()
    {
        if (!await Students.AnyAsync())
        {
            var students = new List<Student>
            {
                new Student { Id = 1, FullName = "Michael Wilson", Email = "michael.wilson@student.lincoln.edu", SchoolId = 1 },
                new Student { Id = 2, FullName = "Sarah Davis", Email = "sarah.davis@student.lincoln.edu", SchoolId = 1 },
                new Student { Id = 3, FullName = "David Miller", Email = "david.miller@student.lincoln.edu", SchoolId = 1 },
                new Student { Id = 4, FullName = "Emma Garcia", Email = "emma.garcia@student.washington.edu", SchoolId = 2 },
                new Student { Id = 5, FullName = "James Martinez", Email = "james.martinez@student.washington.edu", SchoolId = 2 }
            };

            await Students.AddRangeAsync(students);
        }
    }

    private async Task SeedSubjectsAsync()
    {
        if (!await Subjects.AnyAsync())
        {
            // Seed term-agnostic subjects
            var subjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "Algebra I", Code = "MATH101", Description = "Introduction to Algebra", Credits = 3 },
                new Subject { Id = 2, Name = "English Literature", Code = "ENG201", Description = "Classical Literature Study", Credits = 3 },
                new Subject { Id = 3, Name = "Chemistry", Code = "SCI301", Description = "Basic Chemistry Principles", Credits = 4 },
                new Subject { Id = 4, Name = "Elementary Math", Code = "MATH001", Description = "Basic Math for Elementary Students", Credits = 2 },
                new Subject { Id = 5, Name = "Reading Fundamentals", Code = "ENG001", Description = "Basic Reading Skills", Credits = 2 }
            };

            await Subjects.AddRangeAsync(subjects);

            // Seed enrollments (linking students, subjects, terms, and teachers)
            var enrollments = new List<Enrollment>
            {
                // High School enrollments (Term 1 - Fall 2024)
                new Enrollment { Id = 1, StudentId = 1, SubjectId = 1, TermId = 1, TeacherId = 1, EnrollmentDate = DateTime.UtcNow.AddMonths(-2), IsActive = true },
                new Enrollment { Id = 2, StudentId = 1, SubjectId = 2, TermId = 1, TeacherId = 2, EnrollmentDate = DateTime.UtcNow.AddMonths(-2), IsActive = true },
                new Enrollment { Id = 3, StudentId = 2, SubjectId = 1, TermId = 1, TeacherId = 1, EnrollmentDate = DateTime.UtcNow.AddMonths(-2), IsActive = true },
                new Enrollment { Id = 4, StudentId = 2, SubjectId = 3, TermId = 2, TeacherId = 1, EnrollmentDate = DateTime.UtcNow.AddMonths(-1), IsActive = true },
                
                // Elementary School enrollments (Term 1 - Fall 2024)
                new Enrollment { Id = 5, StudentId = 3, SubjectId = 4, TermId = 1, TeacherId = 3, EnrollmentDate = DateTime.UtcNow.AddMonths(-2), IsActive = true },
                new Enrollment { Id = 6, StudentId = 3, SubjectId = 5, TermId = 1, TeacherId = 4, EnrollmentDate = DateTime.UtcNow.AddMonths(-2), IsActive = true },
                new Enrollment { Id = 7, StudentId = 4, SubjectId = 4, TermId = 1, TeacherId = 3, EnrollmentDate = DateTime.UtcNow.AddMonths(-2), IsActive = true },
                new Enrollment { Id = 8, StudentId = 4, SubjectId = 5, TermId = 1, TeacherId = 4, EnrollmentDate = DateTime.UtcNow.AddMonths(-2), IsActive = true },
            };

            await _context.Enrollments.AddRangeAsync(enrollments);
        }
    }
}

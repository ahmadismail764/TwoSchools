using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Infra.Persistence;
namespace TwoSchools.Infra.Seeders;

internal class OrganizationSeeder : IOrganizationSeeder
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

    public async Task Seed()
    {
        await SeedSchoolsAsync();
        await SeedSchoolYearsAsync();
        await SeedTermsAsync();
        await SeedTeachersAsync();
        await SeedStudentsAsync();
        await SeedSubjectsAsync();
        // Note: Enrollments are seeded within SeedSubjectsAsync() after all other entities exist
    }

    private async Task SeedSchoolsAsync()
    {
        if (!await Schools.AnyAsync())
        {
            var schools = new List<School>
            {
                new School 
                { 
                    Name = "Lincoln High School", 
                    Address = "123 Main St, Springfield, IL 62701",
                    PhoneNumber = "+1-217-555-0123",
                    Email = "admin@lincolnhigh.edu",
                    Website = "https://lincolnhigh.edu"
                },
                new School 
                { 
                    Name = "Washington Elementary", 
                    Address = "456 Oak Ave, Springfield, IL 62702",
                    PhoneNumber = "+1-217-555-0456",
                    Email = "office@washington.edu",
                    Website = "https://washington.edu"
                }
            };

            await Schools.AddRangeAsync(schools);
            await _context.SaveChangesAsync(); // Save to get auto-generated IDs
        }
    }

    private async Task SeedSchoolYearsAsync()
    {
        if (!await SchoolYears.AnyAsync())
        {
            var school1 = await Schools.FirstAsync(s => s.Name == "Lincoln High School");
            
            var schoolYears = new List<SchoolYear>
            {
                new SchoolYear 
                { 
                    Year = 2024, 
                    StartDate = new DateTime(2024, 8, 15), 
                    EndDate = new DateTime(2025, 6, 15),
                    SchoolId = school1.Id
                },
                new SchoolYear
                {
                    Year = 2025,
                    StartDate = new DateTime(2025, 8, 15),
                    EndDate = new DateTime(2026, 6, 15),
                    SchoolId = school1.Id
                }
            };

            await SchoolYears.AddRangeAsync(schoolYears);
            await _context.SaveChangesAsync(); // Save to get auto-generated IDs
        }
    }

    private async Task SeedTermsAsync()
    {
        if (!await Terms.AnyAsync())
        {
            var schoolYear2024 = await SchoolYears.FirstAsync(sy => sy.Year == 2024);
            var schoolYear2025 = await SchoolYears.FirstAsync(sy => sy.Year == 2025);
            
            var terms = new List<Term>
            {
                new Term { Name = "Fall 2024", StartDate = new DateTime(2024, 8, 15), EndDate = new DateTime(2024, 12, 20), SchoolYearId = schoolYear2024.Id },
                new Term { Name = "Spring 2025", StartDate = new DateTime(2025, 1, 15), EndDate = new DateTime(2025, 6, 15), SchoolYearId = schoolYear2024.Id },
                new Term { Name = "Fall 2025", StartDate = new DateTime(2025, 8, 15), EndDate = new DateTime(2025, 12, 20), SchoolYearId = schoolYear2025.Id },
                new Term { Name = "Spring 2026", StartDate = new DateTime(2026, 1, 15), EndDate = new DateTime(2026, 6, 15), SchoolYearId = schoolYear2025.Id }
            };

            await Terms.AddRangeAsync(terms);
            await _context.SaveChangesAsync(); // Save to get auto-generated IDs
        }
    }

    private async Task SeedTeachersAsync()
    {
        if (!await Teachers.AnyAsync())
        {
            var school1 = await Schools.FirstAsync(s => s.Name == "Lincoln High School");
            var school2 = await Schools.FirstAsync(s => s.Name == "Washington Elementary");
            
            var teachers = new List<Teacher>
            {
                new Teacher { FullName = "John Smith", Email = "john.smith@lincoln.edu", SchoolId = school1.Id},
                new Teacher { FullName = "Jane Doe", Email = "jane.doe@lincoln.edu", SchoolId = school1.Id},
                new Teacher { FullName = "Bob Johnson", Email = "bob.johnson@washington.edu", SchoolId = school2.Id },
                new Teacher { FullName = "Alice Brown", Email = "alice.brown@washington.edu", SchoolId = school2.Id }
            };

            await Teachers.AddRangeAsync(teachers);
            await _context.SaveChangesAsync(); // Save to get auto-generated IDs
        }
    }

    private async Task SeedStudentsAsync()
    {
        if (!await Students.AnyAsync())
        {
            var school1 = await Schools.FirstAsync(s => s.Name == "Lincoln High School");
            var school2 = await Schools.FirstAsync(s => s.Name == "Washington Elementary");
            
            var students = new List<Student>
            {
                new Student { FullName = "Michael Wilson", Email = "michael.wilson@student.lincoln.edu", SchoolId = school1.Id },
                new Student { FullName = "Sarah Davis", Email = "sarah.davis@student.lincoln.edu", SchoolId = school1.Id },
                new Student { FullName = "David Miller", Email = "david.miller@student.lincoln.edu", SchoolId = school1.Id },
                new Student { FullName = "Emma Garcia", Email = "emma.garcia@student.washington.edu", SchoolId = school2.Id },
                new Student { FullName = "James Martinez", Email = "james.martinez@student.washington.edu", SchoolId = school2.Id }
            };

            await Students.AddRangeAsync(students);
            await _context.SaveChangesAsync(); // Save to get auto-generated IDs
        }
    }

    private async Task SeedSubjectsAsync()
    {
        if (!await Subjects.AnyAsync())
        {
            // Seed term-agnostic subjects
            var subjects = new List<Subject>
            {
                new Subject { Name = "Algebra I", Code = "MATH101", Description = "Introduction to Algebra", Credits = 3 },
                new Subject { Name = "English Literature", Code = "ENG201", Description = "Classical Literature Study", Credits = 3 },
                new Subject { Name = "Chemistry", Code = "SCI301", Description = "Basic Chemistry Principles", Credits = 4 },
                new Subject { Name = "Elementary Math", Code = "MATH001", Description = "Basic Math for Elementary Students", Credits = 2 },
                new Subject { Name = "Reading Fundamentals", Code = "ENG001", Description = "Basic Reading Skills", Credits = 2 }
            };

            await Subjects.AddRangeAsync(subjects);
            await _context.SaveChangesAsync(); // Save to get auto-generated IDs

            // Now seed enrollments using the saved entities
            await SeedEnrollmentsAsync();
        }
    }

    private async Task SeedEnrollmentsAsync()
    {
        // Get all the saved entities with their IDs
        var students = await Students.ToListAsync();
        var subjects = await Subjects.ToListAsync();
        var teachers = await Teachers.ToListAsync();
        var fallTerm = await Terms.FirstAsync(t => t.Name == "Fall 2024");
        var springTerm = await Terms.FirstAsync(t => t.Name == "Spring 2025");

        var enrollments = new List<Enrollment>
        {
            // High school students (Michael, Sarah, David)
            new Enrollment {
                StudentId = students.First(s => s.FullName == "Michael Wilson").Id,
                SubjectId = subjects.First(s => s.Code == "MATH101").Id,
                TermId = fallTerm.Id,
                TeacherId = teachers.First(t => t.FullName == "John Smith").Id,
                EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                IsActive = true
            },
            new Enrollment {
                StudentId = students.First(s => s.FullName == "Sarah Davis").Id,
                SubjectId = subjects.First(s => s.Code == "ENG201").Id,
                TermId = fallTerm.Id,
                TeacherId = teachers.First(t => t.FullName == "Jane Doe").Id,
                EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                IsActive = true
            },
            
            // Elementary students (Emma, James)
            new Enrollment {
                StudentId = students.First(s => s.FullName == "Emma Garcia").Id,
                SubjectId = subjects.First(s => s.Code == "MATH001").Id,
                TermId = fallTerm.Id,
                TeacherId = teachers.First(t => t.FullName == "Bob Johnson").Id,
                EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                IsActive = true
            },
            new Enrollment {
                StudentId = students.First(s => s.FullName == "James Martinez").Id,
                SubjectId = subjects.First(s => s.Code == "ENG001").Id,
                TermId = fallTerm.Id,
                TeacherId = teachers.First(t => t.FullName == "Alice Brown").Id,
                EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                IsActive = true
            }
        };

        await Enrollments.AddRangeAsync(enrollments);
        await _context.SaveChangesAsync();
    }
}

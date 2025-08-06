using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
namespace TwoSchools.Infra.Persistence;

public class SchoolDBContext : DbContext
{
    public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options) { }

    public DbSet<School> Schools { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<Term> Terms { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Enrollment relationships
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Subject)
            .WithMany(sub => sub.Enrollments)
            .HasForeignKey(e => e.SubjectId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Term)
            .WithMany(t => t.Enrollments)
            .HasForeignKey(e => e.TermId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Teacher)
            .WithMany(t => t.Enrollments)
            .HasForeignKey(e => e.TeacherId);

        // Configure unique constraint for Student-Subject-Term enrollment
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.SubjectId, e.TermId })
            .IsUnique();

        // Configure Student-School relationship
        modelBuilder.Entity<Student>()
            .HasOne(s => s.School)
            .WithMany(sc => sc.Students)
            .HasForeignKey(s => s.SchoolId);

        // Configure Teacher-School relationship
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.School)
            .WithMany(sc => sc.Teachers)
            .HasForeignKey(t => t.SchoolId);

        // Configure Term-SchoolYear relationship
        modelBuilder.Entity<Term>()
            .HasOne(term => term.SchoolYear)
            .WithMany(sy => sy.Terms)
            .HasForeignKey(term => term.SchoolYearId);

        base.OnModelCreating(modelBuilder);
    }
}
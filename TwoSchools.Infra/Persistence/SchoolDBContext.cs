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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure many-to-many relationship between Students and Subjects
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Subjects)
            .WithMany(sub => sub.Students)
            .UsingEntity(j => j.ToTable("StudentSubjects"));

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

        // Configure Subject-Teacher relationship
        modelBuilder.Entity<Subject>()
            .HasOne(sub => sub.Teacher)
            .WithMany(t => t.Subjects)
            .HasForeignKey(sub => sub.TeacherId);

        // Configure Subject-Term relationship
        modelBuilder.Entity<Subject>()
            .HasOne(sub => sub.Term)
            .WithMany(term => term.Subjects)
            .HasForeignKey(sub => sub.TermId);

        // Configure Term-SchoolYear relationship
        modelBuilder.Entity<Term>()
            .HasOne(term => term.SchoolYear)
            .WithMany(sy => sy.Terms)
            .HasForeignKey(term => term.SchoolYearId);

        base.OnModelCreating(modelBuilder);
    }
}
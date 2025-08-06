using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;

namespace TwoSchools.Infra.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(SchoolDBContext context) : base(context) { }

    public async Task<IEnumerable<Student>> GetStudentsBySchoolAsync(int schoolId)
    {
        return await _context.Students
            .Where(s => s.SchoolId == schoolId)
            .Include(s => s.School)
            .ToListAsync();
    }

    public async Task<Student?> GetStudentWithEnrollmentsAsync(int studentId)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Subject)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Term)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Teacher)
            .Include(s => s.School)
            .FirstOrDefaultAsync(s => s.Id == studentId);
    }

    public async Task<IEnumerable<Student>> GetStudentsWithEnrollmentsAsync()
    {
        return await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Subject)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Term)
            .Include(s => s.School)
            .ToListAsync();
    }

    public async Task<Student?> GetStudentByEmailAsync(string email)
    {
        return await _context.Students
            .Include(s => s.School)
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
    {
        return await _context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Include(e => e.Subject)
            .Include(e => e.Term)
            .Include(e => e.Teacher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsBySubjectAsync(int subjectId)
    {
        return await _context.Students
            .Where(s => s.Enrollments.Any(e => e.SubjectId == subjectId && e.IsActive))
            .Include(s => s.School)
            .ToListAsync();
    }
}
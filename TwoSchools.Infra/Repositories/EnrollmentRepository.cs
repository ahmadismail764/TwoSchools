using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;

namespace TwoSchools.Infra.Repositories;

public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(SchoolDBContext context) : base(context) { }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
    {
        return await _context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Include(e => e.Subject)
            .Include(e => e.Term)
                .ThenInclude(t => t.SchoolYear)
            .Include(e => e.Teacher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsBySubjectAsync(int subjectId)
    {
        return await _context.Enrollments
            .Where(e => e.SubjectId == subjectId)
            .Include(e => e.Student)
                .ThenInclude(s => s.School)
            .Include(e => e.Term)
            .Include(e => e.Teacher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByTermAsync(int termId)
    {
        return await _context.Enrollments
            .Where(e => e.TermId == termId)
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .Include(e => e.Teacher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByTeacherAsync(int teacherId)
    {
        return await _context.Enrollments
            .Where(e => e.TeacherId == teacherId)
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .Include(e => e.Term)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetActiveEnrollmentsByStudentAsync(int studentId)
    {
        return await _context.Enrollments
            .Where(e => e.StudentId == studentId && e.IsActive)
            .Include(e => e.Subject)
            .Include(e => e.Term)
                .ThenInclude(t => t.SchoolYear)
            .Include(e => e.Teacher)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetEnrollmentAsync(int studentId, int subjectId, int termId)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .Include(e => e.Term)
            .Include(e => e.Teacher)
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.SubjectId == subjectId && e.TermId == termId);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithGradesAsync(int studentId)
    {
        return await _context.Enrollments
            .Where(e => e.StudentId == studentId && e.Grade.HasValue)
            .Include(e => e.Subject)
            .ToListAsync();
    }

    public async Task<decimal?> GetAverageGradeAsync(int studentId)
    {
        var grades = await _context.Enrollments
            .Where(e => e.StudentId == studentId && e.Grade.HasValue)
            .Select(e => e.Grade!.Value)
            .ToListAsync();

        return grades.Any() ? grades.Average() : null;
    }

    public async Task<bool> IsStudentEnrolledAsync(int studentId, int subjectId, int termId)
    {
        return await _context.Enrollments
            .AnyAsync(e => e.StudentId == studentId && e.SubjectId == subjectId && e.TermId == termId && e.IsActive);
    }

    public async Task WithdrawStudentAsync(int studentId, int subjectId, int termId)
    {
        var enrollment = await GetEnrollmentAsync(studentId, subjectId, termId);
        if (enrollment != null)
        {
            enrollment.IsActive = false;
            await UpdateAsync(enrollment);
        }
    }
}

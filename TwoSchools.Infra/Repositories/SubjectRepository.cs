using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;

namespace TwoSchools.Infra.Repositories;

public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
{
    public SubjectRepository(SchoolDBContext context) : base(context) { }

    public async Task<IEnumerable<Subject>> GetSubjectsByTermAsync(int termId)
    {
        return await _context.Enrollments
            .Where(e => e.TermId == termId)
            .Select(e => e.Subject)
            .Distinct()
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Teacher)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId)
    {
        return await _context.Enrollments
            .Where(e => e.TeacherId == teacherId)
            .Select(e => e.Subject)
            .Distinct()
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Term)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
            .ToListAsync();
    }

    public async Task<Subject?> GetSubjectWithStudentsAsync(int subjectId)
    {
        return await _context.Subjects
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                    .ThenInclude(st => st.School)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Teacher)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Term)
            .FirstOrDefaultAsync(s => s.Id == subjectId);
    }

    public async Task<Subject?> GetSubjectWithDetailsAsync(int subjectId)
    {
        return await _context.Subjects
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Term)
                    .ThenInclude(t => t.SchoolYear)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Teacher)
                    .ThenInclude(t => t.School)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                    .ThenInclude(st => st.School)
            .FirstOrDefaultAsync(s => s.Id == subjectId);
    }

    public async Task<IEnumerable<Subject>> GetSubjectsBySchoolYearAsync(int schoolYearId)
    {
        return await _context.Enrollments
            .Include(e => e.Term)
            .Where(e => e.Term.SchoolYearId == schoolYearId)
            .Select(e => e.Subject)
            .Distinct()
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Teacher)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
            .ToListAsync();
    }

    public async Task<Subject?> GetSubjectByCodeAsync(string code)
    {
        return await _context.Subjects
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Term)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Teacher)
            .FirstOrDefaultAsync(s => s.Code == code);
    }
}
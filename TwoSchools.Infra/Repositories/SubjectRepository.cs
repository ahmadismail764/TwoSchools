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
        return await _context.Subjects
            .Where(s => s.TermId == termId)
            .Include(s => s.Teacher)
            .Include(s => s.Students)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId)
    {
        return await _context.Subjects
            .Where(s => s.TeacherId == teacherId)
            .Include(s => s.Term)
            .Include(s => s.Students)
            .ToListAsync();
    }

    public async Task<Subject?> GetSubjectWithStudentsAsync(int subjectId)
    {
        return await _context.Subjects
            .Include(s => s.Students)
                .ThenInclude(st => st.School)
            .Include(s => s.Teacher)
            .Include(s => s.Term)
            .FirstOrDefaultAsync(s => s.Id == subjectId);
    }

    public async Task<Subject?> GetSubjectWithDetailsAsync(int subjectId)
    {
        return await _context.Subjects
            .Include(s => s.Term)
                .ThenInclude(t => t.SchoolYear)
            .Include(s => s.Teacher)
                .ThenInclude(t => t.School)
            .Include(s => s.Students)
                .ThenInclude(st => st.School)
            .FirstOrDefaultAsync(s => s.Id == subjectId);
    }

    public async Task<IEnumerable<Subject>> GetSubjectsBySchoolYearAsync(int schoolYearId)
    {
        return await _context.Subjects
            .Include(s => s.Term)
            .Where(s => s.Term.SchoolYearId == schoolYearId)
            .Include(s => s.Teacher)
            .Include(s => s.Students)
            .ToListAsync();
    }

    public async Task<Subject?> GetSubjectByCodeAsync(string code)
    {
        return await _context.Subjects
            .Include(s => s.Term)
            .Include(s => s.Teacher)
            .FirstOrDefaultAsync(s => s.Code == code);
    }
}
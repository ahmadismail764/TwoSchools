using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;

namespace TwoSchools.Infra.Repositories;

public class TermRepository : BaseRepository<Term>, ITermRepository
{
    public TermRepository(SchoolDBContext context) : base(context) { }

    public async Task<IEnumerable<Term>> GetTermsBySchoolYearAsync(int schoolYearId)
    {
        return await _context.Terms
            .Where(t => t.SchoolYearId == schoolYearId)
            .Include(t => t.SchoolYear)
            .OrderBy(t => t.StartDate)
            .ToListAsync();
    }

    public async Task<Term?> GetCurrentTermAsync()
    {
        var currentDate = DateTime.Now;
        return await _context.Terms
            .Include(t => t.SchoolYear)
            .Include(t => t.Subjects)
            .FirstOrDefaultAsync(t => t.StartDate <= currentDate && t.EndDate >= currentDate);
    }

    public async Task<Term?> GetTermWithSubjectsAsync(int termId)
    {
        return await _context.Terms
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Teacher)
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Students)
            .Include(t => t.SchoolYear)
            .FirstOrDefaultAsync(t => t.Id == termId);
    }

    public async Task<IEnumerable<Term>> GetTermsWithSubjectsAsync()
    {
        return await _context.Terms
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Teacher)
            .Include(t => t.SchoolYear)
            .OrderBy(t => t.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTermAsync(int termId)
    {
        return await _context.Subjects
            .Where(s => s.TermId == termId)
            .Include(s => s.Teacher)
            .Include(s => s.Students)
            .ToListAsync();
    }

    public async Task<Term?> GetTermByNameAsync(string name)
    {
        return await _context.Terms
            .Include(t => t.SchoolYear)
            .FirstOrDefaultAsync(t => t.Name == name);
    }
}
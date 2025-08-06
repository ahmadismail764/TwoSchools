using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;

namespace TwoSchools.Infra.Repositories;

public class SchoolYearRepository : BaseRepository<SchoolYear>, ISchoolYearRepository
{
    public SchoolYearRepository(SchoolDBContext context) : base(context) { }

    public async Task<SchoolYear?> GetCurrentSchoolYearAsync()
    {
        var currentDate = DateTime.Now;
        return await _context.SchoolYears
            .Include(sy => sy.Terms)
            .FirstOrDefaultAsync(sy => sy.StartDate <= currentDate && sy.EndDate >= currentDate);
    }

    public async Task<SchoolYear?> GetSchoolYearWithTermsAsync(int schoolYearId)
    {
        return await _context.SchoolYears
            .Include(sy => sy.Terms)
                .ThenInclude(t => t.Subjects)
            .FirstOrDefaultAsync(sy => sy.Id == schoolYearId);
    }

    public async Task<IEnumerable<SchoolYear>> GetSchoolYearsWithTermsAsync()
    {
        return await _context.SchoolYears
            .Include(sy => sy.Terms)
            .OrderByDescending(sy => sy.StartDate)
            .ToListAsync();
    }

    public async Task<SchoolYear?> GetSchoolYearByNameAsync(string name)
    {
        return await _context.SchoolYears
            .Include(sy => sy.Terms)
            .FirstOrDefaultAsync(sy => sy.Name == name);
    }

    public async Task<IEnumerable<Term>> GetTermsBySchoolYearAsync(int schoolYearId)
    {
        return await _context.Terms
            .Where(t => t.SchoolYearId == schoolYearId)
            .Include(t => t.Subjects)
            .OrderBy(t => t.StartDate)
            .ToListAsync();
    }
}
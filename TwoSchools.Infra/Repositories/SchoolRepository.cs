using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;

namespace TwoSchools.Infra.Repositories;

public class SchoolRepository : BaseRepository<School>, ISchoolRepository
{
    public SchoolRepository(SchoolDBContext context) : base(context) { }

    public async Task<IEnumerable<School>> GetSchoolsWithTeachersAsync()
    {
        return await _context.Schools
            .Include(s => s.Teachers)
            .ToListAsync();
    }

    public async Task<IEnumerable<School>> GetSchoolsWithStudentsAsync()
    {
        return await _context.Schools
            .Include(s => s.Students)
            .ToListAsync();
    }

    public async Task<School?> GetSchoolWithDetailsAsync(int schoolId)
    {
        return await _context.Schools
            .Include(s => s.Teachers)
            .Include(s => s.Students)
            .FirstOrDefaultAsync(s => s.Id == schoolId);
    }

    public async Task<IEnumerable<Teacher>> GetTeachersBySchoolAsync(int schoolId)
    {
        return await _context.Teachers
            .Where(t => t.SchoolId == schoolId)
            .Include(t => t.Subjects)
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsBySchoolAsync(int schoolId)
    {
        return await _context.Students
            .Where(s => s.SchoolId == schoolId)
            .Include(s => s.Subjects)
            .ToListAsync();
    }
}
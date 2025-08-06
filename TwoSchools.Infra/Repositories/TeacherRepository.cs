using Microsoft.EntityFrameworkCore;
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
using TwoSchools.Infra.Persistence;

namespace TwoSchools.Infra.Repositories;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(SchoolDBContext context) : base(context) { }

    public async Task<IEnumerable<Teacher>> GetTeachersBySchoolAsync(int schoolId)
    {
        return await _context.Teachers
            .Where(t => t.SchoolId == schoolId)
            .Include(t => t.School)
            .ToListAsync();
    }

    public async Task<Teacher?> GetTeacherWithSubjectsAsync(int teacherId)
    {
        return await _context.Teachers
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Term)
            .Include(t => t.School)
            .FirstOrDefaultAsync(t => t.Id == teacherId);
    }

    public async Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync()
    {
        return await _context.Teachers
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Term)
            .Include(t => t.School)
            .ToListAsync();
    }

    public async Task<Teacher?> GetTeacherByEmailAsync(string email)
    {
        return await _context.Teachers
            .Include(t => t.School)
            .FirstOrDefaultAsync(t => t.Email == email);
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId)
    {
        return await _context.Subjects
            .Where(s => s.TeacherId == teacherId)
            .Include(s => s.Term)
            .Include(s => s.Students)
            .ToListAsync();
    }
}
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

    public async Task<Student?> GetStudentWithSubjectsAsync(int studentId)
    {
        return await _context.Students
            .Include(s => s.Subjects)
                .ThenInclude(sub => sub.Term)
            .Include(s => s.Subjects)
                .ThenInclude(sub => sub.Teacher)
            .Include(s => s.School)
            .FirstOrDefaultAsync(s => s.Id == studentId);
    }

    public async Task<IEnumerable<Student>> GetStudentsWithSubjectsAsync()
    {
        return await _context.Students
            .Include(s => s.Subjects)
                .ThenInclude(sub => sub.Term)
            .Include(s => s.School)
            .ToListAsync();
    }

    public async Task<Student?> GetStudentByEmailAsync(string email)
    {
        return await _context.Students
            .Include(s => s.School)
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByStudentAsync(int studentId)
    {
        return await _context.Students
            .Where(s => s.Id == studentId)
            .SelectMany(s => s.Subjects)
            .Include(sub => sub.Term)
            .Include(sub => sub.Teacher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsBySubjectAsync(int subjectId)
    {
        return await _context.Students
            .Where(s => s.Subjects.Any(sub => sub.Id == subjectId))
            .Include(s => s.School)
            .ToListAsync();
    }
}
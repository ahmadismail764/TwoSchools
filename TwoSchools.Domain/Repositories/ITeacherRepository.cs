using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface ITeacherRepository : IBaseRepository<Teacher>
{
    Task<IEnumerable<Teacher>> GetTeachersBySchoolAsync(int schoolId);
    Task<Teacher?> GetTeacherWithSubjectsAsync(int teacherId);
    Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync();
    Task<Teacher?> GetTeacherByEmailAsync(string email);
    Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId);
}
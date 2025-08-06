using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface ISchoolRepository : IBaseRepository<School>
{
    Task<IEnumerable<School>> GetSchoolsWithTeachersAsync();
    Task<IEnumerable<School>> GetSchoolsWithStudentsAsync();
    Task<School?> GetSchoolWithDetailsAsync(int schoolId);
    Task<IEnumerable<Teacher>> GetTeachersBySchoolAsync(int schoolId);
    Task<IEnumerable<Student>> GetStudentsBySchoolAsync(int schoolId);
}
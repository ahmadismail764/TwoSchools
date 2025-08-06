using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsBySchoolAsync(int schoolId);
    Task<Student?> GetStudentWithSubjectsAsync(int studentId);
    Task<IEnumerable<Student>> GetStudentsWithSubjectsAsync();
    Task<Student?> GetStudentByEmailAsync(string email);
    Task<IEnumerable<Subject>> GetSubjectsByStudentAsync(int studentId);
    Task<IEnumerable<Student>> GetStudentsBySubjectAsync(int subjectId);
}
using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsBySchoolAsync(int schoolId);
    Task<Student?> GetStudentWithEnrollmentsAsync(int studentId);
    Task<IEnumerable<Student>> GetStudentsWithEnrollmentsAsync();
    Task<Student?> GetStudentByEmailAsync(string email);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId);
    Task<IEnumerable<Student>> GetStudentsBySubjectAsync(int subjectId);
}
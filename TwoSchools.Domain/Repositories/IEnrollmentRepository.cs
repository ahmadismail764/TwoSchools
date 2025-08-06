using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface IEnrollmentRepository : IBaseRepository<Enrollment>
{
    Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsBySubjectAsync(int subjectId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByTermAsync(int termId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByTeacherAsync(int teacherId);
    Task<IEnumerable<Enrollment>> GetActiveEnrollmentsByStudentAsync(int studentId);
    Task<Enrollment?> GetEnrollmentAsync(int studentId, int subjectId, int termId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsWithGradesAsync(int studentId);
    Task<decimal?> GetAverageGradeAsync(int studentId);
    Task<bool> IsStudentEnrolledAsync(int studentId, int subjectId, int termId);
    Task WithdrawStudentAsync(int studentId, int subjectId, int termId);
}

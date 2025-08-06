using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface ISubjectRepository : IBaseRepository<Subject>
{
    Task<IEnumerable<Subject>> GetSubjectsByTermAsync(int termId);
    Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId);
    Task<Subject?> GetSubjectWithStudentsAsync(int subjectId);
    Task<Subject?> GetSubjectWithDetailsAsync(int subjectId); // Includes Term, Teacher, Students
    Task<IEnumerable<Subject>> GetSubjectsBySchoolYearAsync(int schoolYearId);
    Task<Subject?> GetSubjectByCodeAsync(string code);
}
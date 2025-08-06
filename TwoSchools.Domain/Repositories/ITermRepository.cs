using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface ITermRepository : IBaseRepository<Term>
{
    Task<IEnumerable<Term>> GetTermsBySchoolYearAsync(int schoolYearId);
    Task<Term?> GetCurrentTermAsync();
    Task<Term?> GetTermWithSubjectsAsync(int termId);
    Task<IEnumerable<Term>> GetTermsWithSubjectsAsync();
    Task<IEnumerable<Subject>> GetSubjectsByTermAsync(int termId);
}
using TwoSchools.Domain.Entities;

namespace TwoSchools.Domain.Repositories;

public interface ISchoolYearRepository : IBaseRepository<SchoolYear>
{
    Task<SchoolYear?> GetCurrentSchoolYearAsync();
    Task<SchoolYear?> GetSchoolYearWithTermsAsync(int schoolYearId);
    Task<IEnumerable<SchoolYear>> GetSchoolYearsWithTermsAsync();
    Task<IEnumerable<Term>> GetTermsBySchoolYearAsync(int schoolYearId);
}
using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;

namespace TwoSchools.App.Services;

public class TermService
{
    private readonly ITermRepository _termRepository;
    private readonly ISchoolYearRepository _schoolYearRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public TermService(
        ITermRepository termRepository,
        ISchoolYearRepository schoolYearRepository,
        IEnrollmentRepository enrollmentRepository)
    {
        _termRepository = termRepository;
        _schoolYearRepository = schoolYearRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Term?> GetCurrentTermAsync()
    {
        return await _termRepository.GetCurrentTermAsync();
    }

    public async Task<IEnumerable<Term>> GetAllTermsAsync()
    {
        return await _termRepository.GetAllAsync();
    }

    public async Task<Term?> GetTermByIdAsync(int id)
    {
        return await _termRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Term>> GetTermsBySchoolYearAsync(int schoolYearId)
    {
        return await _termRepository.GetTermsBySchoolYearAsync(schoolYearId);
    }

    public async Task<Term> CreateTermAsync(Term term)
    {
        // Business rule: Validate school year exists
        var schoolYear = await _schoolYearRepository.GetByIdAsync(term.SchoolYearId);
        if (schoolYear == null)
            throw new ArgumentException("School year not found");

        // Business rule: Term dates must be within school year
        if (term.StartDate < schoolYear.StartDate || term.EndDate > schoolYear.EndDate)
            throw new ArgumentException("Term dates must be within the school year period");

        // Business rule: Start date must be before end date
        if (term.StartDate >= term.EndDate)
            throw new ArgumentException("Term start date must be before end date");

        // Business rule: Term name must be unique within the school year
        var existingTerms = await _termRepository.GetTermsBySchoolYearAsync(term.SchoolYearId);
        if (existingTerms.Any(t => t.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Term with name '{term.Name}' already exists in this school year");

        return await _termRepository.AddAsync(term);
    }

    public async Task<Term> UpdateTermAsync(Term term)
    {
        var existingTerm = await _termRepository.GetByIdAsync(term.Id);
        if (existingTerm == null)
            throw new ArgumentException("Term not found");

        // Business rule: Can't change dates if there are active enrollments
        if ((existingTerm.StartDate != term.StartDate || existingTerm.EndDate != term.EndDate))
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByTermAsync(term.Id);
            var activeEnrollments = enrollments.Where(e => e.IsActive).ToList();
            if (activeEnrollments.Any())
                throw new InvalidOperationException("Cannot change term dates while there are active enrollments");
        }

        await _termRepository.UpdateAsync(term);
        return term;
    }

    public async Task DeleteTermAsync(int id)
    {
        // Business rule: Can't delete term with enrollments
        var enrollments = await _enrollmentRepository.GetEnrollmentsByTermAsync(id);
        if (enrollments.Any())
            throw new InvalidOperationException("Cannot delete term with existing enrollments");

        await _termRepository.DeleteAsync(id);
    }

    public async Task<int> GetTermEnrollmentCountAsync(int termId)
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsByTermAsync(termId);
        return enrollments.Count(e => e.IsActive);
    }

    public async Task<IEnumerable<Term>> GetActiveTermsAsync()
    {
        var allTerms = await _termRepository.GetAllAsync();
        var currentDate = DateTime.Now;
        return allTerms.Where(t => t.StartDate <= currentDate && t.EndDate >= currentDate);
    }

    public async Task<IEnumerable<Term>> GetUpcomingTermsAsync()
    {
        var allTerms = await _termRepository.GetAllAsync();
        var currentDate = DateTime.Now;
        return allTerms.Where(t => t.StartDate > currentDate).OrderBy(t => t.StartDate);
    }
}
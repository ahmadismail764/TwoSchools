using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;

namespace TwoSchools.App.Services;

public class SchoolYearService
{
    private readonly ISchoolYearRepository _schoolYearRepository;

    public SchoolYearService(ISchoolYearRepository schoolYearRepository)
    {
        _schoolYearRepository = schoolYearRepository;
    }

    public async Task<IEnumerable<SchoolYear>> GetAllSchoolYearsAsync()
    {
        return await _schoolYearRepository.GetAllAsync();
    }

    public async Task<SchoolYear?> GetSchoolYearByIdAsync(int id)
    {
        return await _schoolYearRepository.GetByIdAsync(id);
    }

    public async Task<SchoolYear?> GetCurrentSchoolYearAsync()
    {
        return await _schoolYearRepository.GetCurrentSchoolYearAsync();
    }

    public async Task<SchoolYear> CreateSchoolYearAsync(SchoolYear schoolYear)
    {
        // Business rule: Validate dates
        if (schoolYear.EndDate <= schoolYear.StartDate)
            throw new ArgumentException("End date must be after start date");

        // Business rule: School year cannot overlap with existing ones for the same school
        var existingSchoolYears = await _schoolYearRepository.GetAllAsync();
        var overlapping = existingSchoolYears.Any(sy =>
            sy.SchoolId == schoolYear.SchoolId &&
            ((schoolYear.StartDate >= sy.StartDate && schoolYear.StartDate <= sy.EndDate) ||
            (schoolYear.EndDate >= sy.StartDate && schoolYear.EndDate <= sy.EndDate) ||
            (schoolYear.StartDate <= sy.StartDate && schoolYear.EndDate >= sy.EndDate)));

        if (overlapping)
            throw new InvalidOperationException("School year dates overlap with an existing school year");

        return await _schoolYearRepository.AddAsync(schoolYear);
    }

    public async Task<SchoolYear?> UpdateSchoolYearAsync(SchoolYear schoolYear)
    {
        // Business rule: Validate dates
        if (schoolYear.EndDate <= schoolYear.StartDate)
            throw new ArgumentException("End date must be after start date");

        await _schoolYearRepository.UpdateAsync(schoolYear);
        return await _schoolYearRepository.GetByIdAsync(schoolYear.Id);
    }

    public async Task DeleteSchoolYearAsync(int id)
    {
        await _schoolYearRepository.DeleteAsync(id);
    }
}

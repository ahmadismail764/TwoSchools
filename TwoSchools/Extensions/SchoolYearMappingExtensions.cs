using TwoSchools.Domain.Entities;
using TwoSchools.DTOs;

namespace TwoSchools.Extensions;

public static class SchoolYearMappingExtensions
{
    public static SchoolYearResponse ToResponse(this SchoolYear schoolYear)
    {
        return new SchoolYearResponse
        {
            Id = schoolYear.Id,
            Year = schoolYear.Year,
            StartDate = schoolYear.StartDate,
            EndDate = schoolYear.EndDate,
            SchoolId = schoolYear.SchoolId,
            SchoolName = schoolYear.School?.Name ?? string.Empty,
            Terms = schoolYear.Terms?.Select(t => t.ToResponse()).ToList() ?? new List<TermResponse>(),
            IsActive = schoolYear.StartDate <= DateTime.Now && schoolYear.EndDate >= DateTime.Now
        };
    }

    public static SchoolYear ToEntity(this CreateSchoolYearRequest request)
    {
        return new SchoolYear
        {
            Year = request.Year,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            SchoolId = request.SchoolId
        };
    }

    public static SchoolYear ToEntity(this UpdateSchoolYearRequest request)
    {
        return new SchoolYear
        {
            Year = request.Year,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            SchoolId = request.SchoolId
        };
    }
}

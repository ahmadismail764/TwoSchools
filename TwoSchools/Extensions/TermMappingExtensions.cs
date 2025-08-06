using TwoSchools.Domain.Entities;
using TwoSchools.DTOs;

namespace TwoSchools.Extensions;

public static class TermMappingExtensions
{
    public static TermResponse ToResponse(this Term term)
    {
        return new TermResponse
        {
            Id = term.Id,
            Name = term.Name,
            StartDate = term.StartDate,
            EndDate = term.EndDate,
            SchoolYearId = term.SchoolYearId,
            Year = term.SchoolYear?.Year ?? 0,
            TotalEnrollments = term.Enrollments?.Count ?? 0,
            ActiveEnrollments = term.Enrollments?.Count(e => e.IsActive) ?? 0,
            IsActive = term.EndDate > DateTime.Now
        };
    }

    public static Term ToEntity(this CreateTermRequest request)
    {
        return new Term
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            SchoolYearId = request.SchoolYearId
        };
    }

    public static Term ToEntity(this UpdateTermRequest request)
    {
        return new Term
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            SchoolYearId = request.SchoolYearId
        };
    }
}

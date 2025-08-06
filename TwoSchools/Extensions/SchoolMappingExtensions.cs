using TwoSchools.Domain.Entities;
using TwoSchools.DTOs;

namespace TwoSchools.Extensions;

public static class SchoolMappingExtensions
{
    public static SchoolResponse ToResponse(this School school)
    {
        return new SchoolResponse
        {
            Id = school.Id,
            Name = school.Name,
            Address = school.Address,
            PhoneNumber = school.PhoneNumber,
            Email = school.Email,
            Website = school.Website
        };
    }

    public static School ToEntity(this CreateSchoolRequest request)
    {
        return new School
        {
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Website = request.Website
        };
    }

    public static School ToEntity(this UpdateSchoolRequest request)
    {
        return new School
        {
            Name = request.Name ?? string.Empty,
            Address = request.Address ?? string.Empty,
            PhoneNumber = request.PhoneNumber ?? string.Empty,
            Email = request.Email ?? string.Empty,
            Website = request.Website
        };
    }
}

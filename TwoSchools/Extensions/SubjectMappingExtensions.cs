using TwoSchools.Domain.Entities;
using TwoSchools.DTOs;
namespace TwoSchools.Extensions;

public static class SubjectMappingExtensions
{
    public static SubjectResponse ToResponse(this Subject subject)
    {
        return new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            Code = subject.Code,
            Description = subject.Description,
            Credits = subject.Credits
        };
    }

    public static Subject ToEntity(this CreateSubjectRequest request)
    {
        return new Subject
        {
            Name = request.Name,
            Code = request.Code,
            Description = request.Description,
            Credits = request.Credits
        };
    }

    public static Subject ToEntity(this UpdateSubjectRequest request)
    {
        return new Subject
        {
            Name = request.Name ?? string.Empty,
            Code = request.Code ?? string.Empty,
            Description = request.Description ?? string.Empty,
            Credits = request.Credits ?? 0
        };
    }
}

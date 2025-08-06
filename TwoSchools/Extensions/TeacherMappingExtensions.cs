using TwoSchools.Domain.Entities;
using TwoSchools.DTOs;

namespace TwoSchools.Extensions;

public static class TeacherMappingExtensions
{
    public static TeacherResponse ToResponse(this Teacher teacher)
    {
        return new TeacherResponse
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
            Email = teacher.Email,
            SchoolId = teacher.SchoolId,
            SchoolName = teacher.School?.Name ?? string.Empty,
            Role = "Teacher",
            ActiveClasses = teacher.Enrollments?.Select(e => e.SubjectId).Distinct().Count() ?? 0,
            TotalStudents = teacher.Enrollments?.Count(e => e.IsActive) ?? 0,
            AverageClassGrade = teacher.Enrollments?.Where(e => e.Grade.HasValue).Average(e => e.Grade) ?? null
        };
    }

    public static Teacher ToEntity(this CreateTeacherRequest request)
    {
        return new Teacher
        {
            FullName = request.FullName,
            Email = request.Email,
            SchoolId = request.SchoolId
        };
    }

    public static Teacher ToEntity(this UpdateTeacherRequest request)
    {
        return new Teacher
        {
            FullName = request.FullName,
            Email = request.Email,
            SchoolId = request.SchoolId
        };
    }
}

using TwoSchools.Domain.Entities;
using TwoSchools.DTOs;

namespace TwoSchools.Extensions;

public static class StudentMappingExtensions
{
    public static StudentResponse ToResponse(this Student student)
    {
        var enrollments = student.Enrollments ?? new List<Enrollment>();
        var completedEnrollments = enrollments.Where(e => e.Grade.HasValue).ToList();
        var activeEnrollments = enrollments.Where(e => e.IsActive && !e.Grade.HasValue).Count();

        return new StudentResponse
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            SchoolId = student.SchoolId,
            SchoolName = student.School?.Name ?? "Unknown",
            Role = student.Role,
            ActiveEnrollments = activeEnrollments,
            CompletedCourses = completedEnrollments.Count,
            CurrentGPA = CalculateGPA(completedEnrollments),
            AcademicStanding = DetermineAcademicStanding(CalculateGPA(completedEnrollments))
        };
    }

    public static StudentDetailResponse ToDetailResponse(this Student student)
    {
        var enrollments = student.Enrollments ?? new List<Enrollment>();

        return new StudentDetailResponse
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            SchoolId = student.SchoolId,
            SchoolName = student.School?.Name ?? "Unknown",
            AcademicSummary = CreateAcademicSummary(enrollments),
            RecentEnrollments = enrollments
                .OrderByDescending(e => e.EnrollmentDate)
                .Take(5)
                .Select(e => e.ToResponse())
                .ToList()
        };
    }

    private static StudentAcademicSummary CreateAcademicSummary(List<Enrollment> enrollments)
    {
        var completedEnrollments = enrollments.Where(e => e.Grade.HasValue).ToList();
        var activeEnrollments = enrollments.Where(e => e.IsActive && !e.Grade.HasValue).ToList();

        return new StudentAcademicSummary
        {
            TotalEnrollments = enrollments.Count,
            ActiveEnrollments = activeEnrollments.Count,
            CompletedCourses = completedEnrollments.Count,
            TotalCreditsEarned = completedEnrollments.Sum(e => e.Subject?.Credits ?? 0),
            TotalCreditsAttempted = enrollments.Sum(e => e.Subject?.Credits ?? 0),
            OverallGPA = CalculateGPA(completedEnrollments),
            CurrentTermGPA = CalculateGPA(activeEnrollments),
            AcademicStanding = DetermineAcademicStanding(CalculateGPA(completedEnrollments)),
            LastUpdated = DateTime.UtcNow
        };
    }

    private static decimal? CalculateGPA(List<Enrollment> enrollments)
    {
        var gradesWithCredits = enrollments
            .Where(e => e.Grade.HasValue && e.Subject != null)
            .ToList();

        if (!gradesWithCredits.Any()) return null;

        var totalPoints = gradesWithCredits.Sum(e => ConvertToGradePoints(e.Grade!.Value) * e.Subject!.Credits);
        var totalCredits = gradesWithCredits.Sum(e => e.Subject!.Credits);

        return totalCredits > 0 ? totalPoints / totalCredits : null;
    }

    private static decimal ConvertToGradePoints(decimal grade)
    {
        return grade switch
        {
            >= 90 => 4.0m,
            >= 80 => 3.0m,
            >= 70 => 2.0m,
            >= 60 => 1.0m,
            _ => 0.0m
        };
    }

    private static string DetermineAcademicStanding(decimal? gpa)
    {
        if (!gpa.HasValue) return "No grades recorded";

        return gpa.Value switch
        {
            >= 3.5m => "Dean's List",
            >= 3.0m => "Good Standing",
            >= 2.0m => "Satisfactory",
            >= 1.0m => "Academic Probation",
            _ => "Academic Suspension"
        };
    }
}

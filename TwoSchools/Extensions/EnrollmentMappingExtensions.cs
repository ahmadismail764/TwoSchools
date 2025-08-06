using TwoSchools.Domain.Entities;
using TwoSchools.DTOs;

namespace TwoSchools.Extensions;

public static class EnrollmentMappingExtensions
{
    public static EnrollmentResponse ToResponse(this Enrollment enrollment)
    {
        return new EnrollmentResponse
        {
            Id = enrollment.Id,
            StudentId = enrollment.StudentId,
            StudentName = enrollment.Student?.FullName ?? "Unknown",
            StudentEmail = enrollment.Student?.Email ?? "Unknown",
            SubjectId = enrollment.SubjectId,
            SubjectName = enrollment.Subject?.Name ?? "Unknown",
            SubjectCode = enrollment.Subject?.Code ?? "Unknown",
            SubjectCredits = enrollment.Subject?.Credits ?? 0,
            TermId = enrollment.TermId,
            TermName = enrollment.Term?.Name ?? "Unknown",
            TeacherId = enrollment.TeacherId,
            TeacherName = enrollment.Teacher?.FullName ?? "Unknown",
            Grade = enrollment.Grade,
            LetterGrade = ConvertToLetterGrade(enrollment.Grade),
            EnrollmentDate = enrollment.EnrollmentDate,
            IsActive = enrollment.IsActive,
            Status = enrollment.IsActive ? 
                (enrollment.Grade.HasValue ? "Completed" : "Enrolled") : 
                "Withdrawn"
        };
    }

    public static StudentEnrollmentSummary ToSummary(this IEnumerable<Enrollment> enrollments, string studentName, string studentEmail)
    {
        var enrollmentList = enrollments.ToList();
        var activeEnrollments = enrollmentList.Where(e => e.IsActive && !e.Grade.HasValue).ToList();
        var completedEnrollments = enrollmentList.Where(e => e.IsActive && e.Grade.HasValue).ToList();

        return new StudentEnrollmentSummary
        {
            StudentId = enrollmentList.FirstOrDefault()?.StudentId ?? 0,
            StudentName = studentName,
            StudentEmail = studentEmail,
            ActiveEnrollments = activeEnrollments.Select(e => e.ToResponse()).ToList(),
            CompletedEnrollments = completedEnrollments.Select(e => e.ToResponse()).ToList(),
            CurrentTermGPA = CalculateGPA(activeEnrollments),
            OverallGPA = CalculateGPA(completedEnrollments),
            TotalCreditsEarned = completedEnrollments.Sum(e => e.Subject?.Credits ?? 0),
            TotalCreditsAttempted = enrollmentList.Sum(e => e.Subject?.Credits ?? 0),
            AcademicStanding = DetermineAcademicStanding(CalculateGPA(completedEnrollments))
        };
    }

    public static StudentAverageResponse ToAverageResponse(this IEnumerable<Enrollment> enrollments, string studentName)
    {
        var enrollmentList = enrollments.Where(e => e.Grade.HasValue).ToList();
        var averageGrade = enrollmentList.Any() ? enrollmentList.Average(e => e.Grade!.Value) : (decimal?)null;

        return new StudentAverageResponse
        {
            StudentId = enrollmentList.FirstOrDefault()?.StudentId ?? 0,
            StudentName = studentName,
            AverageGrade = averageGrade,
            AverageLetterGrade = ConvertToLetterGrade(averageGrade),
            CompletedCourses = enrollmentList.Count(e => e.Grade.HasValue),
            TotalEnrollments = enrollmentList.Count,
            TotalCredits = enrollmentList.Sum(e => e.Subject?.Credits ?? 0),
            CalculatedAt = DateTime.UtcNow
        };
    }

    private static string? ConvertToLetterGrade(decimal? grade)
    {
        if (!grade.HasValue) return null;

        return grade.Value switch
        {
            >= 90 => "A",
            >= 80 => "B", 
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
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

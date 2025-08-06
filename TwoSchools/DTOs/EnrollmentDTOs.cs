using System.ComponentModel.DataAnnotations;

namespace TwoSchools.DTOs;

// === REQUEST DTOs ===
public class EnrollStudentRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Student ID must be a positive number")]
    public int StudentId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Subject ID must be a positive number")]
    public int SubjectId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Term ID must be a positive number")]
    public int TermId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Teacher ID must be a positive number")]
    public int TeacherId { get; set; }
}

public class UpdateGradeRequest
{
    [Required]
    [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
    public decimal Grade { get; set; }
}

// === RESPONSE DTOs ===
public class EnrollmentResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
    public int SubjectCredits { get; set; }
    public int TermId { get; set; }
    public string TermName { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public decimal? Grade { get; set; }
    public string? LetterGrade { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool IsActive { get; set; }
    public string Status { get; set; } = string.Empty; // "Enrolled", "Completed", "Withdrawn"
}

public class StudentEnrollmentSummary
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public List<EnrollmentResponse> ActiveEnrollments { get; set; } = new();
    public List<EnrollmentResponse> CompletedEnrollments { get; set; } = new();
    public decimal? CurrentTermGPA { get; set; }
    public decimal? OverallGPA { get; set; }
    public int TotalCreditsEarned { get; set; }
    public int TotalCreditsAttempted { get; set; }
    public string AcademicStanding { get; set; } = string.Empty; // "Good Standing", "Probation", etc.
}

public class StudentAverageResponse
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public decimal? AverageGrade { get; set; }
    public string? AverageLetterGrade { get; set; }
    public int CompletedCourses { get; set; }
    public int TotalEnrollments { get; set; }
    public int TotalCredits { get; set; }
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}

// === SUMMARY DTOs (Great for dashboards) ===
public class EnrollmentStatistics
{
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public int CompletedEnrollments { get; set; }
    public int WithdrawnEnrollments { get; set; }
    public decimal AverageGradeAcrossAll { get; set; }
    public List<SubjectEnrollmentCount> PopularSubjects { get; set; } = new();
}

public class SubjectEnrollmentCount
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
    public int EnrollmentCount { get; set; }
    public decimal AverageGrade { get; set; }
}

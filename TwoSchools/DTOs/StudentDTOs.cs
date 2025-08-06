using System.ComponentModel.DataAnnotations;

namespace TwoSchools.DTOs;

// === STUDENT REQUEST DTOs ===
public class CreateStudentRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "School ID must be a positive number")]
    public int SchoolId { get; set; }
}

public class UpdateStudentRequest
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string? FullName { get; set; }

    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string? Email { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "School ID must be a positive number")]
    public int? SchoolId { get; set; }
}

// === STUDENT RESPONSE DTOs ===
public class StudentResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SchoolId { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public string Role { get; set; } = "Student";
    public int ActiveEnrollments { get; set; }
    public int CompletedCourses { get; set; }
    public decimal? CurrentGPA { get; set; }
    public string AcademicStanding { get; set; } = string.Empty;
}

public class StudentDetailResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SchoolId { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public StudentAcademicSummary AcademicSummary { get; set; } = new();
    public List<EnrollmentResponse> RecentEnrollments { get; set; } = new();
}

public class StudentAcademicSummary
{
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public int CompletedCourses { get; set; }
    public int TotalCreditsEarned { get; set; }
    public int TotalCreditsAttempted { get; set; }
    public decimal? OverallGPA { get; set; }
    public decimal? CurrentTermGPA { get; set; }
    public string AcademicStanding { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

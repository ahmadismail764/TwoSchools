using System.ComponentModel.DataAnnotations;

namespace TwoSchools.DTOs;

// === TEACHER REQUEST DTOs ===
public class CreateTeacherRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "School ID must be a positive number")]
    public int SchoolId { get; set; }
}

public class UpdateTeacherRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "School ID must be a positive number")]
    public int SchoolId { get; set; }
}

// === TEACHER RESPONSE DTOs ===
public class TeacherResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SchoolId { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public string Role { get; set; } = "Teacher";
    public int ActiveClasses { get; set; }
    public int TotalStudents { get; set; }
    public decimal? AverageClassGrade { get; set; }
}

public class TeacherWithEnrollmentsResponse : TeacherResponse
{
    public List<EnrollmentResponse> Enrollments { get; set; } = new();
}

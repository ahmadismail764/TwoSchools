using System.ComponentModel.DataAnnotations;

namespace TwoSchools.DTOs;

// === SCHOOL REQUEST DTOs ===
public class CreateSchoolRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "School name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Please provide a valid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Url(ErrorMessage = "Please provide a valid website URL")]
    public string? Website { get; set; }
}

public class UpdateSchoolRequest
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "School name must be between 2 and 100 characters")]
    public string? Name { get; set; }

    [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
    public string? Address { get; set; }

    [Phone(ErrorMessage = "Please provide a valid phone number")]
    public string? PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string? Email { get; set; }

    [Url(ErrorMessage = "Please provide a valid website URL")]
    public string? Website { get; set; }
}

// === SCHOOL RESPONSE DTOs ===
public class SchoolResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Website { get; set; }
    public int TotalStudents { get; set; }
    public int TotalTeachers { get; set; }
    public int ActiveEnrollments { get; set; }
}

public class SchoolDetailResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Website { get; set; }
    public SchoolStatistics Statistics { get; set; } = new();
    public List<StudentResponse> RecentStudents { get; set; } = new();
    public List<TeacherResponse> Teachers { get; set; } = new();
    public List<SchoolYearResponse> SchoolYears { get; set; } = new();
}

public class SchoolStatistics
{
    public int TotalStudents { get; set; }
    public int TotalTeachers { get; set; }
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public int CompletedEnrollments { get; set; }
    public decimal? AverageGPA { get; set; }
    public int TotalCreditsAwarded { get; set; }
    public decimal StudentRetentionRate { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

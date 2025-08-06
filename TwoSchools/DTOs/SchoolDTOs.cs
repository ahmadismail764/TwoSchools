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

// === SCHOOL YEAR RESPONSE DTOs ===
public class SchoolYearResponse
{
    public int Id { get; set; }
    public int Year { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SchoolId { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public List<TermResponse> Terms { get; set; } = new();
    public bool IsActive { get; set; }
}

// === TERM RESPONSE DTOs ===
public class TermResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SchoolYearId { get; set; }
    public int Year { get; set; }
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public bool IsActive { get; set; }
}

// === TEACHER REQUEST DTOs ===
public class CreateTeacherRequest
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int SchoolId { get; set; }
}

public class UpdateTeacherRequest
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int SchoolId { get; set; }
}

// === TERM REQUEST DTOs ===
public class CreateTermRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int SchoolYearId { get; set; }
}

public class UpdateTermRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int SchoolYearId { get; set; }
}

// === SCHOOL YEAR REQUEST DTOs ===
public class CreateSchoolYearRequest
{
    [Required]
    [Range(2020, 2030, ErrorMessage = "Year must be between 2020 and 2030")]
    public int Year { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int SchoolId { get; set; }
}

public class UpdateSchoolYearRequest
{
    [Required]
    [Range(2020, 2030, ErrorMessage = "Year must be between 2020 and 2030")]
    public int Year { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int SchoolId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace TwoSchools.DTOs;

// === SUBJECT REQUEST DTOs ===
public class CreateSubjectRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Subject name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Subject code must be between 2 and 10 characters")]
    public string Code { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6")]
    public int Credits { get; set; }
}

public class UpdateSubjectRequest
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Subject name must be between 2 and 100 characters")]
    public string? Name { get; set; }

    [StringLength(10, MinimumLength = 2, ErrorMessage = "Subject code must be between 2 and 10 characters")]
    public string? Code { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6")]
    public int? Credits { get; set; }
}

// === SUBJECT RESPONSE DTOs ===
public class SubjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public decimal? AverageGrade { get; set; }
    public string? AverageLetterGrade { get; set; }
}

public class SubjectDetailResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Credits { get; set; }
    public SubjectStatistics Statistics { get; set; } = new();
    public List<SubjectEnrollmentInfo> CurrentEnrollments { get; set; } = new();
    public List<TeacherSubjectHistory> TeachingHistory { get; set; } = new();
}

public class SubjectStatistics
{
    public int TotalEnrollments { get; set; }
    public int ActiveEnrollments { get; set; }
    public int CompletedEnrollments { get; set; }
    public int WithdrawnEnrollments { get; set; }
    public decimal? AverageGrade { get; set; }
    public string? AverageLetterGrade { get; set; }
    public decimal PassRate { get; set; } // Percentage with grade >= 60
    public int TotalCreditsAwarded { get; set; }
}

public class SubjectEnrollmentInfo
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int TermId { get; set; }
    public string TermName { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public decimal? Grade { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool IsActive { get; set; }
}

public class TeacherSubjectHistory
{
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public int TermId { get; set; }
    public string TermName { get; set; } = string.Empty;
    public int StudentsEnrolled { get; set; }
    public decimal? AverageGrade { get; set; }
}

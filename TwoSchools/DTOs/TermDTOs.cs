using System.ComponentModel.DataAnnotations;

namespace TwoSchools.DTOs;

// === TERM REQUEST DTOs ===
public class CreateTermRequest
{
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Term name must be between 2 and 50 characters")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "School Year ID must be a positive number")]
    public int SchoolYearId { get; set; }
}

public class UpdateTermRequest
{
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Term name must be between 2 and 50 characters")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "School Year ID must be a positive number")]
    public int SchoolYearId { get; set; }
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
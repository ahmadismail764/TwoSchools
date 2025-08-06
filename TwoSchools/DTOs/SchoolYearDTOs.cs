using System.ComponentModel.DataAnnotations;

namespace TwoSchools.DTOs;

// === SCHOOL YEAR REQUEST DTOs ===
public class CreateSchoolYearRequest
{
    [Required]
    [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100")]
    public int Year { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "School ID must be a positive number")]
    public int SchoolId { get; set; }
}

public class UpdateSchoolYearRequest
{
    [Required]
    [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100")]
    public int Year { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "School ID must be a positive number")]
    public int SchoolId { get; set; }
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


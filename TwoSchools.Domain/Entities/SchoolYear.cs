namespace TwoSchools.Domain.Entities;

public class SchoolYear
{
    public int Id { get; set; }
    public int Year { get; set; } // e.g., 2024
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SchoolId { get; set; }
    public School School { get; set; } = null!;
    
    // Navigation properties
    public List<Term> Terms { get; set; } = new();
}
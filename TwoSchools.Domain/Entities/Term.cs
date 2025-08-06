namespace TwoSchools.Domain.Entities;

public class Term
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g., "Fall 2024", "Spring 2025"
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SchoolYearId { get; set; }
    public SchoolYear SchoolYear { get; set; } = null!;
    
    // Navigation properties
    public List<Enrollment> Enrollments { get; set; } = new(); // Subject offerings in this term
}
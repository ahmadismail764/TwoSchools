namespace TwoSchools.Domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // e.g., "MATH101"
    public string Description { get; set; } = string.Empty; // Optional subject description
    public int Credits { get; set; } = 3; // Credit hours
    
    // Navigation properties
    public List<Enrollment> Enrollments { get; set; } = new(); // Many offerings through Enrollment
}
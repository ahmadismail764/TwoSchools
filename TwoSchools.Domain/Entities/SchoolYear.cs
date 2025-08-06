namespace TwoSchools.Domain.Entities;

public class SchoolYear
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g., "2024-2025"
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Term> Terms { get; set; } = new();
}
namespace TwoSchools.Domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // e.g., "MATH101"
    public int TermId { get; set; }
    public Term Term { get; set; } = null!;
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public List<Student> Students { get; set; } = new(); // Many-to-many
}
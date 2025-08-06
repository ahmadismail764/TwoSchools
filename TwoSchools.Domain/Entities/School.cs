namespace TwoSchools.Domain.Entities;

public class School
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Website { get; set; }

    // Navigation properties
    public List<Teacher> Teachers { get; set; } = new();
    public List<Student> Students { get; set; } = new();
    public List<SchoolYear> SchoolYears { get; set; } = new();
}

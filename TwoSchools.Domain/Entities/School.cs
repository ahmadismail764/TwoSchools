namespace TwoSchools.Domain.Entities;

public class School
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;

    // Navigation properties
    public List<Teacher> Teachers { get; set; } = new();
    public List<Student> Students { get; set; } = new();
}

using TwoSchools.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SchoolId { get; set; }
    public School School { get; set; } = null!;

    // Navigation properties - FIX: Add = new();
    public List<Subject> Subjects { get; set; } = new();

    // For JWT role-based access control
    public string Role => "Teacher";
}
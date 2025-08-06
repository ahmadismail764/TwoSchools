namespace TwoSchools.Domain.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
    public int TermId { get; set; }
    public Term Term { get; set; } = null!;
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
    public decimal? Grade { get; set; } // nullable until graded
    public bool IsActive { get; set; } = true; // for withdrawal tracking
}

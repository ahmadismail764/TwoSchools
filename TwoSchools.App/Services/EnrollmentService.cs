using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;

namespace TwoSchools.App.Services;

public class EnrollmentService
{
    public readonly IEnrollmentRepository _enrollmentRepository;
    public readonly IStudentRepository _studentRepository;
    public readonly ISubjectRepository _subjectRepository;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        IStudentRepository studentRepository,
        ISubjectRepository subjectRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
    {
        return await _enrollmentRepository.GetByIdAsync(id);
    }

    public async Task<Enrollment> EnrollStudentAsync(int studentId, int subjectId, int termId, int teacherId)
    {
        // Business rules
        var student = await _studentRepository.GetByIdAsync(studentId);
        if (student == null)
            throw new ArgumentException("Student not found");

        var subject = await _subjectRepository.GetByIdAsync(subjectId);
        if (subject == null)
            throw new ArgumentException("Subject not found");

        // Check if already enrolled in this subject for this term
        var existingEnrollment = await _enrollmentRepository.GetEnrollmentAsync(studentId, subjectId, termId);
        if (existingEnrollment != null && existingEnrollment.IsActive)
            throw new InvalidOperationException("Student is already enrolled in this subject for this term");

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            SubjectId = subjectId,
            TermId = termId,
            TeacherId = teacherId,
            EnrollmentDate = DateTime.UtcNow,
            IsActive = true
        };

        return await _enrollmentRepository.AddAsync(enrollment);
    }

    public async Task<IEnumerable<Enrollment>> GetStudentEnrollmentsAsync(int studentId)
    {
        return await _enrollmentRepository.GetActiveEnrollmentsByStudentAsync(studentId);
    }

    public async Task<Enrollment> UpdateGradeAsync(int enrollmentId, decimal grade)
    {
        if (grade < 0 || grade > 100)
            throw new ArgumentException("Grade must be between 0 and 100");

        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
        if (enrollment == null)
            throw new ArgumentException("Enrollment not found");

        enrollment.Grade = grade;
        await _enrollmentRepository.UpdateAsync(enrollment);
        return enrollment;
    }

    public async Task WithdrawStudentAsync(int studentId, int subjectId, int termId)
    {
        await _enrollmentRepository.WithdrawStudentAsync(studentId, subjectId, termId);
    }

    public async Task<decimal?> GetStudentAverageGradeAsync(int studentId)
    {
        return await _enrollmentRepository.GetAverageGradeAsync(studentId);
    }
}

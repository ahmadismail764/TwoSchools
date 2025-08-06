using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;

namespace TwoSchools.App.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISchoolRepository _schoolRepository;

    public StudentService(IStudentRepository studentRepository, ISchoolRepository schoolRepository)
    {
        _studentRepository = studentRepository;
        _schoolRepository = schoolRepository;
    }

    public async Task<IEnumerable<Student>> GetStudentsBySchoolAsync(int schoolId)
    {
        // Business rule: Validate school exists first
        var school = await _schoolRepository.GetByIdAsync(schoolId);
        if (school == null)
            throw new ArgumentException("School not found");
            
        return await _studentRepository.GetStudentsBySchoolAsync(schoolId);
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        // Business rules here (validation, etc.)
        if (string.IsNullOrEmpty(student.Email))
            throw new ArgumentException("Email is required");
            
        // Check if email already exists
        var existingStudent = await _studentRepository.GetStudentByEmailAsync(student.Email);
        if (existingStudent != null)
            throw new InvalidOperationException("Student with this email already exists");

        return await _studentRepository.AddAsync(student);
    }

    public async Task<Student?> GetStudentWithEnrollmentsAsync(int studentId)
    {
        return await _studentRepository.GetStudentWithEnrollmentsAsync(studentId);
    }
}

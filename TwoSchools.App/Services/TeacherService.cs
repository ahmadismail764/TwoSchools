using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;

namespace TwoSchools.App.Services;

public class TeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISchoolRepository _schoolRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public TeacherService(
        ITeacherRepository teacherRepository,
        ISchoolRepository schoolRepository,
        IEnrollmentRepository enrollmentRepository)
    {
        _teacherRepository = teacherRepository;
        _schoolRepository = schoolRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
    {
        return await _teacherRepository.GetAllAsync();
    }

    public async Task<Teacher?> GetTeacherByIdAsync(int id)
    {
        return await _teacherRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Teacher>> GetTeachersBySchoolAsync(int schoolId)
    {
        // Business rule: Validate school exists first
        var school = await _schoolRepository.GetByIdAsync(schoolId);
        if (school == null)
            throw new ArgumentException("School not found");

        return await _teacherRepository.GetTeachersBySchoolAsync(schoolId);
    }

    public async Task<Teacher> CreateTeacherAsync(Teacher teacher)
    {
        // Business rule: Email must be unique
        if (string.IsNullOrEmpty(teacher.Email))
            throw new ArgumentException("Email is required");

        var existingTeacher = await _teacherRepository.GetTeacherByEmailAsync(teacher.Email);
        if (existingTeacher != null)
            throw new InvalidOperationException("Teacher with this email already exists");

        // Business rule: Validate school exists
        var school = await _schoolRepository.GetByIdAsync(teacher.SchoolId);
        if (school == null)
            throw new ArgumentException("School not found");

        return await _teacherRepository.AddAsync(teacher);
    }

    public async Task<Teacher> UpdateTeacherAsync(Teacher teacher)
    {
        var existingTeacher = await _teacherRepository.GetByIdAsync(teacher.Id);
        if (existingTeacher == null)
            throw new ArgumentException("Teacher not found");

        // Business rule: Email must remain unique
        if (existingTeacher.Email != teacher.Email)
        {
            var teacherWithEmail = await _teacherRepository.GetTeacherByEmailAsync(teacher.Email);
            if (teacherWithEmail != null && teacherWithEmail.Id != teacher.Id)
                throw new InvalidOperationException("Teacher with this email already exists");
        }

        await _teacherRepository.UpdateAsync(teacher);
        return teacher;
    }

    public async Task DeleteTeacherAsync(int id)
    {
        // Business rule: Can't delete teacher with active enrollments
        var enrollments = await _enrollmentRepository.GetEnrollmentsByTeacherAsync(id);
        var activeEnrollments = enrollments.Where(e => e.IsActive).ToList();
        
        if (activeEnrollments.Any())
            throw new InvalidOperationException("Cannot delete teacher with active teaching assignments");

        await _teacherRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Enrollment>> GetTeacherEnrollmentsAsync(int teacherId)
    {
        var teacher = await _teacherRepository.GetByIdAsync(teacherId);
        if (teacher == null)
            throw new ArgumentException("Teacher not found");

        return await _enrollmentRepository.GetEnrollmentsByTeacherAsync(teacherId);
    }

    public async Task<IEnumerable<Enrollment>> GetTeacherActiveEnrollmentsAsync(int teacherId)
    {
        var enrollments = await GetTeacherEnrollmentsAsync(teacherId);
        return enrollments.Where(e => e.IsActive);
    }

    public async Task<int> GetTeacherStudentCountAsync(int teacherId)
    {
        var activeEnrollments = await GetTeacherActiveEnrollmentsAsync(teacherId);
        return activeEnrollments.Select(e => e.StudentId).Distinct().Count();
    }

    public async Task<decimal?> GetTeacherAverageGradeAsync(int teacherId)
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsByTeacherAsync(teacherId);
        var gradesWithCredits = enrollments.Where(e => e.Grade.HasValue).ToList();
        
        if (!gradesWithCredits.Any()) return null;
        
        return gradesWithCredits.Average(e => e.Grade!.Value);
    }
}

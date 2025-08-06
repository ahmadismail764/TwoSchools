using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;

namespace TwoSchools.App.Services;

public class SchoolService
{
    private readonly ISchoolRepository _schoolRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public SchoolService(
        ISchoolRepository schoolRepository,
        IStudentRepository studentRepository,
        ITeacherRepository teacherRepository)
    {
        _schoolRepository = schoolRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<IEnumerable<School>> GetAllSchoolsAsync()
    {
        return await _schoolRepository.GetAllAsync();
    }

    public async Task<School?> GetSchoolByIdAsync(int id)
    {
        return await _schoolRepository.GetByIdAsync(id);
    }

    public async Task<School?> GetSchoolWithDetailsAsync(int id)
    {
        return await _schoolRepository.GetSchoolWithDetailsAsync(id);
    }

    public async Task<School> CreateSchoolAsync(School school)
    {
        // Business rule: School name must be unique
        var schools = await _schoolRepository.GetAllAsync();
        if (schools.Any(s => s.Name.Equals(school.Name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"School with name '{school.Name}' already exists");

        // Business rule: Validate required fields
        if (string.IsNullOrWhiteSpace(school.Name))
            throw new ArgumentException("School name is required");

        if (string.IsNullOrWhiteSpace(school.Address))
            throw new ArgumentException("School address is required");

        return await _schoolRepository.AddAsync(school);
    }

    public async Task<School> UpdateSchoolAsync(School school)
    {
        var existingSchool = await _schoolRepository.GetByIdAsync(school.Id);
        if (existingSchool == null)
            throw new ArgumentException("School not found");

        // Business rule: School name must remain unique
        var schools = await _schoolRepository.GetAllAsync();
        if (schools.Any(s => s.Id != school.Id && s.Name.Equals(school.Name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"School with name '{school.Name}' already exists");

        await _schoolRepository.UpdateAsync(school);
        return school;
    }

    public async Task DeleteSchoolAsync(int id)
    {
        // Business rule: Can't delete school with students or teachers
        var students = await _studentRepository.GetStudentsBySchoolAsync(id);
        if (students.Any())
            throw new InvalidOperationException("Cannot delete school with existing students");

        var teachers = await _teacherRepository.GetTeachersBySchoolAsync(id);
        if (teachers.Any())
            throw new InvalidOperationException("Cannot delete school with existing teachers");

        await _schoolRepository.DeleteAsync(id);
    }

    public async Task<int> GetSchoolStudentCountAsync(int schoolId)
    {
        var students = await _studentRepository.GetStudentsBySchoolAsync(schoolId);
        return students.Count();
    }

    public async Task<int> GetSchoolTeacherCountAsync(int schoolId)
    {
        var teachers = await _teacherRepository.GetTeachersBySchoolAsync(schoolId);
        return teachers.Count();
    }

    public async Task<IEnumerable<Student>> GetSchoolStudentsAsync(int schoolId)
    {
        var school = await _schoolRepository.GetByIdAsync(schoolId);
        if (school == null)
            throw new ArgumentException("School not found");

        return await _studentRepository.GetStudentsBySchoolAsync(schoolId);
    }

    public async Task<IEnumerable<Teacher>> GetSchoolTeachersAsync(int schoolId)
    {
        var school = await _schoolRepository.GetByIdAsync(schoolId);
        if (school == null)
            throw new ArgumentException("School not found");

        return await _teacherRepository.GetTeachersBySchoolAsync(schoolId);
    }
}

using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;

namespace TwoSchools.App.Services;

public class SubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public SubjectService(ISubjectRepository subjectRepository, IEnrollmentRepository enrollmentRepository)
    {
        _subjectRepository = subjectRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
        return await _subjectRepository.GetAllAsync();
    }

    public async Task<Subject?> GetSubjectByIdAsync(int id)
    {
        return await _subjectRepository.GetByIdAsync(id);
    }

    public async Task<Subject?> GetSubjectByCodeAsync(string code)
    {
        return await _subjectRepository.GetSubjectByCodeAsync(code);
    }

    public async Task<Subject?> GetSubjectWithDetailsAsync(int id)
    {
        return await _subjectRepository.GetSubjectWithDetailsAsync(id);
    }

    public async Task<Subject> CreateSubjectAsync(Subject subject)
    {
        // Business rule: Subject code must be unique
        var existingSubject = await _subjectRepository.GetSubjectByCodeAsync(subject.Code);
        if (existingSubject != null)
            throw new InvalidOperationException($"Subject with code '{subject.Code}' already exists");

        // Business rule: Credits must be valid
        if (subject.Credits < 1 || subject.Credits > 6)
            throw new ArgumentException("Credits must be between 1 and 6");

        return await _subjectRepository.AddAsync(subject);
    }

    public async Task<Subject> UpdateSubjectAsync(Subject subject)
    {
        var existingSubject = await _subjectRepository.GetByIdAsync(subject.Id);
        if (existingSubject == null)
            throw new ArgumentException("Subject not found");

        // Business rule: Can't change code if there are active enrollments
        if (existingSubject.Code != subject.Code)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsBySubjectAsync(subject.Id);
            var hasActiveEnrollments = enrollments.Any(e => e.IsActive);
            if (hasActiveEnrollments)
                throw new InvalidOperationException("Cannot change subject code while there are active enrollments");
        }

        await _subjectRepository.UpdateAsync(subject);
        return subject;
    }

    public async Task DeleteSubjectAsync(int id)
    {
        // Business rule: Can't delete subject with enrollments
        var enrollments = await _enrollmentRepository.GetEnrollmentsBySubjectAsync(id);
        if (enrollments.Any())
            throw new InvalidOperationException("Cannot delete subject with existing enrollments");

        await _subjectRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTermAsync(int termId)
    {
        return await _subjectRepository.GetSubjectsByTermAsync(termId);
    }

    public async Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId)
    {
        return await _subjectRepository.GetSubjectsByTeacherAsync(teacherId);
    }

    public async Task<decimal?> GetSubjectAverageGradeAsync(int subjectId)
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsBySubjectAsync(subjectId);
        var gradesWithCredits = enrollments.Where(e => e.Grade.HasValue).ToList();
        
        if (!gradesWithCredits.Any()) return null;
        
        return gradesWithCredits.Average(e => e.Grade!.Value);
    }

    public async Task<int> GetSubjectEnrollmentCountAsync(int subjectId)
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsBySubjectAsync(subjectId);
        return enrollments.Count(e => e.IsActive);
    }
}

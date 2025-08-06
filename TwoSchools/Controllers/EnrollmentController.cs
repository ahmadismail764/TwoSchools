using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.DTOs;
using TwoSchools.Extensions;

namespace TwoSchools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<ActionResult<EnrollmentResponse>> EnrollStudent([FromBody] EnrollStudentRequest request)
    {
        try
        {
            var enrollment = await _enrollmentService.EnrollStudentAsync(request.StudentId, request.SubjectId, request.TermId, request.TeacherId);
            var response = enrollment.ToResponse();
            return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnrollmentResponse>> GetEnrollment(int id)
    {
        var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
        if (enrollment == null) return NotFound();
        
        return Ok(enrollment.ToResponse());
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<StudentEnrollmentSummary>> GetStudentEnrollments(int studentId)
    {
        var enrollments = await _enrollmentService.GetStudentEnrollmentsAsync(studentId);
        var student = enrollments.FirstOrDefault()?.Student;
        
        if (student == null) return NotFound("Student not found");
        
        var summary = enrollments.ToSummary(student.FullName, student.Email);
        return Ok(summary);
    }

    [HttpPut("{id}/grade")]
    public async Task<ActionResult<EnrollmentResponse>> UpdateGrade(int id, [FromBody] UpdateGradeRequest request)
    {
        try
        {
            var enrollment = await _enrollmentService.UpdateGradeAsync(id, request.Grade);
            return Ok(enrollment.ToResponse());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{studentId}/subject/{subjectId}/term/{termId}")]
    public async Task<ActionResult> WithdrawStudent(int studentId, int subjectId, int termId)
    {
        await _enrollmentService.WithdrawStudentAsync(studentId, subjectId, termId);
        return NoContent();
    }

    [HttpGet("student/{studentId}/average")]
    public async Task<ActionResult<StudentAverageResponse>> GetStudentAverageGrade(int studentId)
    {
        var enrollments = await _enrollmentService.GetStudentEnrollmentsAsync(studentId);
        var student = enrollments.FirstOrDefault()?.Student;
        
        if (student == null) return NotFound("Student not found");
        
        var averageResponse = enrollments.ToAverageResponse(student.FullName);
        return Ok(averageResponse);
    }
}

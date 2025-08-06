using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.Domain.Entities;

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
    public async Task<ActionResult<Enrollment>> EnrollStudent([FromBody] EnrollStudentRequest request)
    {
        try
        {
            var enrollment = await _enrollmentService.EnrollStudentAsync(request.StudentId, request.SubjectId);
            return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.Id }, enrollment);
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
    public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
    {
        var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
        return enrollment == null ? NotFound() : Ok(enrollment);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<Enrollment>>> GetStudentEnrollments(int studentId)
    {
        var enrollments = await _enrollmentService.GetStudentEnrollmentsAsync(studentId);
        return Ok(enrollments);
    }

    [HttpPut("{id}/grade")]
    public async Task<ActionResult<Enrollment>> UpdateGrade(int id, [FromBody] UpdateGradeRequest request)
    {
        try
        {
            var enrollment = await _enrollmentService.UpdateGradeAsync(id, request.Grade);
            return Ok(enrollment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{studentId}/subject/{subjectId}")]
    public async Task<ActionResult> WithdrawStudent(int studentId, int subjectId)
    {
        await _enrollmentService.WithdrawStudentAsync(studentId, subjectId);
        return NoContent();
    }

    [HttpGet("student/{studentId}/average")]
    public async Task<ActionResult<decimal?>> GetStudentAverageGrade(int studentId)
    {
        var average = await _enrollmentService.GetStudentAverageGradeAsync(studentId);
        return Ok(new { StudentId = studentId, AverageGrade = average });
    }
}

public class EnrollStudentRequest
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
}

public class UpdateGradeRequest
{
    public decimal Grade { get; set; }
}

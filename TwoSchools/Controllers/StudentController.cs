using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.DTOs;
using TwoSchools.Extensions;

namespace TwoSchools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("school/{schoolId}")]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetStudentsBySchool(int schoolId)
    {
        try
        {
            var students = await _studentService.GetStudentsBySchoolAsync(schoolId);
            return Ok(students.Select(s => s.ToResponse()));
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/enrollments")]
    public async Task<ActionResult<StudentDetailResponse>> GetStudentWithEnrollments(int id)
    {
        var student = await _studentService.GetStudentWithEnrollmentsAsync(id);
        if (student == null)
            return NotFound();
        
        return Ok(student.ToDetailResponse());
    }

    [HttpPost]
    public async Task<ActionResult<StudentResponse>> CreateStudent([FromBody] CreateStudentRequest request)
    {
        try
        {
            var student = await _studentService.CreateStudentAsync(request.ToEntity());
            return CreatedAtAction(nameof(GetStudentWithEnrollments), new { id = student.Id }, student.ToResponse());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}

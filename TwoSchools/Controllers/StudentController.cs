using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.Domain.Entities;

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
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentsBySchool(int schoolId)
    {
        try
        {
            var students = await _studentService.GetStudentsBySchoolAsync(schoolId);
            return Ok(students);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/enrollments")]
    public async Task<ActionResult<Student>> GetStudentWithEnrollments(int id)
    {
        var student = await _studentService.GetStudentWithEnrollmentsAsync(id);
        return student == null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        try
        {
            var createdStudent = await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentWithEnrollments), new { id = createdStudent.Id }, createdStudent);
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

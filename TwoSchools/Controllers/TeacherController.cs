using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.Extensions;
using TwoSchools.DTOs;

namespace TwoSchools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly TeacherService _teacherService;

    public TeacherController(TeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeacherResponse>>> GetAllTeachers()
    {
        var teachers = await _teacherService.GetAllTeachersAsync();
        return Ok(teachers.Select(t => t.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherResponse>> GetTeacher(int id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        if (teacher == null)
            return NotFound();
        
        return Ok(teacher.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<TeacherResponse>> CreateTeacher([FromBody] CreateTeacherRequest request)
    {
        var teacher = await _teacherService.CreateTeacherAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, teacher.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeacherResponse>> UpdateTeacher(int id, [FromBody] UpdateTeacherRequest request)
    {
        var entity = request.ToEntity();
        entity.Id = id;
        var teacher = await _teacherService.UpdateTeacherAsync(entity);
        if (teacher == null)
            return NotFound();
        
        return Ok(teacher.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeacher(int id)
    {
        await _teacherService.DeleteTeacherAsync(id);
        return NoContent();
    }
}

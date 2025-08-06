using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.Extensions;
using TwoSchools.DTOs;

namespace TwoSchools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly SubjectService _subjectService;

    public SubjectController(SubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubjectResponse>>> GetAllSubjects()
    {
        var subjects = await _subjectService.GetAllSubjectsAsync();
        return Ok(subjects.Select(s => s.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectResponse>> GetSubject(int id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null)
            return NotFound();
        
        return Ok(subject.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<SubjectResponse>> CreateSubject([FromBody] CreateSubjectRequest request)
    {
        var subject = await _subjectService.CreateSubjectAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, subject.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SubjectResponse>> UpdateSubject(int id, [FromBody] UpdateSubjectRequest request)
    {
        var entity = request.ToEntity();
        entity.Id = id;
        var subject = await _subjectService.UpdateSubjectAsync(entity);
        if (subject == null)
            return NotFound();
        
        return Ok(subject.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubject(int id)
    {
        await _subjectService.DeleteSubjectAsync(id);
        return NoContent();
    }
}

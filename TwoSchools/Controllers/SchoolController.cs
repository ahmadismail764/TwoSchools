using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.DTOs;
using TwoSchools.Extensions;

namespace TwoSchools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolController : ControllerBase
{
    private readonly SchoolService _schoolService;

    public SchoolController(SchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SchoolResponse>>> GetAllSchools()
    {
        var schools = await _schoolService.GetAllSchoolsAsync();
        return Ok(schools.Select(s => s.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SchoolResponse>> GetSchool(int id)
    {
        var school = await _schoolService.GetSchoolByIdAsync(id);
        if (school == null)
            return NotFound();
        
        return Ok(school.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<SchoolResponse>> CreateSchool([FromBody] CreateSchoolRequest request)
    {
        var school = await _schoolService.CreateSchoolAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, school.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SchoolResponse>> UpdateSchool(int id, [FromBody] UpdateSchoolRequest request)
    {
        var entity = request.ToEntity();
        entity.Id = id;
        var school = await _schoolService.UpdateSchoolAsync(entity);
        if (school == null)
            return NotFound();
        
        return Ok(school.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSchool(int id)
    {
        await _schoolService.DeleteSchoolAsync(id);
        return NoContent();
    }
}

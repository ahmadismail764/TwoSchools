using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.Extensions;
using TwoSchools.DTOs;

namespace TwoSchools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolYearController : ControllerBase
{
    private readonly SchoolYearService _schoolYearService;

    public SchoolYearController(SchoolYearService schoolYearService)
    {
        _schoolYearService = schoolYearService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SchoolYearResponse>>> GetAllSchoolYears()
    {
        var schoolYears = await _schoolYearService.GetAllSchoolYearsAsync();
        return Ok(schoolYears.Select(sy => sy.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SchoolYearResponse>> GetSchoolYear(int id)
    {
        var schoolYear = await _schoolYearService.GetSchoolYearByIdAsync(id);
        if (schoolYear == null)
            return NotFound();
        
        return Ok(schoolYear.ToResponse());
    }

    [HttpGet("current")]
    public async Task<ActionResult<SchoolYearResponse>> GetCurrentSchoolYear()
    {
        var schoolYear = await _schoolYearService.GetCurrentSchoolYearAsync();
        if (schoolYear == null)
            return NotFound("No current school year found");
        
        return Ok(schoolYear.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<SchoolYearResponse>> CreateSchoolYear([FromBody] CreateSchoolYearRequest request)
    {
        var schoolYear = await _schoolYearService.CreateSchoolYearAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetSchoolYear), new { id = schoolYear.Id }, schoolYear.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SchoolYearResponse>> UpdateSchoolYear(int id, [FromBody] UpdateSchoolYearRequest request)
    {
        var entity = request.ToEntity();
        entity.Id = id;
        var schoolYear = await _schoolYearService.UpdateSchoolYearAsync(entity);
        if (schoolYear == null)
            return NotFound();
        
        return Ok(schoolYear.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSchoolYear(int id)
    {
        await _schoolYearService.DeleteSchoolYearAsync(id);
        return NoContent();
    }
}

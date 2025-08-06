using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;
using TwoSchools.Extensions;
using TwoSchools.DTOs;

namespace TwoSchools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TermController : ControllerBase
{
    private readonly TermService _termService;

    public TermController(TermService termService)
    {
        _termService = termService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TermResponse>>> GetAllTerms()
    {
        var terms = await _termService.GetAllTermsAsync();
        return Ok(terms.Select(t => t.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TermResponse>> GetTerm(int id)
    {
        var term = await _termService.GetTermByIdAsync(id);
        if (term == null)
            return NotFound();
        
        return Ok(term.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<TermResponse>> CreateTerm([FromBody] CreateTermRequest request)
    {
        var term = await _termService.CreateTermAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetTerm), new { id = term.Id }, term.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TermResponse>> UpdateTerm(int id, [FromBody] UpdateTermRequest request)
    {
        var entity = request.ToEntity();
        entity.Id = id;
        var term = await _termService.UpdateTermAsync(entity);
        if (term == null)
            return NotFound();
        
        return Ok(term.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTerm(int id)
    {
        await _termService.DeleteTermAsync(id);
        return NoContent();
    }
}

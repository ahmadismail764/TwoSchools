using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;

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

    // Note: Service methods need to be implemented
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Subject>>> GetAllSubjects()
    // {
    //     var subjects = await _subjectService.GetAllSubjectsAsync();
    //     return Ok(subjects);
    // }
}

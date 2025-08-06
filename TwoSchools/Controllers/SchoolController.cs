using Microsoft.AspNetCore.Mvc;
using TwoSchools.App.Services;

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

    // Note: Service methods need to be implemented
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<School>>> GetAllSchools()
    // {
    //     var schools = await _schoolService.GetAllSchoolsAsync();
    //     return Ok(schools);
    // }
}

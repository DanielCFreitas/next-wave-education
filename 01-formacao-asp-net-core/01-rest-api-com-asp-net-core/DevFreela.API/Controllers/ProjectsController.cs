using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] string search)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateProjectInputModel model)
    {
        return CreatedAtAction(nameof(GetById), new { id = 1 }, model );
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromQuery] int id, [FromBody] UpdateProjectInputModel model)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromQuery] int id)
    {
        return NoContent();
    }

    [HttpPut("{id}/start")]
    public IActionResult Start([FromQuery] int id)
    {
        return NoContent();
    }
    
    [HttpPut("{id}/complete")]
    public IActionResult Complete([FromQuery] int id)
    {
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public IActionResult Comments([FromQuery] int id, [FromBody] CreateProjectCommentInputModel model)
    {
        return Ok();
    }
}
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string search = "", [FromQuery] int page = 0, [FromQuery] int size = 3)
    {
        var result = _projectService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _projectService.GetById(id);
        if (!result.IsSuccess)return BadRequest(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateProjectInputModel model)
    {
        var result = _projectService.Insert(model);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] UpdateProjectInputModel model)
    {
        var result = _projectService.Update(model);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromQuery] int id)
    {
        var result = _projectService.Delete(id);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpPut("{id}/start")]
    public IActionResult Start([FromQuery] int id)
    {
        var result = _projectService.Start(id);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public IActionResult Complete([FromQuery] int id)
    {
        var result = _projectService.Complete(id);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpPost("/comments")]
    public IActionResult Comments([FromQuery] int id, [FromBody] CreateProjectCommentInputModel model)
    {
        var result = _projectService.InsertComment(id, model);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }
}
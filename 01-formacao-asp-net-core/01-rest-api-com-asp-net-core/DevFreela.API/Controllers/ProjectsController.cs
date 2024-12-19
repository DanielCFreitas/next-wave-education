using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly DevFreelaDbContext _context;

    public ProjectsController(DevFreelaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string search = "")
    {
        var projects =
            _context.Projects
                .Include(project => project.Client)
                .Include(project => project.Freelancer)
                .Where(project => !project.IsDeleted && (search.IsNullOrEmpty() || project.Title.Contains(search) || project.Description.Contains(search)))
                .ToList();

        var model = projects
            .Select(ProjectItemViewModel.FromEntity)
            .ToList();

        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var project = _context.Projects
            .Include(project => project.Client)
            .Include(project => project.Freelancer)
            .Include(project => project.Comments)
            .SingleOrDefault(project => project.Id == id);

        var model = ProjectItemViewModel.FromEntity(project);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateProjectInputModel model)
    {
        var project = model.ToEntity();

        _context.Projects.Add(project);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] UpdateProjectInputModel model)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return NotFound();

        project.Update(model.Title, model.Description, model.TotalCost);

        _context.Projects.Update(project);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromQuery] int id)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return NotFound();

        project.SetAsDeleted();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPut("{id}/start")]
    public IActionResult Start([FromQuery] int id)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return NotFound();

        project.Start();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public IActionResult Complete([FromQuery] int id)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return NotFound();

        project.Complete();
        _context.Projects.Update(project);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public IActionResult Comments([FromQuery] int id, [FromBody] CreateProjectCommentInputModel model)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return NotFound();

        var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

        _context.ProjectComments.Add(comment);
        _context.SaveChanges();

        return Ok();
    }
}
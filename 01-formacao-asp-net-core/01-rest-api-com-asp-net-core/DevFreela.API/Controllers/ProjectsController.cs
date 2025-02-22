using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] string search = "", [FromQuery] int page = 0,
        [FromQuery] int size = 3)
    {
        var query = new GetAllProjectsQuery(search, page, size);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetProjectByIdQuery() { Id = id };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InsertProjectCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess) 
            return BadRequest(result.Message);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var command = new DeleteProjectCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start([FromQuery] int id)
    {
        var command = new StartProjectCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete([FromQuery] int id)
    {
        var command = new CompleteProjectCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }

    [HttpPost("/comments")]
    public async Task<IActionResult> Comments([FromQuery] int id, [FromBody] InsertCommentCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result.Message);
        return NoContent();
    }
}
using DevFreela.Application.Commands.InsertSkillsToUser;
using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.SetUserProfilePicture;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess) return NotFound(result.Message);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InsertUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }

    [HttpPost("{id}/skills")]
    public async Task<IActionResult> PostSkills([FromRoute] int id, InsertSkillsToUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result.Message);

        return Ok();
    }

    [HttpPut("{id}/profile-picture")]
    public async Task<IActionResult> PutProfilePicture(IFormFile file)
    {
        var command = new SetUserProfilePictureCommand(file.FileName, file.Length);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest();

        return Ok(command.FileName);
    }
}
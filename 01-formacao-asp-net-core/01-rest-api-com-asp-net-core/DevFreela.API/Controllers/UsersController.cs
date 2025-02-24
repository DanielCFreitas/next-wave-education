using DevFreela.Application.Commands.AuthenticateCommand;
using DevFreela.Application.Commands.InsertSkillsToUser;
using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.SetUserProfilePicture;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Notifications;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;

    private readonly IAuthService _authService;
    private readonly IMediator _mediator;
    private readonly IMemoryCache _memoryCache;
    private readonly IEmailService _emailService;

    public UsersController(DevFreelaDbContext dbContext, IAuthService authService, IMediator mediator,
        IMemoryCache memoryCache,
        IEmailService emailService)
    {
        _dbContext = dbContext;

        _authService = authService;
        _mediator = mediator;
        _memoryCache = memoryCache;
        _emailService = emailService;
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
    [AllowAnonymous]
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

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AuthenticateCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("password-recovery/request")]
    public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryInputModel model)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

        if (user is null) return BadRequest();

        var code = new Random().Next(100000, 999999).ToString();
        var cacheKey = $"RecoveryCode:{model.Email}";
        _memoryCache.Set(cacheKey, code, TimeSpan.FromMinutes(10));

        await _emailService.SendAsync(user.Email, "Código de Recuperação", $"Seu código de recuperação é: {code}");

        return NoContent();
    }

    [HttpPost("password-recovery/validate")]
    public IActionResult ValidateRecoveryCode(ValidateRecoveryCodeInputModel model)
    {
        var cacheKey = $"RecoveryCode:{model.Email}";

        if (!_memoryCache.TryGetValue(cacheKey, out string code) || code != model.Code)
            return BadRequest();

        return NoContent();
    }

    [HttpPost("password-recovery/change")]
    public async Task<IActionResult> ChangePassword(ChangePasswordInputModel model)
    {
        var cacheKey = $"RecoveryCode:{model.Email}";

        if (!_memoryCache.TryGetValue(cacheKey, out string code) || code != model.Code)
            return BadRequest();

        _memoryCache.Remove(cacheKey);

        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
        var hash = _authService.ComputeHash(model.NewPassword);
        user.UpdatePassword(hash);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
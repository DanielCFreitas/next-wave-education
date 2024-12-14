using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DevFreelaDbContext _context;

    public UsersController(DevFreelaDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _context.Users
            .Include(user => user.Skills)
            .ThenInclude(skill => skill.Skill)
            .SingleOrDefault(u => u.Id == id);

        if (user is null) return NotFound();

        var model = UserViewModel.FromEntity(user);

        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        var user = new User(model.FullName, model.Email, model.BirthDate);

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("{id}/skills")]
    public IActionResult PostSkills([FromRoute] int id, UserSkillsInputModel model)
    {
        var userSkills = model.SkillsId
            .Select(skillId => new UserSkill(id, skillId));
        
        _context.UsersSkills.AddRange(userSkills);
        _context.SaveChanges();
        
        return Ok();
    }

    [HttpPut("{id}/profile-picture")]
    public IActionResult PutProfilePicture(IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";

        return Ok(description);
    }
}
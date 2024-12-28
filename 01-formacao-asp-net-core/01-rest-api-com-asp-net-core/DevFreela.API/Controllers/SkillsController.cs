using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly DevFreelaDbContext _context;

    public SkillsController(DevFreelaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var skills = _context.Skills
            .Include(skill => skill.UserSkills)
            .ToList();

        var skillsViewModel = skills.Select(SkillViewModel.FromEntity);
        
        return Ok(skillsViewModel);
    }

    [HttpPost]
    public IActionResult Post(CreateSkillInputModel model)
    {
        var skill = new Skill(model.Description);
        
        _context.Skills.Add(skill);
        _context.SaveChanges();
        
        return NoContent();
    }
}
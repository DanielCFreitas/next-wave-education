using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        return Ok();
    }

    [HttpPost("{id}/skills")]
    public IActionResult PostSkills([FromRoute] int id, UserSkillsInputModel model)
    {
        return Ok();
    }

    [HttpPut("{id}/profile-picture")]
    public IActionResult PutProfilePicture(IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";
        
        return Ok(description);
    }
    
}
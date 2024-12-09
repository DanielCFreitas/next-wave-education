﻿using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly FreelanceTotalCostConfig _freelanceTotalCostConfig;
    
    public ProjectsController(IOptions<FreelanceTotalCostConfig> options)
    {
        _freelanceTotalCostConfig = options.Value;
    }
    
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
        if(model.TotalCost < _freelanceTotalCostConfig.Minimum || 
           model.TotalCost > _freelanceTotalCostConfig.Maximum)
            return BadRequest("Numero fora do limite minimo e máximo");
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
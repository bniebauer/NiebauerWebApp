using System;
using Microsoft.AspNetCore.Mvc;
using NiebauerWebApp.Shared.Models;
using NiebauerWebApp.Server.Services;

namespace NiebauerWebApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly CosmosDbService _cosmosDbService;

    public SkillsController(CosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var skills = await _cosmosDbService.GetAllAsync<Skill>("Skills");
        if (skills == null)
        {
            return NotFound();
        }

        return Ok(skills);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Skill skill)
    {
        if (skill == null)
        {
            return BadRequest();
        }

        await _cosmosDbService.AddAsync("Skills", skill, skill.Id);
        return CreatedAtAction(nameof(Get), new { id = skill.Id }, skill);
    }

}

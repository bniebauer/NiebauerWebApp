using System;
using Microsoft.AspNetCore.Mvc;
using NiebauerWebApp.Server.Services;

namespace NiebauerWebApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly CosmosDbService _cosmosDbService;

    public ProjectsController(CosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var projects = await _cosmosDbService.GetAllAsync<Models.Project>("Projects");
        if (projects == null || !projects.Any())
        {
            return NotFound();
        }

        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Models.Project project)
    {
        if (project == null)
        {
            return BadRequest();
        }

        await _cosmosDbService.AddAsync("Projects", project, project.Id);
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }

}

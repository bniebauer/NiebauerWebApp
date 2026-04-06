using System;
using Microsoft.AspNetCore.Mvc;
using NiebauerWebApp.Shared.Models;
using NiebauerWebApp.Server.Services;

namespace NiebauerWebApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkExperienceController : ControllerBase
{
    private readonly CosmosDbService _cosmosDbService;

    public WorkExperienceController(CosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var workExperiences = await _cosmosDbService.GetAllAsync<WorkExperience>("WorkExperience");
        if (workExperiences == null)
        {
            return NotFound();
        }

        return Ok(workExperiences);
    }

    [HttpPost]
    public async Task<IActionResult> Post(WorkExperience workExperience)
    {
        if (workExperience == null)
        {
            return BadRequest();
        }

        await _cosmosDbService.AddAsync("WorkExperience", workExperience, workExperience.Id);
        return CreatedAtAction(nameof(Get), new { id = workExperience.Id }, workExperience);
    }

}

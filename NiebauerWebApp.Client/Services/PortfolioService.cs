using System;
using System.Net.Http.Json;
using NiebauerWebApp.Shared.Models;

namespace NiebauerWebApp.Client.Services;

public class PortfolioService
{
    private readonly HttpClient _http;

    public PortfolioService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<WorkExperience>> GetWorkExperienceAsync()
    {
        var workExperiences = await _http.GetFromJsonAsync<List<WorkExperience>>("api/workexperience");
        if (workExperiences == null)
        {
            return new List<WorkExperience>();
        }
        return workExperiences;
    }

    public async Task<List<Skill>> GetSkillsAsync()
    {
        var skills = await _http.GetFromJsonAsync<List<Skill>>("api/skill");
        if (skills == null)
        {
            return new List<Skill>();
        }
        return skills;
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        var response = await _http.GetAsync("api/projects"); //.GetFromJsonAsync<List<Project>>("api/projects");
        if (!response.IsSuccessStatusCode)
        {
            // Handle error, e.g., log it or throw an exception
            Console.Error.WriteLine($"Failed to fetch projects: {response.StatusCode}");
            return new List<Project>();
        }
        var projects = await response.Content.ReadFromJsonAsync<List<Project>>();

        if (projects == null)
        {
            return new List<Project>();
        }
        return projects;
    }
}

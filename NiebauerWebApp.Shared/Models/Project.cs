using System;
using Newtonsoft.Json;

namespace NiebauerWebApp.Shared.Models;

public class Project
{  
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new List<string>();
    public string GitHubUrl { get; set; } = string.Empty;

}

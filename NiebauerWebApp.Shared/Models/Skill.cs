using System;
using Newtonsoft.Json;

namespace NiebauerWebApp.Shared.Models;

public class Skill
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}

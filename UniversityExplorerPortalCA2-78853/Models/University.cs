using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UniversityExplorerPortalCA2_78853.Models;

public class University
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } =  string.Empty;

    [JsonPropertyName("web_pages")] public List<string> WebPages { get; set; } = new();
    [JsonPropertyName("domains")] public List<string> Domains { get; set; } = new();
    
    public String PrimaryWebsite => WebPages.FirstOrDefault() ?? string.Empty;
    public String PrimaryDomain => Domains.FirstOrDefault() ?? string.Empty;

}
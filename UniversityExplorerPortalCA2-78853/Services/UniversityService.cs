using System.Net.Http.Json;
using System.Text.Json;
using UniversityExplorerPortalCA2_78853.Models; 

namespace UniversityExplorerPortalCA2_78853.Services;

public class UniversityService (HttpClient httpClient)
{

    public async Task<List<University>> GetUniversitiesByCountryAsync(string country)
    {
       var url = $"http://universities.hipolabs.com/search?country={Uri.EscapeDataString(country)}";
       var universities = await httpClient.GetFromJsonAsync<List<University>>(url);
       return universities ?? new List<University>();
    }
}
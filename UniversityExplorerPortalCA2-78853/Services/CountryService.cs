using System.Net.Http.Json;
using System.Text.Json;
using UniversityExplorerPortalCA2_78853.Models; 

namespace UniversityExplorerPortalCA2_78853.Services;

public class CountryService(HttpClient httpClient)
{
    private const string BaseUrl = "https://countries.dev/countries";

    public async Task<List<Country>> GetCountriesAsync()
    {
        try
        {
            var countries = await httpClient.GetFromJsonAsync<List<Country>>(BaseUrl);
            return (countries ?? new List<Country>())
                .OrderBy(c => c.Name)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<Country>();
        }
    }
}
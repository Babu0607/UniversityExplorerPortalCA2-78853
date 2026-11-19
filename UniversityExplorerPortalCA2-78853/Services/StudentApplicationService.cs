using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using UniversityExplorerPortalCA2_78853.Models;

namespace UniversityExplorerPortalCA2_78853.Services;

public class StudentApplicationService(HttpClient httpClient)
{
    private const string BaseUrl = "https://edutots.net/api/studentapplication";

    // Holds the last API error so pages can display it.
    public string? LastError { get; private set; }

    public async Task<List<StudentApplication>> GetApplicationsAsync()
    {
        var apps = await httpClient.GetFromJsonAsync<List<StudentApplication>>(BaseUrl);
        return apps?.OrderBy(a => a.Id).ToList() ?? new List<StudentApplication>();
    }

    public async Task<StudentApplication?> GetApplicationByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<StudentApplication>();
    }

    // Builds the JSON by hand so the field names always match the API.
    private static Dictionary<string, object?> BuildPayload(StudentApplication a)
    {
        return new Dictionary<string, object?>
        {
            ["studentName"] = a.StudentName,
            ["email"] = a.Email,
            ["emailAddress"] = a.Email,
            ["country"] = a.Country,
            ["university"] = a.UniversityName,
            ["course"] = a.CourseName,
            ["applicationDate"] = a.ApplicationDate?.ToString("yyyy-MM-ddTHH:mm:ss"),
            ["personalStatement"] = a.PersonalStatement ?? string.Empty
        };
    }

    public async Task<bool> CreateApplicationAsync(StudentApplication application)
    {
        LastError = null;
        try
        {
            var payload = BuildPayload(application);
            var response = await httpClient.PostAsJsonAsync(BaseUrl, payload);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                LastError = $"{(int)response.StatusCode} {response.StatusCode}: {body}  |  SENT: {JsonSerializer.Serialize(payload)}";
                Console.WriteLine($"API REJECTED POST ({LastError})");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            LastError = $"Exception: {ex.Message}";
            Console.WriteLine($"EXCEPTION: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateApplicationAsync(int id, StudentApplication application)
    {
        LastError = null;
        try
        {
            var payload = BuildPayload(application);
            payload["id"] = id;   // API requires the id in the body to match the URL

            var response = await httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", payload);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                LastError = $"{(int)response.StatusCode} {response.StatusCode}: {body}  |  SENT: {JsonSerializer.Serialize(payload)}";
                Console.WriteLine($"API REJECTED PUT ({LastError})");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            LastError = $"Exception: {ex.Message}";
            Console.WriteLine($"EXCEPTION: {ex.Message}");
            return false;
        }
    }
}
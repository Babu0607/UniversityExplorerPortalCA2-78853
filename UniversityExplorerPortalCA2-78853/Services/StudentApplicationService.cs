using System.Net;
using System.Net.Http.Json;
using UniversityExplorerPortalCA2_78853.Models; 

namespace UniversityExplorerPortalCA2_78853.Services;

public class StudentApplicationService(HttpClient httpClient)
{
    private const string BaseUrl = "https://edutots.net/api/studentapplication";

    public async Task<List<StudentApplication>> GetApplicationsAsync()
    {
        var apps = await httpClient.GetFromJsonAsync<List<StudentApplication>>(BaseUrl);
        return apps?.OrderBy(a => a.Id).ToList() ?? new List<StudentApplication>();
    }

    public async Task<StudentApplication?> GetApplicationByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        return await response.Content.ReadFromJsonAsync<StudentApplication>();
    }

    public async Task<bool> CreateApplicationAsync(StudentApplication application)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(BaseUrl, application);
            
            // If the API rejects it, log the exact error message returned by edutots.net
            if (!response.IsSuccessStatusCode) return response.IsSuccessStatusCode;
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API REJECTED POST ({response.StatusCode}): {errorBody}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"EXCEPTION: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateApplicationAsync(int id, StudentApplication application)
    {
        try
        {
            application.Id = id;
            var response = await httpClient.PutAsJsonAsync($"studentapplication/{id}", application);

            if (response.IsSuccessStatusCode) return response.IsSuccessStatusCode;
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API REJECTED PUT ({response.StatusCode}): {errorBody}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"EXCEPTION: {ex.Message}");
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UniversityExplorerPortalCA2_78853.Models;

public class StudentApplication
{
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please type your student Name.")]
    [StringLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
    [JsonPropertyName("studentName")]
    public string StudentName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please type your student EmailAddress.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please type your Country.")]
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please type your University Name.")]
    [JsonPropertyName("university")]
    public string UniversityName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please type your Course Name.")]
    [JsonPropertyName("course")]
    public string CourseName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please type your Application Date.")]
    [JsonPropertyName("applicationDate")]
    public DateTime? ApplicationDate { get; set; } = DateTime.Today;
    
    [JsonPropertyName("personalStatement")]
    public string PersonalStatement { get; set; } = string.Empty;
}
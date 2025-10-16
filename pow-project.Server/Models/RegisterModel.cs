using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class RegisterModel
{
    [JsonPropertyName("user_name")]
    [Required]
    public string userName { get; set; } = default!;

    [Required, EmailAddress]
    public string email { get; set; } = default!;

    [Required]
    public string password { get; set; } = default!;
}

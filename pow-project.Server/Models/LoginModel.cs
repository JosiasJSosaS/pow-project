using System.ComponentModel.DataAnnotations;

namespace pow_project.Server.Models
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public required string email { get; set; } = default!;

        [Required]
        public required string password { get; set; } = default!;
    }
}

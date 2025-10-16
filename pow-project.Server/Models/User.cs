using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace pow_project.Server.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string? DisplayName { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public long ListCount { get; set; } = 0;

        public virtual ICollection<MovieList> Lists { get; set; } = new HashSet<MovieList>();
    }
}

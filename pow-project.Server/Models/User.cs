using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pow_project.Server.Models
{
    public class User : IdentityUser
    {
        public User() : base() {
            joined = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long userId { get; set; }

        public string? userName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime joined { get; set; }
        public long listCount { get; set; }

        public virtual ICollection<MovieList>? lists { get; set; } = null;
    }
}

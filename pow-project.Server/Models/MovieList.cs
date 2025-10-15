using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pow_project.Server.Models
{
    [Table ("List")]
    public class MovieList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long listId { get; set; }
        public string? name { get; set; }

        public virtual User? createdBy { get; set; }
        public virtual ICollection<Movie>? movies { get; set; }
        public virtual ICollection<Comment>? comments { get; set; } = null;
    }
}

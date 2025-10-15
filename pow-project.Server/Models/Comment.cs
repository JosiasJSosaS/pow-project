using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pow_project.Server.Models
{
    [Table ("Comment")]
    public class Comment
    {
        Comment()
        {
            createdAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long commentId { get; set; }
        public string? content { get; set; }
        public DateTime createdAt { get; set; }
        public virtual User? createdBy { get; set; }
    }
}
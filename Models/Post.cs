using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property for the owner (user)
        public virtual IdentityUser Owner { get; set; }

        [Required]
        public byte[] PostImage { get; set; }
    }
}

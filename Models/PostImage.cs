using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class PostImage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Owner")]
        public string? OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property for the owner (user)
        public virtual IdentityUser? Owner { get; set; }

        public byte[]? ImageFile { get; set; }

        [NotMapped]
        public IFormFile? PostImageFiles { get; set; }
        // Foreign key property for the associated post
        public int PostId { get; set; }

        // Navigation property for the associated post
        public virtual Post? Post { get; set; }
    }
}

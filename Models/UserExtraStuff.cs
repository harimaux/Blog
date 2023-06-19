using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class UserExtraStuff
    {
        [Key]
        public int Id { get; set; }

        public byte[]? Avatar { get; set; }

        public string? Likes { get; set; }

        public string? Dislikes { get; set; }

        public string? Followers { get; set; }

        public string? NumberOfPosts { get; set; }

        public DateTime LastSeen { get; set; }

        public string? AdditionalInfo { get; set; }

        public string? AdditionalInfo2 { get; set; }

        public string? AdditionalInfo3 { get; set; }

        [ForeignKey("Owner")]
        public string? UserId { get; set; }

        // Navigation property for the owner (user)
        public virtual IdentityUser? Owner { get; set; }

    }
}

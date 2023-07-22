using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class PostCategory
    {
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }
    }
}

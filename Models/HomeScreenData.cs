using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class HomeScreenData
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Views { get; set; }

        public bool IsPopular { get; set; }

        public bool IsNew { get; set; }

        public string? Category { get; set; }
    }
}

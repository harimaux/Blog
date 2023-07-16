using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace Blog.Models
{
    public class TextEditor
    {
        //public string? Title { get; set; }
        //public string? RichContent { get; set; }
        //public string? Category { get; set; }

        [Required(ErrorMessage = "Please enter the post title.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Please select at least one category.")]
        public string[]? Category { get; set; }

        [Required(ErrorMessage = "Please enter the post content.")]
        public string? RichContent { get; set; }
    }
}

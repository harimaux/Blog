namespace Blog.Models
{
    public class CreatePostFormData
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? PostImageFile { get; set; }
        public string? Category { get; set; }
    }
}

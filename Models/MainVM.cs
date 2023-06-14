namespace Blog.Models
{
    public class MainVM
    {
        public Post PostModel { get; set; }
        public PostImage PostImage { get; set; }
        public CreatePostFormData CreatePostFormData { get; set; }

        public bool SetPost { get; set; }

        public MainVM()
        {
            PostModel = new Post();

            PostImage = new PostImage();

            CreatePostFormData = new CreatePostFormData();
        }

    }
}

namespace Blog.Models
{
    public class DisplayUserPosts
    {
        public Post? UserPost { get; set; }
        public List<PostImage>? PostImages { get; set; }
    }
}

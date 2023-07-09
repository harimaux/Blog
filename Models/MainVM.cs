using System.Buffers.Text;

namespace Blog.Models
{
    public class MainVM
    {
        public Post Post { get; set; }
        public PostImage PostImage { get; set; }
        public UserExtraStuff UserExtraStuff { get; set; }
        public bool SetPost { get; set; }
        public List<byte[]>? ImagePreview { get; set; }
        public List<Post>? PostsList { get; set; }
        public TextEditor TextEditor { get; set; }


        public MainVM()
        {
            Post = new Post();

            PostImage = new PostImage();

            UserExtraStuff = new UserExtraStuff();

            TextEditor = new TextEditor();

            PostsList = new List<Post>();

        }

    }
}

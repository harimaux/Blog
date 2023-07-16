using System.Buffers.Text;

namespace Blog.Models
{
    public class MainVM
    {
        public Post Post { get; set; }
        public UserExtraStuff UserExtraStuff { get; set; }
        public bool SetPost { get; set; }
        public List<byte[]>? ImagePreview { get; set; }
        public List<Post>? PostsList { get; set; }
        public TextEditor TextEditor { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public MainVM()
        {
            Post = new Post();

            UserExtraStuff = new UserExtraStuff();

            TextEditor = new TextEditor();

            PostsList = new List<Post>();

        }

    }
}

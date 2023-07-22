using Blog.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Buffers.Text;

namespace Blog.Models
{
    public class MainVM
    {
        private readonly ApplicationDbContext _dbContext;

        public Post Post { get; set; }
        public UserExtraStuff UserExtraStuff { get; set; }
        public List<Post>? PostsList { get; set; }
        public TextEditor TextEditor { get; set; }
        public List<PostCategory>? PostCategory { get; set; }
        public List<StockAvatars>? StockAvatars { get; set; }

        //Pagination on user post page
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public MainVM(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Post = new Post();

            UserExtraStuff = new UserExtraStuff();

            TextEditor = new TextEditor();

            PostsList = new List<Post>();

            PostCategory = new List<PostCategory>();

            StockAvatars = new List<StockAvatars>();
        }

    }
}

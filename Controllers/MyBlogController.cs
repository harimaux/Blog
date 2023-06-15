using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blog.Controllers
{
    public class MyBlogController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MyBlogController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        public ApplicationDbContext Get_dbContext()
        {
            return _dbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            // Get logged user details
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            // Get user posts
            var userPosts = _dbContext.Posts!.Where(p => p.OwnerId == userId).ToList();
            var userImages = _dbContext.PostImages!.Where(p => p.OwnerId == userId).ToList();

            MainVM vm = new();

            var userPostViewModels = userPosts.Select(post =>
            {
                PostImage? postImage = userImages.FirstOrDefault(image => image.PostId == post.Id);
                return new DisplayUserPosts
                {
                    UserPost = post,
                    PostImage = postImage
                };
            }).ToList();

            vm.DisplayUserContent = userPostViewModels;

            vm.SetPost = false;


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostFormData formData)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var newPost = new Post
                {
                    Title = formData.Title,
                    Content = formData.Content,
                    OwnerId = currentUser.Id,
                    CreatedAt = DateTime.Now,
                    Category = formData.Category
                };

                _dbContext.Posts!.Add(newPost);
                await _dbContext.SaveChangesAsync();

                var newPostImage = new PostImage();

                if (formData.PostImageFile != null && formData.PostImageFile.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await formData.PostImageFile.CopyToAsync(memoryStream);
                    newPostImage.ImageFile = memoryStream.ToArray();
                }

                newPostImage.Name = formData.PostImageFile!.FileName;
                newPostImage.Description = formData.Category;
                newPostImage.OwnerId = currentUser.Id;
                newPostImage.CreatedAt = DateTime.Now;
                newPostImage.PostId = newPost.Id;

                _dbContext.PostImages!.Add(newPostImage);
                await _dbContext.SaveChangesAsync();

                var newVM = new MainVM
                {
                    PostModel = newPost,
                    ImagePreview = newPostImage.ImageFile,
                    SetPost = true
                };

                return PartialView("_Post", newVM);
            }

            return BadRequest();
        }

    }
}

using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Data.SqlClient.Server;
using System.Drawing;

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

            var userPostViewModels = userPosts.Select(post =>
            {
                var postImages = userImages.Where(image => image.PostId == post.Id).ToList();
                return new DisplayUserPosts
                {
                    UserPost = post,
                    PostImages = postImages
                };
            }).ToList();

            var vm = new MainVM
            {
                DisplayUserContent = userPostViewModels,
                SetPost = false
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostFormData formData)
        {
            // Check for null values in inputs
            if (string.IsNullOrEmpty(formData.Title) || string.IsNullOrEmpty(formData.Content) || string.IsNullOrEmpty(formData.Category) || formData.PostImageFile == null)
            {
                return BadRequest("Stop hacking!!!");
            }

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

                var imagePreviews = new List<byte[]>();

                foreach (var img in formData.PostImageFile!)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await img.CopyToAsync(memoryStream);
                        var newPostImage = new PostImage
                        {
                            ImageFile = memoryStream.ToArray(),
                            Name = img.FileName,
                            Description = formData.Category,
                            OwnerId = currentUser.Id,
                            CreatedAt = DateTime.Now,
                            PostId = newPost.Id
                        };
                        _dbContext.PostImages!.Add(newPostImage);

                        imagePreviews.Add(memoryStream.ToArray());
                    }
                }

                await _dbContext.SaveChangesAsync();

                var newVM = new MainVM
                {
                    PostModel = newPost,
                    ImagePreview = imagePreviews,
                    SetPost = true
                };

                return PartialView("_Post", newVM);
            }

            return BadRequest();
        }


    }
}

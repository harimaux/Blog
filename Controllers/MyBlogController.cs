using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

            var vm = new MainVM();

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
                    CreatedAt = DateTime.Now
                };

                _dbContext.Posts!.Add(newPost);

                await _dbContext.SaveChangesAsync();

                var newPostImage = new PostImage();

                if (formData.PostImageFile != null && formData.PostImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formData.PostImageFile.CopyToAsync(memoryStream);
                        newPostImage.ImageFile = memoryStream.ToArray();
                    }
                }

                newPostImage.OwnerId = currentUser.Id;
                newPostImage.CreatedAt = DateTime.Now;

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

            // If the model state is not valid, handle the error
            return BadRequest();
        }

    }
}

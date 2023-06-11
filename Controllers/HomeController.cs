using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Get logged user details
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //--------------------------------------------------------------------------- Photo upload - test

        //[HttpPost]
        //public async Task<IActionResult> Create(NewPostVM model, IFormFile photo)
        //{

        //    var currentUser = _userManager.GetUserAsync(User).Result;


        //    if (ModelState.IsValid)
        //    {
        //        // Create a new instance of the Post entity
        //        var post = new Post
        //        {
        //            Title = model.Title,
        //            Content = model.Content,
        //            OwnerId = currentUser.Id,
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        // Process the photo if it was provided
        //        if (photo != null)
        //        {
        //            using (var stream = new MemoryStream())
        //            {
        //                await photo.CopyToAsync(stream);
        //                post.PostImage = stream.ToArray();
        //            }
        //        }

        //        // Save the post to the database
        //        _dbContext.Posts.Add(post);
        //        await _dbContext.SaveChangesAsync();

        //        return RedirectToAction("Index");
        //    }

        //    // If the model state is not valid, return the view with validation errors
        //    return View(model);
        //}
    }
}
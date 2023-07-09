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
using System.Text.Json;
using Quill.Delta;
using Newtonsoft.Json.Linq;
using System.Xml;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using Microsoft.CodeAnalysis;

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


            if (user != null && _dbContext.Posts != null)
            {
                var userPosts = _dbContext.Posts.Where(x => x.OwnerId == userId).ToList();

                vm.PostsList = userPosts;
            }

            return View(vm);

        }


        [HttpPost]
        public async Task<IActionResult> GetPost(TextEditor formData)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newPost = new Post
            {
                Title = formData.Title,
                Category = "test cat",
                Content = formData.RichContent,
                OwnerId = userId,
                CreatedAt = DateTime.Now
            };


            _dbContext.Posts!.Add(newPost);
            await _dbContext.SaveChangesAsync();

            return View("Index");
        }


    }
}


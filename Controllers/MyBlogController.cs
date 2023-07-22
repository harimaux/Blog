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
        public IActionResult Index(int page = 1)
        {
            // Get logged user details
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            var vm = new MainVM(_dbContext);

            //Get post categories
            if(user != null && _dbContext.PostCategory != null)
            {
                vm.PostCategory = _dbContext.PostCategory.ToList();
            }

            // Get user posts
            if (user != null && _dbContext.Posts != null)
            {
                int pageSize = 3; // Number of posts to display per page
                var userPosts = _dbContext.Posts
                    .Where(x => x.OwnerId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();

                int totalCount = userPosts.Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure the requested page is within the valid range
                page = Math.Max(1, Math.Min(page, totalPages));

                // Get the posts for the requested page
                var postsForPage = userPosts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                vm.PostsList = postsForPage;
                vm.CurrentPage = page;
                vm.TotalPages = totalPages;
            }

            return View(vm);

        }



        [HttpPost]
        public async Task<IActionResult> SavePost(TextEditor formData)
        {
            var vm = new MainVM(_dbContext);

            if (!ModelState.IsValid)
            {
                // Model is not valid, return the form with validation errors
                return PartialView("PostError", formData);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newPost = new Post
            {
                Title = formData.Title,
                Category = string.Join(",", formData.Category ?? Array.Empty<string>()),
                Content = formData.RichContent,
                OwnerId = userId,
                CreatedAt = DateTime.Now
            };

            _dbContext.Posts!.Add(newPost);
            await _dbContext.SaveChangesAsync();

            vm.Post = newPost;

            return PartialView("_Post", vm);
        }



        [HttpPost]
        public async Task<IActionResult> DeletePost(int postId)
        {
            if(_dbContext.Posts != null)
            {
                var post = await _dbContext.Posts.FindAsync(postId);

                if (post == null)
                {
                    return NotFound(); // Or any appropriate error response
                }

                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();

                // Redirect to an appropriate view or action
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}


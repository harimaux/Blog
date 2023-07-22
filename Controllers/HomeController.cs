using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
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

            //var vm = new MainVM(_dbContext);

            //var newfef = new Avatar();

            ////Sets stock avatars object
            //vm.StockAvatars = _dbContext.StockAvatars?.ToList();

            ////Sets UserExtraStuff
            //vm.UserExtraStuff = _dbContext.UserExtraStuff?.FirstOrDefault(x => x.UserId == userId) ?? new UserExtraStuff();


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(StockAvatars model, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        model.ImageBase64 = Convert.ToBase64String(ms.ToArray());
                    }
                }

                _dbContext.Add(model);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect to a page showing all uploaded images.
            }

            return View(model);
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

    }
}
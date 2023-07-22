using Blog.Controllers;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.Areas.Identity.Pages.Account.Manage
{
    public class AvatarModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public AvatarModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add a property to store the MainVM model
        public MainVM? Mv { get; set; }

        public UserExtraStuff? UserExSt { get; set; }

        public IActionResult OnGet()
        {
            // Pass the ApplicationDbContext to MainVM's constructor
            Mv = new MainVM(_dbContext);

            if(_dbContext.StockAvatars != null)
            {
                Mv.StockAvatars = _dbContext.StockAvatars.ToList();
            }
            

            ViewData["Title"] = "Avatar Page";


            return Page();
        }

        public IActionResult OnPostChooseAvatar(int avatarId)
        {
            Mv = new MainVM(_dbContext);
            Mv.UserExtraStuff.StockAvatarId = avatarId.ToString();

            if (_dbContext.UserExtraStuff != null)
            {
                _dbContext.UserExtraStuff.Update(Mv.UserExtraStuff);
                _dbContext.SaveChanges();
            }

            return RedirectToPage("Avatar");
        }

        public IActionResult OnPostUploadAvatar(IFormFile customAvatar)
        {
            // This handler will be executed when the "Upload Avatar" button is clicked
            // in the second form.

            // Use the customAvatar parameter to access the uploaded file.
            // For example, you can save the file to a specific location or database.

            // Perform your logic here.

            return RedirectToPage("Avatar");
        }

    }
}


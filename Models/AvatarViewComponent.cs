// AvatarViewComponent.cs
using Microsoft.AspNetCore.Mvc;
using Blog.Models; // Replace with the appropriate namespace for your models
using Blog.Areas.Identity.Pages.Account.Manage;

public class AvatarViewComponent : ViewComponent
{
    private readonly UserExtraStuff _userExtraStuff; // Replace with your data context or service

    public AvatarViewComponent(UserExtraStuff userExtraStuff)
    {
        _userExtraStuff = userExtraStuff;
    }


    public IViewComponentResult Invoke()
    {
        // Fetch avatar data or build the necessary model for the avatar
        var model = new StockAvatars(); // Replace AvatarModel with the appropriate model for the avatar
        return View(model);
    }
}

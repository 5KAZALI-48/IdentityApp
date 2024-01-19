using IdentityApp.Areas.Admin.Models;
using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public HomeController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> UserList()
    {
        var userList = await _userManager.Users.ToListAsync();
        var userViewModelList = userList.Select(userList => new AdminUserViewModel()
        {
            Id = userList.Id,
            Email = userList.Email,
            Name = userList.UserName
        }).ToList();
        return View(userViewModelList);
    }
}

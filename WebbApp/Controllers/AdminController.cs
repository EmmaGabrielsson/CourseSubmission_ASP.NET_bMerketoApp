using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.Identities;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly UserService _userService;

    public AdminController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Admin - Dashboard";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(UpdateRoleViewModel model)
    {
        ViewData["Title"] = "Admin - Dashboard";

        if (ModelState.IsValid)
        {
            var update = await _userService.UpdateUserRoleAsync(model);
            if (update)
                return RedirectToAction("index", "admin");
        }

        ModelState.AddModelError("", "Something went wrong when trying to change users role.");
        return View(model);
    }
}
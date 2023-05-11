using Microsoft.AspNetCore.Mvc;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

public class HomeController : Controller
{
    private readonly SubscribeService _subscribeService;

    public HomeController(SubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Home";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(EmailViewModel model)
    {
        ViewData["Title"] = "Home";

        if (ModelState.IsValid)
        {
            if (await _subscribeService.ExistAsync(model))
            {
                ModelState.AddModelError("", "You are already a subsciber for our newsletter.");
                return View(model);
            }

            if (await _subscribeService.RegisterForSubscribeAsync(model))
            {
                ModelState.AddModelError("", "Welcome! You are now a subscriber for our newsletter");
                return View(model);
            }

        }
        ModelState.AddModelError("", "You have entered an incorrect email");
        return View(model);
    }

}

using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.Entities;
using WebbApp.Models.ViewModels;
using WebbApp.Repositories;
using WebbApp.Services;

namespace WebbApp.Controllers;

public class HomeController : Controller
{
    #region Constructors & Private Fields
    private readonly ProductService _productService;
    private readonly CategoryRepo _categoryRepo;
    private readonly SubscribeRepo _subscribeRepo;
    private readonly SeedService _seedService;

    public HomeController(ProductService productService, CategoryRepo categoryRepo, SubscribeRepo subscribeRepo, SeedService seedService)
    {
        _productService = productService;
        _categoryRepo = categoryRepo;
        _subscribeRepo = subscribeRepo;
        _seedService = seedService;
    }
    #endregion
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Home";
        await _seedService.CreateInitializedDataAsync();

        var latestShowcase = await _productService.GetLatestShowcaseAsync();
        ViewBag.LatestShowcase = latestShowcase;

        var categories = await _categoryRepo.GetAllDataAsync();
        ViewBag.Categories = categories;

        var bestCollection = await _productService.GetBestCollectionAsync();
        if(bestCollection != null)
            ViewBag.BestCollection = bestCollection;

        var onSaleItems = await _productService.GetAllOnSaleItemsAsync();
        Random random = new();
        GridCollectionItemViewModel randomItem = onSaleItems[random.Next(onSaleItems.Count)];
        ViewBag.RandomItem = randomItem;

        var topSaleItemsList = await _productService.GetAllTopSaleProductsAsync();
        ViewBag.TopSaleItemsList = topSaleItemsList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(EmailViewModel model)
    {
        ViewData["Title"] = "Home";

        if (ModelState.IsValid)
        {
            if (await _subscribeRepo.GetIdentityAsync(x => x.Email == model.Email) != null)
            {
                ModelState.AddModelError("", "You are already a subsciber for our newsletter.");
                return View(model);
            }
            SubscriberEntity subscriber = new()
            {
                Email = model.Email
            };
            if (await _subscribeRepo.AddIdentityAsync(subscriber) != null)
            {
                ModelState.AddModelError("", "Welcome! You are now a subscriber for our newsletter");
                return View(model);
            }

        }
        ModelState.AddModelError("", "You have entered an incorrect email");
        return View(model);
    }

}

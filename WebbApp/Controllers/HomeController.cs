using Microsoft.AspNetCore.Mvc;
using WebbApp.Repositories;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

public class HomeController : Controller
{
    private readonly SubscribeService _subscribeService;
    private readonly ProductService _productService;
    private readonly CategoryRepo _categoryRepo;

    public HomeController(SubscribeService subscribeService, ProductService productService, CategoryRepo categoryRepo)
    {
        _subscribeService = subscribeService;
        _productService = productService;
        _categoryRepo = categoryRepo;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Home";
        await _productService.CreateInitializedDataAsync();

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

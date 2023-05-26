using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.ViewModels;
using WebbApp.Services;

namespace WebbApp.Controllers;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    #region Constructors & Private Fields

    private readonly UserService _userService;
    private readonly ProductService _productService;
    public AdminController(UserService userService, ProductService productService)
    {
        _userService = userService;
        _productService = productService;
    }
    #endregion

    #region UserList and RoleChange (https://domain.com/admin)
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
            var update = await _userService.ChangeUserRoleAsync(model);
            if (update)
                return RedirectToAction("index", "admin");

            ModelState.AddModelError("", "Something went wrong when trying to change users role. Make sure to choose a role and enter a valid user-id.");
        }

        return View(model);
    }

    #endregion

    #region AddProducts (https://domain.com/admin/products)
    public async Task<IActionResult> Products()
    {
        ViewData["Title"] = "Admin - Add Products";
        ViewBag.Tags = await _productService.GetTagsAsync();
        ViewBag.Categories = await _productService.GetCategoriesAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Products(ProductRegisterViewModel model, string[] tags, string[] categories)
    {
        ViewData["Title"] = "Admin - Add Products";
        ViewBag.Tags = await _productService.GetTagsAsync(tags);
        ViewBag.Categories = await _productService.GetCategoriesAsync(categories);

        if (ModelState.IsValid)
        {
            var product = await _productService.CreateAsync(model);
            if (product != null)
            {
                if (await _productService.UploadImageAsync(product, model.Image))
                {
                    if (tags.Any())
                        await _productService.AddProductTagsAsync(product, tags);

                    if (categories.Any())
                        await _productService.AddProductCategoriesAsync(product, categories);

                    return RedirectToAction("Products");

                }
                ModelState.AddModelError("", "Could not upload product-image.");
                return View(model);
            }
            ModelState.AddModelError("", "Something went wrong when trying to add product.");
            return View(model);
        }
        ModelState.AddModelError("", "You have not entered valid input in all requiered fields.");
        return View(model);
    }

    #endregion
}
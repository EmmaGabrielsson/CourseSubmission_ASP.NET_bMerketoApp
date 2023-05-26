using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbApp.Models.Identities;
using WebbApp.Models.ViewModels;
using WebbApp.Services;

namespace WebbApp.Controllers;

public class AccountController : Controller
{
    #region Constructors & Private Fields

    private readonly UserService _userService;
    private readonly AdressService _adressService;
    private readonly UserManager<AppUser> _userManager;
    public AccountController(UserService userService, AdressService adressService, UserManager<AppUser> userManager)
    {
        _userService = userService;
        _adressService = adressService;
        _userManager = userManager;
    }
    #endregion

    #region My Account (https://domain.com/account)
    [Authorize]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "My account";

        var userInfo = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);
        if(userInfo != null)
        {
            var adresses = await _adressService.GetUserAdressAsync(userInfo);

            ProfileViewModel profileView = userInfo;
            profileView.Adresses = adresses;

            return View(profileView);
        }
        return View();
    }
    #endregion

    #region Register (https://domain.com/account/register)

    public IActionResult Register()
    {
        ViewData["Title"] = "Register new account";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(AccountRegisterViewModel registerViewModel)
    {
        ViewData["Title"] = "Register new account";

        if (ModelState.IsValid)
        {
            if(await _userService.UserExsist(registerViewModel.Email ))
                ModelState.AddModelError("", "There is already a user with the same email address");
            else
            {
                var user = await _userService.RegisterAsync(registerViewModel);
                if (user != null)
                {
                    if (user.ImageUrl != null)
                        await _userService.UploadImageAsync(user, registerViewModel.ImageFile!);

                    return RedirectToAction("login", "account");
                }
                else
                    ModelState.AddModelError("", "Something went wrong when trying to registrate a new user-account.");
            }
        }
        return View(registerViewModel);
    }
    #endregion

    #region Login (https://domain.com/account/login)
    public IActionResult Login()
    {
        ViewData["Title"] = "Login";
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        ViewData["Title"] = "Login";

        if (ModelState.IsValid)
        {
            if(await _userService.LoginAsync(loginViewModel))
                return RedirectToAction("index", "account");

            ModelState.AddModelError("", "You have entered an incorrect password or email");
        }
        return View(loginViewModel);
    }
    #endregion

    #region Logout (https://domain.com/account/logout)
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        ViewData["Title"] = "Logout";
        if (await _userService.LogoutAsync(User))
            return LocalRedirect("/");

        return RedirectToAction("index");
    }
    #endregion

    #region New Password (https://domain.com/account/password)
    public IActionResult ForgotPassword()
    {
        ViewData["Title"] = "New Password";
        return View();
    }
    
    /*
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(EmailViewModel model)
    {
        ViewData["Title"] = "New Password";

        if (ModelState.IsValid)
        {
            // Send an email with this link
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user != null)
            {

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
        }
        return View(model);
    }
    */
    #endregion

}

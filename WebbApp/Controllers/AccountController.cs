using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.ViewModels;
using WebbApp.Repositories;
using WebbApp.Services;

namespace WebbApp.Controllers;

public class AccountController : Controller
{
    #region Constructors & Private Fields

    private readonly UserService _userService;
    private readonly AdressService _adressService;
    private readonly UserRepo _userRepo;
    public AccountController(UserService userService, AdressService adressService, UserRepo userRepo)
    {
        _userService = userService;
        _adressService = adressService;
        _userRepo = userRepo;
    }
    #endregion

    #region My Account (https://domain.com/account)
    [Authorize]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "My account";

        var userInfo = await _userRepo.GetIdentityAsync(x => x.UserName == User.Identity!.Name);
        var adresses = await _adressService.GetUserAdressAsync(userInfo);

        ProfileViewModel profileView = userInfo;
        profileView.Adresses = adresses;

        return View(profileView);
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
            var user = await _userService.GetAsync(x => x.Email == model.Email);
            if (user != null)
                return RedirectToAction("Login", "Account");

            ModelState.AddModelError("", "You have entered an incorrect email");
        }
        if (ModelState.IsValid)
        {
            // Send an email with this link
            var user = await _userService.GetAsync(x => x.Email == model.Email);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id}, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }
        return View(model);
    }
    */
    #endregion

}

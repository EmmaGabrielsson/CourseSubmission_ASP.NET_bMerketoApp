using Microsoft.AspNetCore.Mvc;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    #region My Account (https://domain.com/account)
    public IActionResult Index()
    {
        ViewData["Title"] = "My account";
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
            if(await _userService.UserExist(x => x.Email == registerViewModel.Email ))
                ModelState.AddModelError("", "There is already a user with the same email address");
            else
            {
                if(await _userService.RegisterAsync(registerViewModel)) 
                    return RedirectToAction("Login", "Account");
                else
                    ModelState.AddModelError("", "Something went wrong when trying to registrate user.");
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
                return RedirectToAction("Index", "Account");

            ModelState.AddModelError("", "You have entered an incorrect password or email");
        }
        return View(loginViewModel);
    }
    #endregion

    #region Logout (https://domain.com/account/logout)
    public IActionResult Logout()
    {
        ViewData["Title"] = "Logout";
        return View();
    }
    #endregion
}

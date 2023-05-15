﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.Dtos;
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
    [Authorize]
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

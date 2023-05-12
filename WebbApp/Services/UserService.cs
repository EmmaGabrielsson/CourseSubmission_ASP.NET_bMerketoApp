using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Security.Claims;
using WebbApp.Contexts;
using WebbApp.Models.Entities;
using WebbApp.Models.Identities;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class UserService
{
    private readonly IdentityContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SeedService _seedService;
    private readonly AdressService _adressService;

    public UserService(IdentityContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, SeedService seedService, AdressService adressService)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _seedService = seedService;
        _adressService = adressService;
    }

    public async Task<bool> UserExist(Expression<Func<AppUser, bool>> predicate)
    {
        if (await _context.Users.AnyAsync(predicate))
            return true;

        return false;
    }
    public async Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> predicate)
    {
        var _userEntity = await _context.Users.FirstOrDefaultAsync(predicate);
        return _userEntity!;
    }
    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
    public async Task<bool> RegisterAsync(AccountRegisterViewModel registerViewModel)
    {
            await _seedService.InitializeRoles();
            var roleName = "user";

            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";
            AppUser _userEntity = registerViewModel;

            var result = await _userManager.CreateAsync(_userEntity, registerViewModel.Password);           
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_userEntity, roleName);
                var _adressEntity = await _adressService.GetOrCreateAsync(registerViewModel);

                if (_adressEntity != null)
                {
                    await _adressService.AddAdressAsync(_userEntity, _adressEntity);
                    return true;
                }
            }            
            return false;        
        /*
                 try
        {
            await _seedService.InitializeRoles();
            var roleName = "user";

            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            AppUser _userEntity = registerViewModel;

            var result = await _userManager.CreateAsync(_userEntity, registerViewModel.Password);           
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_userEntity, roleName);
                var _adressEntity = await _adressService.GetOrCreateAsync(registerViewModel);

                if (_adressEntity != null)
                {
                    await _adressService.AddAdressAsync(_userEntity, _adressEntity);
                    return true;
                }
            }            
        }
        catch
        {
            return false;        
        }

         */
    }
    public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
    {
        try
        {
            var _userEntity = await GetAsync(x => x.Email == loginViewModel.Email);
            if (_userEntity != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);
                return result.Succeeded;
            }
            return false;
        }
        catch { return false; }
    }
    public async Task<bool> LogoutAsync(ClaimsPrincipal user)
    {
        await _signInManager.SignOutAsync();
        return _signInManager.IsSignedIn(user);
    }

    public async Task<bool> ChangePasswordAsync(AppUser user, string newPassword)
    {
        var currentPassword = user.PasswordHash!;
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (result.Succeeded)
            return true;

        return false;
    }

    public async Task<AppUser> UpdateAsync(AppUser model)
    {
        _context.Users.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> UpdateUserRoleAsync(UpdateRoleViewModel model)
    {
        var findRole = await _roleManager.FindByNameAsync(model.Role);

        if (findRole == null)
            return false;

        var user = await GetAsync(x => x.Id == model.UserId);
        var currentRole = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRoleAsync(user, currentRole.First());
        await _context.SaveChangesAsync();
        var result = await _userManager.AddToRoleAsync(user, model.Role);
        await _context.SaveChangesAsync();

        if (result.Succeeded)
            return true;

        return false;
    }

    public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles;
    }
}



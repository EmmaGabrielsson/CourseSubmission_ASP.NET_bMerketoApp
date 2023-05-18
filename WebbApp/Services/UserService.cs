using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebbApp.Models.Identities;
using WebbApp.Models.ViewModels;
using WebbApp.Repositories;

namespace WebbApp.Services;

public class UserService
{
    #region Constructors & Private Fields
    private readonly UserRepo _userRepo;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SeedService _seedService;
    private readonly AdressService _adressService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public UserService(UserRepo userRepo, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, SeedService seedService, AdressService adressService, IWebHostEnvironment webHostEnvironment)
    {
        _userRepo = userRepo;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _seedService = seedService;
        _adressService = adressService;
        _webHostEnvironment = webHostEnvironment;
    }
    #endregion
    public async Task<bool> UserExsist(string email)
    {
        try
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result != null)
                return true;

            return false;
        } catch { return false; }
    }
    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        try
        {
            return await _userRepo.GetAllIdentityAsync();
        }
        catch { return null!; }
    }
    public async Task<AppUser> RegisterAsync(AccountRegisterViewModel registerViewModel)
    {
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
                    return _userEntity;
                }
            }            
            return null!;        
        } catch { return null!; }
    }
    public async Task<bool> UploadImageAsync(AppUser user, IFormFile image)
    {
        try
        {
            string imagePath = $"{_webHostEnvironment.WebRootPath}/images/profiles/{user.ImageUrl}";
            await image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
            return true;
        }
        catch { return false; }
    }
    public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
    {
        try
        {
            var _userEntity = await _userRepo.GetIdentityAsync(x => x.Email == loginViewModel.Email);
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
        try
        {
            await _signInManager.SignOutAsync();
            return _signInManager.IsSignedIn(user);
        } catch { return false; }
    }
    public async Task<bool> UpdateUserRoleAsync(UpdateRoleViewModel model)
    {
        try
        {
            var findRole = await _roleManager.FindByNameAsync(model.Role);

            if (findRole == null)
                return false;

            var user = await _userRepo.GetIdentityAsync(x => x.Id == model.UserId);
            var currentRole = await _userManager.GetRolesAsync(user);
            if (currentRole != null)
                await _userManager.RemoveFromRoleAsync(user, currentRole.First());
            
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
                return true;

            return false;
        }
        catch { return false; }
    }
    public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
    {
        try
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        } catch { return null!; }
    }
    /*
    public async Task<bool> ChangePasswordAsync(AppUser user, string newPassword)
    {
        var currentPassword = user.PasswordHash!;
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (result.Succeeded)
            return true;

        return false;
    }
    */
}



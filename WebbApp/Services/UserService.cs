using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using WebbApp.Contexts;
using WebbApp.Models.Identities;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class UserService
{
    private readonly IdentityContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly SeedService _seedService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(IdentityContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, SeedService seedService, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _seedService = seedService;
        _roleManager = roleManager;
    }

    public async Task<bool> UserExist(Expression<Func<AppUser, bool>> predicate)
    {
        if (!await _context.Users.AnyAsync(predicate))
            return true;

        return false;
    }
    public async Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> predicate)
    {
        var _userEntity = await _context.Users.FirstOrDefaultAsync(predicate);
        return _userEntity!;
    }
    public async Task<bool> RegisterAsync(AccountRegisterViewModel registerViewModel)
    {
        try
        {
            await _seedService.InitializeRoles();
            var roleName = "user";

            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            IdentityUser identityUser = registerViewModel;
            await _userManager.CreateAsync(identityUser, registerViewModel.Password);
            
            await _userManager.AddToRoleAsync(identityUser, roleName);

            await _context.SaveChangesAsync();
            /*
            AdressEntity _adressEntity = registerViewModel;
            AccountEntity _userEntity = registerViewModel;

            _context.Adresses.Add(_adressEntity);
            await _context.SaveChangesAsync();

            _userEntity.Adresses = (ICollection<AccountAdressEntity>)_adressEntity;
            _context.Users.Add(_userEntity);
            await _context.SaveChangesAsync();
            */
            return true;
        }
        catch
        {
            return false;        
        }
    }
    public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
    {
        try
        {
            /*var _userEntity = await GetAsync(x => x.Email == loginViewModel.Email);
            if (_userEntity != null)
                return _userEntity.VerifySecurePassword(loginViewModel.Password);
            */
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);
            return result.Succeeded;
        }
        catch { return false; }
    }
    public async Task<bool> LogoutAsync(ClaimsPrincipal user)
    {
        await _signInManager.SignOutAsync();
        return _signInManager.IsSignedIn(user);
    }
}

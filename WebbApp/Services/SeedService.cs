using Microsoft.AspNetCore.Identity;

namespace WebbApp.Services;

public class SeedService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task InitializeRoles()
    {
        if (!await _roleManager.RoleExistsAsync("admin"))
            await _roleManager.CreateAsync(new IdentityRole("admin"));
        
        if (!await _roleManager.RoleExistsAsync("user"))
            await _roleManager.CreateAsync(new IdentityRole("user"));
    }
}

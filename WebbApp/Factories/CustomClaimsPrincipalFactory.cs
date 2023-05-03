using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebbApp.Services;

namespace WebbApp.Factories;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
{
    
    private readonly UserService _userService;
    public CustomClaimsPrincipalFactory(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor, UserService userService) : base(userManager, optionsAccessor)
    {
        _userService = userService;
    }
    /*
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
    {
        var claimsIdentity = await base.GenerateClaimsAsync(user);

        var userIdentity = await _userService.GetAsync(user.Id);
        claimsIdentity.AddClaim(new Claim("DisplayName", $"{userIdentity.FirstName} {userIdentity.LastName})");

        return claimsIdentity;
    }
    */
}

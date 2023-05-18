using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebbApp.Models.Entities;

namespace WebbApp.Models.ViewModels;

public class UpdateRoleViewModel
{
    [Required(ErrorMessage = "You need to enter user id")]
    [Display(Name = "User ID*")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter users new role")]
    public string Role { get; set; } = null!;

    public static implicit operator IdentityRole(UpdateRoleViewModel model)
    {
        return new IdentityRole
        {
            Name = model.Role,
        };
    }

}

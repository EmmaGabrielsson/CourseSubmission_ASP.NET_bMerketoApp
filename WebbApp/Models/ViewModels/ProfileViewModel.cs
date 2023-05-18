using WebbApp.Models.Entities;
using WebbApp.Models.Identities;

namespace WebbApp.Models.ViewModels;

public class ProfileViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Company { get; set; }
    public string? ImageUrl { get; set; }
    public string Email { get; set; } = null!;
    public List<AdressEntity> Adresses { get; set; } = new List<AdressEntity>();

    public static implicit operator ProfileViewModel(AppUser user)
    {
        return new ProfileViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Company = user.CompanyName,
            ImageUrl = user.ImageUrl,
            Email = user.Email!
        };
    }
}

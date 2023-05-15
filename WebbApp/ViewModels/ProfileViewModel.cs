using WebbApp.Models.Entities;
using WebbApp.Models.Identities;

namespace WebbApp.ViewModels;

public class ProfileViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string? PhoneNumber { get; set; } 
    public string? Company { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<AdressEntity> Adresses { get; set; } = new HashSet<AdressEntity>();

}

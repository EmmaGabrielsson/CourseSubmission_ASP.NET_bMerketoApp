﻿using Microsoft.AspNetCore.Identity;
using WebbApp.Models.Entities;

namespace WebbApp.Models.Identities;

public class AppUser : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set;} = null!;
    public string? CompanyName { get; set; }

    public string? ImageUrl { get; set; }
    public ICollection<UserAdressEntity> Adresses { get; set; } = new HashSet<UserAdressEntity>();


}

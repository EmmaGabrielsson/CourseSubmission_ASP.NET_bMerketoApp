﻿using Microsoft.EntityFrameworkCore;
using WebbApp.Models.Identities;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

[PrimaryKey(nameof(UserId), nameof(AdressId))]
public class UserAdressEntity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    [ForeignKey(nameof(Adress))]
    public int AdressId { get; set; }
    public AdressEntity Adress { get; set; } = null!;
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebbApp.Models.Entities;
using WebbApp.Models.Identities;

namespace WebbApp.Contexts;

public class IdentityContext : IdentityDbContext<AppUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }
    public DbSet<AdressEntity> AspNetAdresses { get; set; }
    public DbSet<UserAdressEntity> AspNetUserAdresses { get; set; }
    public DbSet<SubscriberEntity> AspNetNewsletterSubscribers { get; set; }
    public DbSet<ContactFormEntity> AspNetContactForms { get; set; }
}

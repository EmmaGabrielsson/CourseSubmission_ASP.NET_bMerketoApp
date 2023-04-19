using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebbApp.Models.Entities;

namespace WebbApp.Contexts;

public class IdentityContext : IdentityDbContext
{

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }
    public DbSet<AdressEntity> Adresses { get; set; }
    public DbSet<AccountEntity> IdentityUsers { get; set; }

}

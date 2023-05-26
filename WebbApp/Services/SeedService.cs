using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Services;

public class SeedService
{
    #region Constructors & Private Fields
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly DataContext _context;

    public SeedService(RoleManager<IdentityRole> roleManager, DataContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }
    #endregion


    // inserts data in neccesary roletable in database when used
    public async Task InitializeRoles()
    {
        if (!await _roleManager.RoleExistsAsync("admin"))
            await _roleManager.CreateAsync(new IdentityRole("admin"));
        
        if (!await _roleManager.RoleExistsAsync("user"))
            await _roleManager.CreateAsync(new IdentityRole("user"));
    }


    // inserts data in collection table in database when used
    public async Task CreateInitializedDataAsync()
    {
        if (!await _context.Collections.AnyAsync())
        {
            var featuredTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "featured");
            var popularTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "popular");

            await _context.AddAsync(new CollectionEntity
            {
                Title = "best collection",
                ProductIds = await _context.ProductTags.Where(x => x.TagId == featuredTag!.Id).ToListAsync()
            });
            await _context.AddAsync(new CollectionEntity
            {
                Title = "top selling products in this week",
                ProductIds = await _context.ProductTags.Where(x => x.TagId == popularTag!.Id).ToListAsync()
            });
            await _context.SaveChangesAsync();
        }
    }

}

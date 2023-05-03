using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Services;

public class ShowcaseService
{
    private readonly DataContext _context;

    public ShowcaseService(DataContext context)
    {
        _context = context;
    }
    public async Task CreateInitializedShowcaseAsync()
    {
        if (!await _context.Showcases.AnyAsync())
        {
            await _context.AddAsync(new ShowcaseEntity 
            {
                Ingress = "WELCOME TO BMERKETO SHOP",
                Title = "Exclusive Chair gold Collection.",
                ImageUrl = "images/placeholders/625x647.svg",
                LinkText = "SHOP NOW",
                LinkUrl = "/products"
            });
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ShowcaseEntity> GetLatestShowcaseAsync()
    {
        await CreateInitializedShowcaseAsync();
        var showcase = _context.Showcases.OrderByDescending(x => x.CreatedDate).LastOrDefault()!;

        if  (showcase != null)
            return showcase;

        return null!;
    }
}

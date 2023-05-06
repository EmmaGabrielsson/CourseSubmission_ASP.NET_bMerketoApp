using Microsoft.EntityFrameworkCore;
using WebbApp.Contexts;
using WebbApp.Models.Entities;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class SubscribeService
{
    private readonly IdentityContext _context;

    public SubscribeService(IdentityContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistAsync(EmailViewModel model)
    {
        var _subscriber = await _context.AspNetNewsletterSubscribers.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (_subscriber != null)
            return true;

        return false;
    }
    public async Task<bool> RegisterForSubscribeAsync(EmailViewModel model)
    {
        try
        {
            SubscriberEntity subscriber = new()
            {
                Email = model.Email
            };
            _context.AspNetNewsletterSubscribers.Add(subscriber);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }

    }
}

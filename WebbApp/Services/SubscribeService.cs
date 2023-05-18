using Microsoft.EntityFrameworkCore;
using WebbApp.Contexts;
using WebbApp.Models.Entities;
using WebbApp.Models.ViewModels;

namespace WebbApp.Services;

public class SubscribeService
{
    private readonly IdentityContext _context;

    public SubscribeService(IdentityContext context)
    {
        _context = context;
    }

    public async Task<bool> ExsistAsync(EmailViewModel model)
    {
        try
        {
            var _subscriber = await _context.AspNetNewsletterSubscribers.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (_subscriber != null)
                return true;
        } catch { return false; }
        return false;
    }
    public async Task<bool> RegisterForSubscribeAsync(EmailViewModel model)
    {
        try {
            SubscriberEntity subscriber = new()
            {
                Email = model.Email
            };
            _context.AspNetNewsletterSubscribers.Add(subscriber);
            await _context.SaveChangesAsync();
            return true;
        } catch { return false; }
    }
}

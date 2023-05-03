using Microsoft.EntityFrameworkCore;
using WebbApp.Contexts;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class SubscribeService
{
    private readonly IdentityContext _context;

    public SubscribeService(IdentityContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterForSubscribeAsync(EmailViewModel model)
    {
        try
        {
            
        var _subscriber = await _context.AspNetNewsletterSubscribers.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (_subscriber != null)
                return false;

            else
            {
                _context.AspNetNewsletterSubscribers.Add(_subscriber!);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch
        {
            return false;
        }

    }
}

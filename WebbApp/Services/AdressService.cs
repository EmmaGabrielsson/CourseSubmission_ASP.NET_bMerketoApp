using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebbApp.Contexts;
using WebbApp.Models.Entities;
using WebbApp.Models.Identities;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class AdressService
{
    private readonly IdentityContext _context;

    public AdressService(IdentityContext context)
    {
        _context = context;
    }

    public async Task<List<AdressEntity>> GetUserAdressAsync(AppUser user)
    {
        var _adresses = await _context.AspNetUserAdresses.Where(x => x.UserId == user.Id).ToListAsync();

        var _foundAdressesList = new List<AdressEntity>();
        foreach (var adress in _adresses)
        {
            var _foundAdress = await _context.AspNetAdresses.FirstOrDefaultAsync(x => x.Id == adress.AdressId);
            
            if (_foundAdress != null)
                _foundAdressesList.Add(_foundAdress);
        }

        return _foundAdressesList;
    }

    public async Task<bool> AddAdressAsync(AppUser user, AdressEntity adress)
    {
        UserAdressEntity newUserAdress = new()
        {
            UserId = user.Id,
            AdressId = adress.Id
        };

        if (newUserAdress != null)
        {
            await _context.AspNetUserAdresses.AddAsync(newUserAdress);
            await _context.SaveChangesAsync();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    public async Task<AdressEntity> GetOrCreateAsync(AccountRegisterViewModel model)
    {
        var adress = await _context.AspNetAdresses.FirstOrDefaultAsync(x => x.StreetName == model.StreetName && x.PostalCode == model.PostalCode && x.City == model.City);
        if (adress != null)
            return adress!;

        await _context.AspNetAdresses.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<AdressEntity> GetAsync(Expression<Func<AdressEntity, bool>> predicate)
    {
        var _userEntity = await _context.AspNetAdresses.FirstOrDefaultAsync(predicate);
        return _userEntity!;
    }

}

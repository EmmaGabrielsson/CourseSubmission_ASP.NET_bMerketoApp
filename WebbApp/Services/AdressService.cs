using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebbApp.Contexts;
using WebbApp.Models.Entities;
using WebbApp.Models.Identities;

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
        try
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
        catch { return null!; }
    }
    public async Task<bool> AddAdressAsync(AppUser user, AdressEntity adress)
    {
        try
        {
            var findUser = await _context.Users.FindAsync(user.Id);
            if(findUser != null)
            {
                var findAdress = await _context.AspNetAdresses.FirstOrDefaultAsync(x => x.StreetName == adress.StreetName & x.PostalCode == adress.PostalCode && x.City == adress.City );
                if (findAdress != null){
                    UserAdressEntity newUserAdress = new()
                    {
                        UserId = findUser.Id,
                        AdressId = findAdress.Id
                    };
                    await _context.AspNetUserAdresses.AddAsync(newUserAdress);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

        }catch { return false; }

        return false;
    }
    public async Task<AdressEntity> GetOrCreateAsync(AdressEntity entity)
    {
        try
        {
            var adress = await _context.AspNetAdresses.FirstOrDefaultAsync(x => x.StreetName == entity.StreetName && x.PostalCode == entity.PostalCode && x.City == entity.City);
            if (adress != null)
                return adress!;

            await _context.AspNetAdresses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        } catch { return null!; }
    }
    public async Task<AdressEntity> GetAsync(Expression<Func<AdressEntity, bool>> predicate)
    {
        try
        {
            var _userEntity = await _context.AspNetAdresses.FirstOrDefaultAsync(predicate);
            return _userEntity!;
        } catch { return null!; }
    }

}

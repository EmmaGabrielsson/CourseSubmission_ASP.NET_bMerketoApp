using Microsoft.AspNetCore.Identity;
using WebbApp.Models.Entities;
using WebbApp.Models.Identities;
using WebbApp.Repositories;

namespace WebbApp.Services;

public class AdressService
{
    #region Constructors & Private Fields
        private readonly AdressRepo _adressRepo;
        private readonly UserAdressRepo _userAdressRepo;
        private readonly UserManager<AppUser> _userManager;
        public AdressService(AdressRepo adressRepo, UserAdressRepo userAdressRepo, UserManager<AppUser> userManager)
        {
            _adressRepo = adressRepo;
            _userAdressRepo = userAdressRepo;
            _userManager = userManager;
        }
    #endregion

    public async Task<List<AdressEntity>> GetUserAdressAsync(AppUser user)
    {
        try
        {
            var _adresses = await _userAdressRepo.GetAllIdentityAsync(x => x.UserId == user.Id);
            if (!_adresses.Any())
                return null!;

            var _foundAdressesList = new List<AdressEntity>();
            foreach (var adress in _adresses)
            {
                var _foundAdress = await _adressRepo.GetIdentityAsync(x => x.Id == adress.AdressId);
            
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
            var findUser = await _userManager.FindByIdAsync(user.Id);
            if(findUser != null)
            {
                var findAdress = await _adressRepo.GetIdentityAsync(x => x.StreetName == adress.StreetName & x.PostalCode == adress.PostalCode && x.City == adress.City );
                if (findAdress != null){
                    UserAdressEntity newUserAdress = new()
                    {
                        UserId = findUser.Id,
                        AdressId = findAdress.Id
                    };
                    
                    if(await _userAdressRepo.AddIdentityAsync(newUserAdress) != null)
                        return true;
                }
            }
            return false;

        }catch { return false; }
    }
    public async Task<AdressEntity> GetOrCreateAsync(AdressEntity entity)
    {
        try
        {
            var adress = await _adressRepo.GetIdentityAsync(x => x.Id == entity.Id);
            if (adress != null)
                return adress;

            await _adressRepo.AddIdentityAsync(entity);
            return entity;
        } catch { return null!; }
    }

}

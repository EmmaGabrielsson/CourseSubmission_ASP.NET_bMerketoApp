using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebbApp.Contexts;
using WebbApp.Models.Entities;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> UserExist(Expression<Func<AccountEntity, bool>> predicate)
    {
        if (!await _context.Users.AnyAsync(predicate))
            return true;

        return false;
    }
    public async Task<AccountEntity> GetAsync(Expression<Func<AccountEntity, bool>> predicate)
    {
        var _userEntity = await _context.Users.FirstOrDefaultAsync(predicate);
        return _userEntity!;
    }
    public async Task<bool> RegisterAsync(AccountRegisterViewModel registerViewModel)
    {
        try
        {
            AdressEntity _adressEntity = registerViewModel;
            AccountEntity _userEntity = registerViewModel;

            _context.Adresses.Add(_adressEntity);
            await _context.SaveChangesAsync();

            _userEntity.AdressId = _adressEntity.Id;
            _context.Users.Add(_userEntity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;        
        }
    }
    public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
    {
        var _userEntity = await GetAsync(x => x.Email == loginViewModel.Email);
        if (_userEntity != null)
            return _userEntity.VerifySecurePassword(loginViewModel.Password);

        return false;
    }
}

using System.ComponentModel.DataAnnotations;
using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class AccountRegisterViewModel
{
    [Required(ErrorMessage = "You need to enter your first name ↑")]
    [Display(Name = "First Name *")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter your last name ↑")]
    [Display(Name = "Last Name *")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter your email adress ↑")]
    [Display(Name = "Email Adress *")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z]{2,}$", ErrorMessage = "You need to enter a valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter a password ↑")]
    [Display(Name = "Password *")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "You need to enter a valid password with atleast 8 characters and both uppercase and lowercase letters.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "You need to confirm password ↑")]
    [Display(Name = "Confirm Password *")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter streetname ↑")]
    [Display(Name = "Street Name *")]
    public string StreetName { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter postalcode ↑")]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter city ↑")]
    [Display(Name = "City")]
    public string City { get; set; } = null!;

    
    public static implicit operator AdressEntity(AccountRegisterViewModel registerViewModel)
    {
        return new AdressEntity
        {
            StreetName = registerViewModel.StreetName,
            PostalCode = registerViewModel.PostalCode,
            City = registerViewModel.City
        };
    }
    

    public static implicit operator AccountEntity(AccountRegisterViewModel registerViewModel)
    {
        var _userEntity = new AccountEntity
        {
            Email = registerViewModel.Email,
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
        };
        _userEntity.GenerateSecurePassword(registerViewModel.Password);
        return _userEntity;
    }
}

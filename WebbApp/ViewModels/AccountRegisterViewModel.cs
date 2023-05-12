using System.ComponentModel.DataAnnotations;
using WebbApp.Models.Entities;
using WebbApp.Models.Identities;

namespace WebbApp.ViewModels;

public class AccountRegisterViewModel
{
    [Required(ErrorMessage = "You need to enter your first name ↑")]
    [Display(Name = "First Name*")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter your last name ↑")]
    [Display(Name = "Last Name*")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter your email adress ↑")]
    [Display(Name = "E-mail*")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You need to enter a valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter a password ↑")]
    [Display(Name = "Password*")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "You need to enter a valid password with atleast 8 characters and both uppercase and lowercase letters.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "You need to confirm password ↑")]
    [Display(Name = "Confirm Password*")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter streetname ↑")]
    [Display(Name = "Street Name*")]
    public string StreetName { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter postalcode ↑")]
    [Display(Name = "Postal Code*")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter city ↑")]
    [Display(Name = "City*")]
    public string City { get; set; } = null!;

    [Display(Name = "Mobile (optional)")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Company (optional)")]
    public string? Company { get; set; }

    [Display(Name = "Upload Profile Image (optional)")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "I have read and accepts the terms and agreements")]
    [Required(ErrorMessage = "You need to accept terms and agreements")]
    public bool AcceptTermsAndAgreements { get; set; } = false;

    public static implicit operator AdressEntity(AccountRegisterViewModel registerViewModel)
    {
        return new AdressEntity
        {
            StreetName = registerViewModel.StreetName,
            PostalCode = registerViewModel.PostalCode,
            City = registerViewModel.City
        };
    }


    public static implicit operator AppUser(AccountRegisterViewModel registerViewModel)
    {
        var _userEntity = new AppUser
        {
            UserName = registerViewModel.Email,
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            Email = registerViewModel.Email,
            PhoneNumber = registerViewModel.PhoneNumber,
            CompanyName = registerViewModel.Company
        };
        return _userEntity;
    }
}

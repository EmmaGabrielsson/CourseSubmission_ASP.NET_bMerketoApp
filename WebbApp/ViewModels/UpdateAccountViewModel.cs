using System.ComponentModel.DataAnnotations;
using WebbApp.Models.Entities;
using WebbApp.Models.Identities;

namespace WebbApp.ViewModels;

public class UpdateAccountViewModel
{    public string Email { get; set; } = null!;

    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Display(Name = "New Password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "You need to enter a valid password with atleast 8 characters and both uppercase and lowercase letters.")]
    public string? NewPassword { get; set; }

    [Display(Name = "Confirm New Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Password doesn't match")]
    public string? ConfirmPassword { get; set; }

    [Display(Name = "Street Name")]
    public string? StreetName { get; set; }

    [Display(Name = "Postal Code")]
    [DataType(DataType.PostalCode)]
    public string? PostalCode { get; set; }

    [Display(Name = "City")]
    public string? City { get; set; }

    [Display(Name = "Mobile")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Company")]
    public string? Company { get; set; }

    [Display(Name = "Upload Profile Image")]
    public IFormFile? ImageFile { get; set; }


    public static implicit operator AdressEntity(UpdateAccountViewModel registerViewModel)
    {
        return new AdressEntity
        {
            StreetName = registerViewModel.StreetName,
            PostalCode = registerViewModel.PostalCode,
            City = registerViewModel.City
        };
    }


    public static implicit operator AppUser(UpdateAccountViewModel registerViewModel)
    {
        var _userEntity = new AppUser
        {
            UserName = registerViewModel.Email,
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            PhoneNumber = registerViewModel.PhoneNumber,
            CompanyName = registerViewModel.Company
        };
        return _userEntity;
    }

}

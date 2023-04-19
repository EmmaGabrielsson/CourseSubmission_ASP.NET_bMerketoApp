using System.ComponentModel.DataAnnotations;

namespace WebbApp.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "You need to enter your email adress")]
    [Display(Name = "Email Adress")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You need to enter your password")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;

namespace WebbApp.ViewModels;

public class EmailViewModel
{
    [Required(ErrorMessage = "You need to enter your email adress")]
    [Display(Name = "E-mail*")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You need to enter a valid email")]
    public string Email { get; set; } = null!;
}

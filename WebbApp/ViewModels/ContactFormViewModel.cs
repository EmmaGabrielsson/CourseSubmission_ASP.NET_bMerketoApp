using System.ComponentModel.DataAnnotations;

namespace WebbApp.ViewModels;

public class ContactFormViewModel
{
    [Required(ErrorMessage = "Your name is required")]
    [Display(Name = "Your Name*")]
    public string Name { get; set; } = null!;


    [Required(ErrorMessage = "Your email is required")]
    [Display(Name = "Your Email*")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You need to enter a valid email")]
    public string Email { get; set; } = null!;


    [Display(Name = "Phone Number (optional)")]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }


    [Display(Name = "Company (optional)")]
    public string? Company { get; set; }


    [Display(Name = "Message*")]
    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; } = null!;


    [Display(Name = "Save my name, email in the this browser for the next time I comment.")]
    public bool RememberMe { get; set; } = true;
}

namespace WebbApp.Models.Entities;

public class ContactFormEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? CompanyName { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

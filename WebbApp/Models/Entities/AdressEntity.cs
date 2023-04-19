namespace WebbApp.Models.Entities;

public class AdressEntity
{
    public int Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? Country { get; set; }

    public ICollection<AccountAdressEntity> Accounts { get; set; } = new HashSet<AccountAdressEntity>();
}

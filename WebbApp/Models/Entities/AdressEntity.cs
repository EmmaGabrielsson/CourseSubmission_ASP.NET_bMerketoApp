using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

public class AdressEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string StreetName { get; set; } = null!;

    [Column(TypeName = "char(5)")]
    public string PostalCode { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string City { get; set; } = null!;

    public ICollection<UserAdressEntity> Users { get; set; } = new HashSet<UserAdressEntity>();
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

[PrimaryKey(nameof(UserId), nameof(AdressId))]
public class AccountAdressEntity
{
    [ForeignKey(nameof(Account))]
    public string UserId { get; set; } = null!;
    public AccountEntity Account { get; set; } = null!;

    [ForeignKey(nameof(Adress))]
    public int AdressId { get; set; }
    public AdressEntity Adress { get; set; } = null!;
}

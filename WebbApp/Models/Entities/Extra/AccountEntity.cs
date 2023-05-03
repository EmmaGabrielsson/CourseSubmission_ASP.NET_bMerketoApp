using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace WebbApp.Models.Entities.Extra;

public class AccountEntity : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public byte[] Password { get; private set; } = null!;
    public byte[] SecurityKey { get; private set; } = null!;
    public IFormFile? ProfileImage { get; set; }
    public ICollection<UserAdressEntity> Adresses { get; set; } = new HashSet<UserAdressEntity>();
    public ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();


    public void GenerateSecurePassword(string password)
    {
        using var hmac = new HMACSHA512();
        SecurityKey = hmac.Key;
        Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool VerifySecurePassword(string password)
    {
        using var hmac = new HMACSHA512(SecurityKey);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < hash.Length; i++)
        {
            if (hash[i] != Password[i])
                return false;
        }

        return true;
    }
}

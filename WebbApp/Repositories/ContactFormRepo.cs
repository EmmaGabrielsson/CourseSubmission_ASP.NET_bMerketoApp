using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories;

public class ContactFormRepo : Repo<ContactFormEntity>
{
    public ContactFormRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
    {
    }
}

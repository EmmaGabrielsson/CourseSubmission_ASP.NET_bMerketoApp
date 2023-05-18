using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories
{
    public class UserAdressRepo : Repo<UserAdressEntity>
    {
        public UserAdressRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
        {
        }
    }
}

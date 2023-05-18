using WebbApp.Contexts;
using WebbApp.Models.Identities;

namespace WebbApp.Repositories
{
    public class UserRepo : Repo<AppUser>
    {
        public UserRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
        {
        }
    }
}

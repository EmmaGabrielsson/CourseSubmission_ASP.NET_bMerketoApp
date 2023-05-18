using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories
{
    public class AdressRepo : Repo<AdressEntity>
    {
        public AdressRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
        {
        }
    }
}

using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories
{
    public class CollectionRepo : Repo<CollectionEntity>
    {
        public CollectionRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
        {
        }
    }
}

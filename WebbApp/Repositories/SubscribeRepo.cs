using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories
{
    public class SubscribeRepo : Repo<SubscriberEntity>
    {
        public SubscribeRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
        {
        }
    }
}

using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories
{
    public class ProductTagRepo : Repo<ProductTagEntity>
    {
        public ProductTagRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
        {
        }
    }
}

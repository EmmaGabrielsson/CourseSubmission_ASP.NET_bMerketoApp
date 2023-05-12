using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories;

public class CategoryRepo : Repo<CategoryEntity>
{
    public CategoryRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
    {
    }
}

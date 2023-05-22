using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Repositories;

public class OrderRepo : Repo<OrderEntity>
{
    public OrderRepo(IdentityContext identityContext, DataContext dataContext) : base(identityContext, dataContext)
    {
    }
}

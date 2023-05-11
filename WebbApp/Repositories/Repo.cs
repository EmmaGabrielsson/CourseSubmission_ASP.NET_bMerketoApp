using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebbApp.Contexts;

namespace WebbApp.Repositories
{
    public abstract class Repo<Entity> where Entity : class
    {

        #region Private fields & Constructors
        private readonly IdentityContext _identityContext;
        private readonly DataContext _dataContext;
        protected Repo(IdentityContext identityContext, DataContext dataContext)
        {
            _identityContext = identityContext;
            _dataContext = dataContext;
        }
        #endregion

        #region Tasks for DataContext
        public virtual async Task<Entity> AddDataAsync(Entity entity)
        {
            await _dataContext.Set<Entity>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<Entity> GetDataAsync(Expression<Func<Entity, bool>> expression)
        {
            var entity = await _dataContext.Set<Entity>().FirstOrDefaultAsync(expression);
            return entity!;
        }
        public virtual async Task<IEnumerable<Entity>> GetAllDataAsync()
        {
            return await _dataContext.Set<Entity>().ToListAsync();
        }
        public virtual async Task<IEnumerable<Entity>> GetAllDataAsync(Expression<Func<Entity, bool>> expression)
        {
            return await _dataContext.Set<Entity>().Where(expression).ToListAsync();
        }
        public virtual async Task<Entity> UpdateDataAsync(Entity entity)
        {
            _dataContext.Set<Entity>().Update(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<Entity> RemoveDataAsync(Entity entity)
        {
            _dataContext.Set<Entity>().Remove(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Tasks for IdentityContext
        public virtual async Task<Entity> AddIdentityAsync(Entity entity)
        {
            await _identityContext.Set<Entity>().AddAsync(entity);
            await _identityContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<Entity> GetIdentityAsync(Expression<Func<Entity, bool>> expression)
        {
            var entity = await _identityContext.Set<Entity>().FirstOrDefaultAsync(expression);
            return entity!;
        }
        public virtual async Task<IEnumerable<Entity>> GetAllIdentityAsync()
        {
            return await _identityContext.Set<Entity>().ToListAsync();
        }
        public virtual async Task<IEnumerable<Entity>> GetAllIdentityAsync(Expression<Func<Entity, bool>> expression)
        {
            return await _identityContext.Set<Entity>().Where(expression).ToListAsync();         
        }
        public virtual async Task<Entity> UpdateIdentityAsync(Entity entity)
        {
            _identityContext.Set<Entity>().Update(entity);
            await _identityContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<Entity> RemoveIdentityAsync(Entity entity)
        {
            _identityContext.Set<Entity>().Remove(entity);
            await _identityContext.SaveChangesAsync();
            return entity;
        }

        #endregion

    }
}

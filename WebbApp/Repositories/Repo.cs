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
            try {
                await _dataContext.Set<Entity>().AddAsync(entity);
                await _dataContext.SaveChangesAsync();
                return entity;
            }
            catch { return null!; }
        }
        public virtual async Task<Entity> GetDataAsync(Expression<Func<Entity, bool>> expression)
        {
            try { 
                var entity = await _dataContext.Set<Entity>().FirstOrDefaultAsync(expression);
                return entity!;
            }
            catch { return null!; }
        }
        public virtual async Task<IEnumerable<Entity>> GetAllDataAsync()
        {
            try { 
                return await _dataContext.Set<Entity>().ToListAsync();
            }
            catch { return null!; }

        }
        public virtual async Task<IEnumerable<Entity>> GetAllDataAsync(Expression<Func<Entity, bool>> expression)
        {
            try { 
                return await _dataContext.Set<Entity>().Where(expression).ToListAsync();
            }
            catch { return null!; }
        }
        public virtual async Task<Entity> UpdateDataAsync(Entity entity)
        {
            try { 
                _dataContext.Set<Entity>().Update(entity);
                await _dataContext.SaveChangesAsync();
                return entity;
            }
            catch { return null!; }
        }
        public virtual async Task<bool> RemoveDataAsync(Entity entity)
        {
            try {
                _dataContext.Set<Entity>().Remove(entity);
                await _dataContext.SaveChangesAsync();
                return true;
            } catch { return false; }
        }

        #endregion

        #region Tasks for IdentityContext
        public virtual async Task<Entity> AddIdentityAsync(Entity entity)
        {
            try { 
                await _identityContext.Set<Entity>().AddAsync(entity);
                await _identityContext.SaveChangesAsync();
                return entity;
            }
            catch { return null!; }
        }
        public virtual async Task<Entity> GetIdentityAsync(Expression<Func<Entity, bool>> expression)
        {
            try { 
                var entity = await _identityContext.Set<Entity>().FirstOrDefaultAsync(expression);
                return entity!;
            }
            catch { return null!; }
        }
        public virtual async Task<IEnumerable<Entity>> GetAllIdentityAsync()
        {
            try {
                return await _identityContext.Set<Entity>().ToListAsync();
            }
            catch { return null!; }
        }
        public virtual async Task<IEnumerable<Entity>> GetAllIdentityAsync(Expression<Func<Entity, bool>> expression)
        {
            try {
                return await _identityContext.Set<Entity>().Where(expression).ToListAsync();
            }
            catch { return null!; }         
        }
        public virtual async Task<Entity> UpdateIdentityAsync(Entity entity)
        {
            try { 
                _identityContext.Set<Entity>().Update(entity);
                await _identityContext.SaveChangesAsync();
                return entity;
            }
            catch { return null!; }
        }
        public virtual async Task<bool> RemoveIdentityAsync(Entity entity)
        {
            try
            {
                _identityContext.Set<Entity>().Remove(entity);
                await _identityContext.SaveChangesAsync();
                return true;

            } catch { return false; }
        }

        #endregion

    }
}

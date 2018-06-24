using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EXCSLA.Models;

namespace EXCSLA.Services.DataServices.EFDataServices
{
    public class EFDataService<TContext> : EFReadOnlyDataService<TContext>, IDataService<TContext>
        where TContext : DbContext
    {
        public EFDataService(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void CreateChild<TEntityChild>(TEntityChild entityChild) where TEntityChild : class, IEntityChild
        {
            _context.Set<TEntityChild>().Add(entityChild);
        }

        public virtual void UpdateChild<TEntityChild>(TEntityChild entityChild) where TEntityChild : class, IEntityChild
        {
            _context.Set<TEntityChild>().Attach(entityChild);
            _context.Entry(entityChild).State = EntityState.Modified;
        }

        public virtual void DeleteChild<TEntityChild>(TEntityChild entityChild) where TEntityChild : class, IEntityChild
        {
            var dbSet = _context.Set<TEntityChild>();
            if (_context.Entry(entityChild).State == EntityState.Detached)
            {
                dbSet.Attach(entityChild);
            }
            dbSet.Remove(entityChild);
        }

        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }
}
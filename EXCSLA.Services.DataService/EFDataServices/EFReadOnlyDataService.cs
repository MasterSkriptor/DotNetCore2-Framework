using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EXCSLA.Models;

namespace EXCSLA.Services.DataServices.EFDataServices
{
    public class EFReadOnlyDataService<TContext> : IReadOnlyDataService
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public EFReadOnlyDataService(TContext context)
        {
            _context = context;
        }
        
        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        protected virtual IQueryable<TEntityChild> GetQueryableChildren<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntityChild : class, IEntityChild
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntityChild> query = _context.Set<TEntityChild>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual List<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<List<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual List<TEntityChild> GetAllChildren<TEntityChild>(object parentID, 
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null, 
            string includeProperties = null, 
            int? skip = null, 
            int? take = null) where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(c => c.ParentId == parentID, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<List<TEntityChild>> GetAllChildrenAsync<TEntityChild>(object parentID, 
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null, 
            string includeProperties = null, 
            int? skip = null, 
            int? take = null) where TEntityChild : class, IEntityChild
        {
            return await GetQueryableChildren<TEntityChild>(c => c.ParentId == parentID, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual List<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<List<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual List<TEntityChild> GetChild<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public async virtual Task<List<TEntityChild>> GetChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntityChild : class, IEntityChild
        {
            return await GetQueryableChildren<TEntityChild>(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, null, includeProperties).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual TEntityChild GetOneChild<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            string includeProperties = "") where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter, null, includeProperties).SingleOrDefault();
        }

        public async virtual Task<TEntityChild> GetOneChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            string includeProperties = "") where TEntityChild : class, IEntityChild
        {
            return await GetQueryableChildren<TEntityChild>(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
        where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual TEntityChild GetFirstChild<TEntityChild>(
        Expression<Func<TEntityChild, bool>> filter = null,
        Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
        string includeProperties = "")
        where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntityChild> GetFirstChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null)
            where TEntityChild : class, IEntityChild
        {
            return await GetQueryableChildren<TEntityChild>(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual TEntity GetLast<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
        where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, includeProperties).LastOrDefault();
        }

        public virtual async Task<TEntity> GetLastAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties).LastOrDefaultAsync();
        }

        public virtual TEntityChild GetLastChild<TEntityChild>(
        Expression<Func<TEntityChild, bool>> filter = null,
        Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
        string includeProperties = "")
        where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter, orderBy, includeProperties).LastOrDefault();
        }

        public virtual async Task<TEntityChild> GetLastChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null)
            where TEntityChild : class, IEntityChild
        {
            return await GetQueryableChildren<TEntityChild>(filter, orderBy, includeProperties).LastOrDefaultAsync();
        }

        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).CountAsync();
        }

        public virtual int GetChildCount<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter).Count();
        }

        public virtual Task<int> GetChildCountAsync<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).AnyAsync();
        }

        public virtual bool GetChildExists<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter).Any();
        }

        public virtual Task<bool> GetChildExistsAsync<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild
        {
            return GetQueryableChildren<TEntityChild>(filter).AnyAsync();
        }
    }
}
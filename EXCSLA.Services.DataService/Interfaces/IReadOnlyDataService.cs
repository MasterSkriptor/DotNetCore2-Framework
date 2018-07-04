using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EXCSLA.Models;

namespace EXCSLA.Services.DataServices
{
    public interface IReadOnlyDataService
    {
        List<TEntity> GetAll<TEntity>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
        where TEntity : class, IEntity;

        Task<List<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity;

        List<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity;

        Task<List<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity;

        TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class, IEntity;

        Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class, IEntity;

        TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity;

        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity;

        TEntity GetLast<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity;

        Task<TEntity> GetLastAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity;

        TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity;

        Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        List<TEntityChild> GetAllChildren<TEntityChild>(object parentID, 
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null, 
            string includeProperties = null, 
            int? skip = null, 
            int? take = null) where TEntityChild : class, IEntityChild;
        
        Task<List<TEntityChild>> GetAllChildrenAsync<TEntityChild>(object parentID, 
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null, 
            string includeProperties = null, 
            int? skip = null, 
            int? take = null) where TEntityChild : class, IEntityChild;
        
        List<TEntityChild> GetChild<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntityChild : class, IEntityChild;
        
        Task<List<TEntityChild>> GetChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntityChild : class, IEntityChild;
        
        TEntityChild GetOneChild<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            string includeProperties = "") where TEntityChild : class, IEntityChild;

        Task<TEntityChild> GetOneChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            string includeProperties = "") where TEntityChild : class, IEntityChild;
        
        TEntityChild GetFirstChild<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = "")
            where TEntityChild : class, IEntityChild;
        
        Task<TEntityChild> GetFirstChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null)
            where TEntityChild : class, IEntityChild;
        
        TEntityChild GetLastChild<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = "")
            where TEntityChild : class, IEntityChild;

        Task<TEntityChild> GetLastChildAsync<TEntityChild>(
            Expression<Func<TEntityChild, bool>> filter = null,
            Func<IQueryable<TEntityChild>, IOrderedQueryable<TEntityChild>> orderBy = null,
            string includeProperties = null)
            where TEntityChild : class, IEntityChild;

        int GetChildCount<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild;

        Task<int> GetChildCountAsync<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild;

        bool GetChildExists<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild;

        Task<bool> GetChildExistsAsync<TEntityChild>(Expression<Func<TEntityChild, bool>> filter = null)
            where TEntityChild : class, IEntityChild;
    }
}
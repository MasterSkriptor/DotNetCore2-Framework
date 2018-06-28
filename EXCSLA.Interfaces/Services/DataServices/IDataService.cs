using System.Collections.Generic;
using System.Threading.Tasks;
using EXCSLA.Models;
using Microsoft.EntityFrameworkCore;

namespace EXCSLA.Services.DataServices
{
    public interface IDataService : IReadOnlyDataService
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
        where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void CreateChild<TEntityChild>(TEntityChild entityChild)
            where TEntityChild : class, IEntityChild;

        void UpdateChild<TEntityChild>(TEntityChild entityChild) 
            where TEntityChild : class, IEntityChild;
        
        void DeleteChild<TEntityChild>(TEntityChild entityChild) 
            where TEntityChild : class, IEntityChild;

        void Save();

        Task SaveAsync();
    }

    public interface IDataService<TContext> : IDataService where TContext : DbContext
    {

    }
}
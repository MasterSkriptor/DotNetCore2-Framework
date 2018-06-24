using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EXCSLA.Models;
using Microsoft.EntityFrameworkCore;

namespace EXCSLA.Services.DataServices.WebObjectDataServices
{
    public interface IWebObjectDataService : IDataService, IReadOnlyDataService
    {
        Task<IEnumerable<TEntity>> GetActiveAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntity : class, IWebObject;

            Task FlushDeletedAsync<TEntity>()
                where TEntity : class, IWebObject;
    }

    public interface IWebObjectDataService<TContext> : IWebObjectDataService where TContext : DbContext
    {
        
    }
}
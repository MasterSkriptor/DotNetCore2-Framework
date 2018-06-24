using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EXCSLA.Models;
using EXCSLA.Services.DataServices.EFDataServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EXCSLA.Services.DataServices.WebObjectDataServices
{
    public class WebObjectDataService<TContext> : EFDataService<TContext>, IWebObjectDataService<TContext>
        where TContext : DbContext
    {

        public WebObjectDataService(TContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TEntity>> GetActiveAsync<TEntity>(Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IWebObject
        {
            var query = base.GetQueryable<TEntity>(null, orderBy, includeProperties);

            query = query.Where(m => m.ShowOnSite == true && m.MarkedForDeletion == false);

            if(skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if(take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public override void Create<TEntity>(TEntity entity, string createdBy = null)
        {
            if(entity is IWebObject)
            {
                ((IWebObject)entity).ShowOnSite = true;
                ((IWebObject)entity).MarkedForDeletion = false;
            }

            base.Create<TEntity>(entity, createdBy);
        }

        public override void Delete<TEntity>(object id)
        {
            TEntity entity = base._context.Set<TEntity>().Find(id);
            if(entity is IWebObject)
            {
                ((IWebObject)entity).ShowOnSite = false;
                ((IWebObject)entity).MarkedForDeletion = true;

                base.Update(entity);
            }
            else
            {
                base.Delete<TEntity>(id);
            }
            
        }

        public override void Delete<TEntity>(TEntity entity)
        {
            if(entity is IWebObject)
            {
                ((IWebObject)entity).ShowOnSite = false;
                ((IWebObject)entity).MarkedForDeletion = true;

                base.Update(entity);
            }
            else
            {
                base.Delete<TEntity>(entity);
            }
        }

        public async Task FlushDeletedAsync<TEntity>()
            where TEntity : class, IWebObject
        {
            IEnumerable<TEntity> items = await base.GetQueryable<TEntity>().Where(m => m.MarkedForDeletion == true).ToListAsync();

            foreach(var item in items)
            {
                base.Delete(item);
            }
        }

        public void FlushDeleted<TEntity>()
            where TEntity : class, IWebObject
        {
            List<TEntity> items = base.GetQueryable<TEntity>().Where(m => m.MarkedForDeletion == true).ToList();

            foreach(var item in items)
            {
                base.Delete(item);
            }
        }

    }
}
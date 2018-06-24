using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EXCSLA.Models;
using EXCSLA.Services.DataServices;

namespace EXCSLA.Controllers
{
    [Authorize]
    public class EntityBaseController<TContext, TEntity> : Controller 
        where TContext : DbContext 
        where TEntity : class, IEntity, new()
    {
        private readonly IDataService<TContext> _dataService;

        public EntityBaseController(IDataService<TContext> dataService)
        {
            _dataService = dataService;
        }

        [AllowAnonymous]
        public virtual async Task<IActionResult> Index()
        {
            var model = await _dataService.GetAllAsync<TEntity>();

            if(model == null) return NotFound();

            return View(model);
        }

        [AllowAnonymous]
        public virtual async Task<IActionResult> Item(int? id)
        {
            if(id == null) return NotFound();

            var model = await _dataService.GetByIdAsync<TEntity>(id);

            if(model == null) return NotFound();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> Admin()
        {
            var model = await _dataService.GetAllAsync<TEntity>();

            if(model == null)
                return NotFound();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public virtual IActionResult Create()
        {
            TEntity model = new TEntity();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TEntity model)
        {
            _dataService.Create<TEntity>(model);
            await _dataService.SaveAsync();

            return RedirectToAction("Admin");
        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return NotFound();

            var model = await _dataService.GetByIdAsync<TEntity>(id);

            if(model == null)
                return NotFound();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int? id, TEntity model)
        {
            if(id != (int)model.Id)
                return NotFound();

            _dataService.Update(model);
            await _dataService.SaveAsync();

            return RedirectToAction("Admin");

        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> Delete(int? id)
        {
            if( id == null)
                return NotFound();

            var model = await _dataService.GetByIdAsync<TEntity>(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(int? id, TEntity model)
        {
            if( id == null)
                return NotFound();

            if( id != (int)model.Id)
                return NotFound();
            
            _dataService.Delete<TEntity>(await _dataService.GetByIdAsync<TEntity>(model.Id));
            await _dataService.SaveAsync();

            return RedirectToAction("Admin");
        }
    }
}
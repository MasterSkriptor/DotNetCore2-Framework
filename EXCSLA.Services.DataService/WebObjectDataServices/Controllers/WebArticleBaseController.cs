using System.Threading.Tasks;
using EXCSLA.Models;
using EXCSLA.Services.DataServices.WebObjectDataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EXCSLA.Controllers
{
    public class WebArticleBaseController<TContext, TEntity> : Controller 
        where TContext : DbContext where TEntity : class, IWebArticle, new()
    {
        private readonly IWebObjectDataService<TContext> _dataService;

        public WebArticleBaseController(IWebObjectDataService<TContext> dataService)
        {
            _dataService = dataService;
        }

        public virtual async Task<IActionResult> Index()
        {
            var model = await _dataService.GetActiveAsync<TEntity>();

            if(model == null) return NotFound();

            return View(model);
        }

        public virtual async Task<IActionResult> Article(int? id)
        {
            if(id == null) return NotFound();

            var model = await _dataService.GetByIdAsync<TEntity>(id);

            if(model == null) return NotFound();

            return View(model);
        }

        public virtual async Task<IActionResult> Admin()
        {
            var model = await _dataService.GetAllAsync<TEntity>();

            if(model == null)
                return NotFound();

            return View(model);
        }

        public virtual IActionResult Create()
        {
            TEntity model = new TEntity();

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TEntity model)
        {
            _dataService.Create<TEntity>(model);
            await _dataService.SaveAsync();

            return RedirectToAction("Admin");
        }

        public virtual async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return NotFound();

            var model = await _dataService.GetByIdAsync<TEntity>(id);

            if(model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int? id, TEntity model)
        {
            if(id != (int)model.Id)
                return NotFound();

            _dataService.Update(model);
            await _dataService.SaveAsync();

            return RedirectToAction("Admin");

        }

        public virtual async Task<IActionResult> Delete(int? id)
        {
            if( id == null)
                return NotFound();

            var model = await _dataService.GetByIdAsync<TEntity>(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

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

        public virtual IActionResult FlushDeleted()
        {
            var model = new EXCSLA.Models.ConfirmDelete();

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> FlushDeleted(bool confirm)
        {
            if(confirm == true)
            {
                await _dataService.FlushDeletedAsync<TEntity>();
                await _dataService.SaveAsync();
            }

            return RedirectToAction("Admin");
        }
    }
}
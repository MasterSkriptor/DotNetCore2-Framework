using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EXCSLA.Models;
using EXCSLA.Services.DataServices;
using Microsoft.AspNetCore.Identity;

namespace EXCSLA.Controllers
{
    public class EntityBaseController<TContext, TEntity, TIdentityUser> : Controller 
        where TContext : DbContext 
        where TEntity : class, IEntity, new()
        where TIdentityUser : IdentityUser
    {
        private readonly IDataService<TContext> _dataService;
        private readonly UserManager<TIdentityUser> _userManager;

        public EntityBaseController(IDataService<TContext> dataService, UserManager<TIdentityUser> userManager)
        {
            _dataService = dataService;
            _userManager = userManager;
        }

        public virtual async Task<IActionResult> Index()
        {
            var model = await _dataService.GetAllAsync<TEntity>();

            if(model == null) return NotFound();

            return View(model);
        }

        public virtual async Task<IActionResult> Item(int? id)
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
            _dataService.Create<TEntity>(model, await GetUserNameAsync());
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

            _dataService.Update(model, await GetUserNameAsync());
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

        private async Task<string> GetUserNameAsync()
        {
            var user = await _userManager.GetUserAsync(this.User);
            return user.UserName;
        }
    }
}
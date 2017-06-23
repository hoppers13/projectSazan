using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectSazan.Persistence;
using Microsoft.AspNetCore.Identity;
using ProjectSazan.Web.Models;
using ProjectSazan.Domain;
using System;
using ProjectSazan.Web.Models.PhilatelyViewModels;

namespace ProjectSazan.Web.Controllers
{
	[Authorize]
    public class PhilatelyController : Controller
    {
		IPhilatelicCollectionRepository repository;
		UserManager<ApplicationUser> userManager;

		public PhilatelyController(
			 UserManager<ApplicationUser> userManager,
			 IPhilatelicCollectionRepository repository)
		{
			this.userManager = userManager;
			this.repository = repository;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
			var userIdentity = new UserIdentity { Id = userManager.GetUserName(HttpContext.User) };

			var model = await repository.GetCollectionNamesAsync(userIdentity);

            return View(model);
        }

        public IActionResult Collection(Guid id)
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCollection(AddCollection coll)
        {
            var userIdentity = new UserIdentity { Id = userManager.GetUserName(HttpContext.User) };

            await repository.CreateCollection(userIdentity, coll.NewCollection);

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult AddItem(PhilstelicItemViewModel item)
        {
            var a = item;

            return RedirectToAction("index");
        }


    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectSazan.Persistence;
using Microsoft.AspNetCore.Identity;
using ProjectSazan.Web.Models;
using ProjectSazan.Domain;
using System;
using ProjectSazan.Web.Models.PhilatelyViewModels;
using ProjectSazan.Domain.Philately;

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
			var userIdentity = GetUserIdentity();

			var model = await repository.GetCollectionNamesAsync(userIdentity);

            return View(model);
        }

        public async Task<IActionResult> Collection(Guid id)
        {
			var model = await repository.GetCollectionAsync(id);

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCollection(AddCollection coll)
        {
            var userIdentity = GetUserIdentity();

            await repository.CreateCollectionAsync(userIdentity, coll.NewCollection);

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(PhilatelicItemViewModel item)
        {
            var userIdentity = GetUserIdentity();

            var philatelicItem = new PhilatelicItem {
                Id = Guid.NewGuid(),
                CatalogueReference = new CatalogueReference {
                    Catalogue = (CataloguesInUse)Enum.Parse(typeof(CataloguesInUse), item.Catalogue),
                    Area = item.Area,
                    Number = item.Number
                },
                Year = item.Year,
                Description = item.Description,
                Paid = new Price
                {
                    Currency = (Currency)Enum.Parse(typeof(Currency), item.Currency),
                    Figure = item.Price
                },
                Acquired = DateTime.Parse(item.Acquired),
                Conditions = (Conditions)Enum.Parse(typeof(Conditions), item.Condition),
                Scans = new Scans()
            };

            await repository.AddPhilatelicItem(userIdentity, item.CollectionId, philatelicItem);

            return  RedirectToAction("collection", new { id = item.CollectionId } );
        }

        private UserIdentity GetUserIdentity()
        {
            return new UserIdentity { Id = userManager.GetUserName(HttpContext.User) };
        }

    }
}
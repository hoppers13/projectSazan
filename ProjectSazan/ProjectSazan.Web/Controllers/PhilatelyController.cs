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
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProjectSazan.Web.Controllers
{
	[Authorize]
    public class PhilatelyController : Controller
    {
        IHostingEnvironment hostingEnvironment;
        IPhilatelicCollectionRepository collectionRepository;
        IQuoteRepository quoteRepository;
        UserManager<ApplicationUser> userManager;

		public PhilatelyController(
            IHostingEnvironment hostingEnvironment,
			UserManager<ApplicationUser> userManager,
			IPhilatelicCollectionRepository collectionRepository,
            IQuoteRepository quoteRepository )
		{
            this.hostingEnvironment = hostingEnvironment;
			this.userManager = userManager;
			this.collectionRepository = collectionRepository;
            this.quoteRepository = quoteRepository;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
			var userIdentity = GetUserIdentity();

			var model = await collectionRepository.GetCollectionNamesAsync(userIdentity);

            return View(model);
        }

        public async Task<IActionResult> Collection(Guid id)
        {
			var model = await collectionRepository.GetCollectionAsync(id);

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCollection(AddCollection coll)
        {
            var userIdentity = GetUserIdentity();

            await collectionRepository.CreateCollectionAsync(userIdentity, coll.Title);

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(PhilatelicItemViewModel item, List<IFormFile> scans)
        {
            var userIdentity = GetUserIdentity();

            var thisCulture = new CultureInfo("en-GB");
            DateTime.TryParseExact(item.Acquired,
                                    "dd/MM/yyyy",
                                    thisCulture,
                                    DateTimeStyles.None,
                                    out DateTime dateAcquired);

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
                Acquired = dateAcquired == DateTime.MinValue ? DateTime.Now.Date : dateAcquired.Date,
                Conditions = (Conditions)Enum.Parse(typeof(Conditions), item.Condition),
                Scans = new Scans()
            };

            // handle uploaded files
            foreach(var scan in scans)
            {
                if(scan.Length > 0)
                {
                    var filename = $"{Guid.NewGuid()}.jpg"; //do not always assume it's a jpg

                    using(var stream = new FileStream($"{hostingEnvironment.WebRootPath}\\images\\scans\\{filename}", FileMode.Create))
                    {
                        await scan.CopyToAsync(stream);
                        philatelicItem.Scans.Add(new Scan { Image = $"/images/scans/{filename}", Caption = scan.FileName });
                    }                    
                }
            }
            
            await collectionRepository.AddPhilatelicItemAsync(userIdentity, item.CollectionId, philatelicItem);

            return  RedirectToAction("collection", new { id = item.CollectionId } );
        }

        [HttpPost]
        public async Task<IActionResult> Quote(Guid collectionId, IEnumerable<Guid> items)
        {
            var quote = new Quote
            {
                Id = Guid.NewGuid(),
                Collector = GetUserIdentity(),
                ItemsToInsure = items
            };

            await quoteRepository.AddIncompleteQuote(quote);

            var model = new CompleteQuoteRequest
            {
				QuoteToCompleteId = quote.Id,
                Collector = quote.Collector,
                Items = await collectionRepository.GetPhilatelicItemsAsync(quote.Collector, quote.ItemsToInsure),
                AllAvailableImsurers = await quoteRepository.GetAllInsurersAsync()                
            };
			
            return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> RequestQuote(Guid incompleteQuoteId, IEnumerable<Guid> items, IEnumerable<Guid> insurers)
		{
			return View();
		}

        private UserIdentity GetUserIdentity()
        {
            return new UserIdentity { Id = userManager.GetUserName(HttpContext.User) };
        }
    }
}
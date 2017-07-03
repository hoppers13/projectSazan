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
using ProjectSazan.Web.Persistence.FileSystem;

namespace ProjectSazan.Web.Controllers
{
	[Authorize]
    public class PhilatelyController : Controller
    {
        IHostingEnvironment hostingEnvironment;
        IPhilatelicCollectionRepository collectionRepository;
		IScanRepository scanRepository;
		IQuoteRepository quoteRepository;
		PhilatelicCollectionRepository fsRepository;
		UserManager<ApplicationUser> userManager;

		public PhilatelyController(
            IHostingEnvironment hostingEnvironment,
			UserManager<ApplicationUser> userManager,
			IPhilatelicCollectionRepository collectionRepository,
			IScanRepository scanRepository,
			IQuoteRepository quoteRepository )
		{
            this.hostingEnvironment = hostingEnvironment;
			this.userManager = userManager;
			this.collectionRepository = collectionRepository;
			this.scanRepository = scanRepository;
            this.quoteRepository = quoteRepository;
			fsRepository = new PhilatelicCollectionRepository(hostingEnvironment);
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
			var userIdentity = GetUserIdentity();

			// var model = await collectionRepository.GetCollectionNamesAsync(userIdentity);
			var model = await fsRepository.GetCollectionNamesAsync(userIdentity);

            return View(model);
        }

        public async Task<IActionResult> Collection(Guid id)
        {
			//var model = await collectionRepository.GetCollectionAsync(GetUserIdentity(), id);
			var model = await fsRepository.GetCollectionAsync(GetUserIdentity(), id);

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCollection(AddCollection coll)
        {
            var userIdentity = GetUserIdentity();
					
            // await collectionRepository.CreateCollectionAsync(userIdentity, coll.Title);
            await fsRepository.CreateCollectionAsync(userIdentity, coll.Title);

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
			
            foreach(var scan in scans)
            {
                if(scan.Length > 0)
                {
					var scanPath = await scanRepository.SaveCollectableScan(userIdentity, item.CollectionId, scan);
					philatelicItem.Scans.Add(new Scan { Image = $"/dataStorage{scanPath.Path}", Caption = scan.FileName });       
				}
            }
            
            //await collectionRepository.AddPhilatelicItemAsync(userIdentity, item.CollectionId, philatelicItem);
            await fsRepository.AddPhilatelicItemAsync(userIdentity, item.CollectionId, philatelicItem);

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
                //Items = await collectionRepository.GetPhilatelicItemsAsync(quote.Collector, quote.ItemsToInsure),
				Items = await fsRepository.GetPhilatelicItemsAsync(quote.Collector, quote.ItemsToInsure),
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
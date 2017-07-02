using ProjectSazan.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectSazan.Domain;
using ProjectSazan.Domain.Philately;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProjectSazan.Web.Persistence.FileSystem
{
	public class PhilatelicCollectionRepository : IPhilatelicCollectionRepository
	{
		private string webRoot;

		public PhilatelicCollectionRepository(IHostingEnvironment hostingEnvironment)
		{
			this.webRoot = hostingEnvironment.WebRootPath;
		}
		
		public Task CreateCollectionAsync(UserIdentity collector, string newCollection)
		{
			var collection = new PhilatelicCollection { Id = Guid.NewGuid(), CollectorId = collector.Id, Title = newCollection };
			var jsonDocument = JsonConvert.SerializeObject(collection);

			var documentName = $"{collection.Id}.json";
			var paths = PersistencePathCreator.CreateCollectionDocumentPath(collector, collection.Id, documentName);

			var collectionSummaryPath = $"{webRoot}\\dataStorage\\{collector.Id}\\collections.json";

			var directory = $"{webRoot}{paths.DirectoryPath}";

			return Task.Run(() =>
			{				
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				using (var streamWriter = new StreamWriter(File.Create($"{webRoot}{paths.FilestreamPath}")))
				{
					streamWriter.Write(jsonDocument);
				}

				// update colletions summary
				CollectionsSummary summary;
				if (File.Exists(collectionSummaryPath))
				{
					using (var streamReader = new StreamReader(new FileStream(collectionSummaryPath, FileMode.Open)))
					{
						summary = JsonConvert.DeserializeObject<CollectionsSummary>(streamReader.ReadToEnd());
						summary.Collections.Add(new ColletionSummary { Id = collection.Id, Title = collection.Title });
					}
				}
				else
				{
					summary = new CollectionsSummary
					{
						Collector = collector,
						Collections = new List<ColletionSummary>
						{
							new ColletionSummary{Id = collection.Id, Title = collection.Title}
						}
					};
				}

				using (var streamWriter = new StreamWriter(File.Create(collectionSummaryPath)))
				{
					streamWriter.Write(JsonConvert.SerializeObject(summary));
				}
			});			
		}


		public Task AddPhilatelicItemAsync(UserIdentity userIdentity, Guid collectionId, PhilatelicItem philatelicItem)
		{
			throw new NotImplementedException();
		}

		public Task<IPhilatelicCollection> GetCollectionAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collectorId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<PhilatelicItem>> GetPhilatelicItemsAsync(UserIdentity collector, IEnumerable<Guid> itemsToInsure)
		{
			throw new NotImplementedException();
		}
	}
}

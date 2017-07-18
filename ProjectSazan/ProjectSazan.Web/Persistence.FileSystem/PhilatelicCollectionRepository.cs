using ProjectSazan.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectSazan.Domain;
using ProjectSazan.Domain.Philately;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

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
			var collection = new PhilatelicCollection { Id = Guid.NewGuid(), CollectorId = collector.Id, Title = newCollection, Items = new List<PhilatelicItem>() };
			var jsonDocument = JsonConvert.SerializeObject(collection);

			var documentName = $"{collection.Id}.json";
			var paths = PersistencePathCreator.CreateCollectionDocumentPath(collector, collection.Id, documentName);

			var collectionSummaryPath = $"{webRoot}{PersistencePathCreator.GetCollectionSummaryPersistencePath(collector)}";

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

		public Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collector)
		{
			var path = $"{webRoot}{PersistencePathCreator.GetCollectionSummaryPersistencePath(collector)}";

			return Task.Run(() =>
			{
				if (!File.Exists(path)) return (new List<IPhilatelicCollection>() as IEnumerable<ICollectableCollection>);
				
				using (var streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
				{
					var summary = JsonConvert.DeserializeObject<CollectionsSummary>(streamReader.ReadToEnd());
					var result = new List<IPhilatelicCollection>();

					foreach(var coll in summary.Collections)
					{
						result.Add(new PhilatelicCollection { Id = coll.Id, Title = coll.Title, CollectorId = collector.Id });
					}

					return result;
				}
			});			
		}

		public Task<IPhilatelicCollection> GetCollectionAsync(UserIdentity collector, Guid collectionId)
		{			
			var path = $"{webRoot}{PersistencePathCreator.GetCollectionPersistencePath(collector, collectionId)}";

			return Task.Run(() =>
			{
				if (!File.Exists(path)) throw new Exception("could not find required collection");

				using (var streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
				{
					return JsonConvert.DeserializeObject<PhilatelicCollection>(streamReader.ReadToEnd()) as IPhilatelicCollection; 
				}
			});			
		}
        
		public Task<IEnumerable<PhilatelicItem>> GetPhilatelicItemsAsync(UserIdentity collector, IEnumerable<Guid> itemsToInsure)
		{
			var path = $"{webRoot}{PersistencePathCreator.GetCollectionSummaryPersistencePath(collector)}";

			return Task.Run(() =>
			{
				if (!File.Exists(path)) return new List<PhilatelicItem>() as IEnumerable<PhilatelicItem>;

				var result = new List<PhilatelicItem>();
				CollectionsSummary summary;
				using (var streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
				{
					summary = JsonConvert.DeserializeObject<CollectionsSummary>(streamReader.ReadToEnd());										
				};

				foreach (var coll in summary.Collections)
				{
					var persistedCollection = GetCollectionAsync(collector, coll.Id).Result;
					result.AddRange(persistedCollection.Items.Where(i => itemsToInsure.Contains(i.Id)));
				}

				return result;
			});
		}

        public Task SavePhilatelicItemAsync(UserIdentity collector, Guid collectionId, PhilatelicItem philatelicItem)
        {
            var path = $"{webRoot}{PersistencePathCreator.GetCollectionPersistencePath(collector, collectionId)}";

            PhilatelicCollection collection;

            return Task.Run(() => {

                if (!File.Exists(path)) throw new Exception("could not find required collection");

                using (var streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    collection = JsonConvert.DeserializeObject<PhilatelicCollection>(streamReader.ReadToEnd());
                }

                var item = collection.Items.SingleOrDefault(itm => itm.Id == philatelicItem.Id);

                if(item == null)
                {
                    // add new item
                    collection.Items.Add(philatelicItem);
                }
                else
                {
                    // keep previous scans if no new scans provided
                    if (!philatelicItem.Scans.Any())
                    {
                        philatelicItem.Scans = item.Scans;
                    }

                    // update existing item
                    var index = collection.Items.IndexOf(item);
                    collection.Items.RemoveAt(index);
                    collection.Items.Insert(index, philatelicItem);                    
                }

                using (var streamWriter = new StreamWriter(File.Create(path)))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(collection));
                }

            });            
        }
    }
}

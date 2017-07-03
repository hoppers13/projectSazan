using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectSazan.Domain;
using ProjectSazan.Domain.Philately;
using System.Linq;

namespace ProjectSazan.Persistence.InMemory
{
	public class PhilatelicCollectionRepository : IPhilatelicCollectionRepository
	{
        public Task AddPhilatelicItemAsync(UserIdentity userIdentity, Guid collectionId, PhilatelicItem philatelicItem)
        {
            return Task.Run(() => {
                if(InMemoryStore.CollectorColletions[userIdentity.Id].SingleOrDefault(collId => collId == collectionId) == null)
                {
                    throw new Exception("This is not one of the collector's collections");
                }

                InMemoryStore.PhilatelicCollections[collectionId].Items.Add(philatelicItem);
            });
        }

        public Task CreateCollectionAsync(UserIdentity userIdentity, string newCollection)
        {
            var collection = new PhilatelicCollection { Id = Guid.NewGuid(), CollectorId = userIdentity.Id, Title = newCollection }; 

            return Task.Run(() =>
            {
				InMemoryStore.PhilatelicCollections.Add(collection.Id, collection);

                if (InMemoryStore.CollectorColletions.ContainsKey(userIdentity.Id))
                {
                    InMemoryStore.CollectorColletions[userIdentity.Id].Add(collection.Id);                    
                }
                else
                {
                    InMemoryStore.CollectorColletions.Add(userIdentity.Id, new List<Guid> { collection.Id });
                }                
            });            
        }

		public Task<IPhilatelicCollection> GetCollectionAsync(UserIdentity collector, Guid id)
		{
			// collector not needed here - given how data are organised in memeory
			return Task.Run(() =>  InMemoryStore.PhilatelicCollections[id]);
		}

		public Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collectorId)
		{
			return Task.Run(() => InMemoryStore.CollectorColletions[collectorId.Id]
										.Select(id => InMemoryStore.PhilatelicCollections[id]) as IEnumerable<ICollectableCollection>);			
		}

        public Task<IEnumerable<PhilatelicItem>> GetPhilatelicItemsAsync(UserIdentity collector, IEnumerable<Guid> ids)
        {
            return Task.Run(() =>
            {
                List<PhilatelicItem> result = new List<PhilatelicItem>();
                var collections = InMemoryStore.CollectorColletions[collector.Id].Select(collectionId =>
                                                                InMemoryStore.PhilatelicCollections[collectionId]);
                foreach (var collection in collections)
                {
                    if (collection.Items == null) continue;
                    result.AddRange(collection.Items.Where(item => ids.Contains(item.Id)));
                }

                return result as IEnumerable<PhilatelicItem>;
            });            
        }        
    }
}
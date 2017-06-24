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

		public Task<IPhilatelicCollection> GetCollectionAsync(Guid id)
		{
			return Task.Run(() =>  InMemoryStore.PhilatelicCollections[id]);
		}

		public Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collectorId)
		{
			return Task.Run(() => InMemoryStore.CollectorColletions[collectorId.Id]
										.Select(id => InMemoryStore.PhilatelicCollections[id]) as IEnumerable<ICollectableCollection>);			
		}
	}
}
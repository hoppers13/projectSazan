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
        public Task CreateCollection(UserIdentity userIdentity, string newCollection)
        {
            var collection = new PhilatelicCollection { Id = Guid.NewGuid(), CollectorId = userIdentity.Id, Title = newCollection }; 

            return Task.Run(() =>
            {
                if (InMemoryStore.PhilatelicCollections.ContainsKey(userIdentity.Id))
                {
                    var collections = InMemoryStore.PhilatelicCollections[userIdentity.Id].ToList();
                    collections.Add(collection);

                    InMemoryStore.PhilatelicCollections[userIdentity.Id] = collections;
                }
                else
                {
                    InMemoryStore.PhilatelicCollections.Add(userIdentity.Id, new List<PhilatelicCollection> { collection });
                }
                
            });            
        }

        public Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collectorId)
		{
			return Task.Run(() => InMemoryStore.PhilatelicCollections[collectorId.Id] as IEnumerable<ICollectableCollection>);			
		}
	}
}
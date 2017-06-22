using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectSazan.Domain;

namespace ProjectSazan.Persistence.InMemory
{
	public class PhilatelicCollectionRepository : IPhilatelicCollectionRepository
	{		
		public Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collectorId)
		{
			return Task.Run(() => InMemoryStore.PhilatelicCollections[collectorId.Id] as IEnumerable<ICollectableCollection>);			
		}
	}
}
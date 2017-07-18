using ProjectSazan.Domain;
using ProjectSazan.Domain.Philately;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSazan.Persistence
{
    public interface IPhilatelicCollectionRepository
    {
		Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collector);
        Task CreateCollectionAsync(UserIdentity collector, string newCollection);
		Task<IPhilatelicCollection> GetCollectionAsync(UserIdentity collector, Guid id);
        Task<IEnumerable<PhilatelicItem>> GetPhilatelicItemsAsync(UserIdentity collector, IEnumerable<Guid> itemsToInsure);
        Task SavePhilatelicItemAsync(UserIdentity collector, Guid collectionId, PhilatelicItem philatelicItem);
	    Task RemovePhilatelicItemAsync(UserIdentity collector, Guid collectionId, Guid itemId);
    }
}

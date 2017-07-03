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
        Task AddPhilatelicItemAsync(UserIdentity collector, Guid collectionId, PhilatelicItem philatelicItem);
        Task<IEnumerable<PhilatelicItem>> GetPhilatelicItemsAsync(UserIdentity collector, IEnumerable<Guid> itemsToInsure);
    }
}

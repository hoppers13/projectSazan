using ProjectSazan.Domain;
using ProjectSazan.Domain.Philately;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSazan.Persistence
{
    public interface IPhilatelicCollectionRepository
    {
		Task<IEnumerable<ICollectableCollection>> GetCollectionNamesAsync(UserIdentity collectorId);
        Task CreateCollectionAsync(UserIdentity userIdentity, string newCollection);
		Task<IPhilatelicCollection> GetCollectionAsync(Guid id);
        Task AddPhilatelicItem(UserIdentity userIdentity, Guid collectionId, PhilatelicItem philatelicItem);
    }
}

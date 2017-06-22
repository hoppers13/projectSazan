using ProjectSazan.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSazan.Persistence
{
    public interface IPhilatelicCollectionRepository
    {
		Task<IEnumerable<string>> GetCollectionNamesAsync(UserIdentity collectorId);
    }
}

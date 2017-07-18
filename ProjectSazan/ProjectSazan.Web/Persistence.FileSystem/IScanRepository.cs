using Microsoft.AspNetCore.Http;
using ProjectSazan.Domain;
using System;
using System.Threading.Tasks;

namespace ProjectSazan.Web.Persistence.FileSystem
{
	public interface IScanRepository
	{
		Task<ScanPath> SaveCollectableScan(UserIdentity colletor, Guid collectionId, IFormFile scan);
		Task RemoveScan(UserIdentity collector, Guid collectionId, string filename);
	}

}

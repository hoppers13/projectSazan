using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProjectSazan.Domain;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectSazan.Web.Persistence.FileSystem
{
	public class ScanRepository : IScanRepository
	{
		private string webRoot;

		public ScanRepository(IHostingEnvironment hostingEnvironment)
		{
			this.webRoot = hostingEnvironment.WebRootPath;
		}

		public async Task<ScanPath> SaveCollectableScan(UserIdentity collector, Guid collectionId, IFormFile scan)
		{
			var filename = $"{Guid.NewGuid()}.jpg";  //TODO: do not assume file will be a jpg
			
			var paths = PersistencePathCreator.CreateCollectableScanPath(collector, collectionId, filename);
			var scanPath = new ScanPath { Path = paths.PathToPersist };

			var directory = $"{webRoot}{paths.DirectoryPath}";
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			using (var stream = new FileStream($"{webRoot}{paths.FilestreamPath}", FileMode.Create))
			{
				await scan.CopyToAsync(stream);
			};

			return scanPath;
		}

		public Task RemoveScan(UserIdentity collector, Guid collectionId, string filename)
		{
			var toDelete = $"{webRoot}{filename}";

			return Task.Run(() =>
			{
				if (!File.Exists(toDelete)) return;

				File.Delete(toDelete);
			});

			
		}
	}
}

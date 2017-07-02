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
			var filename = $"{Guid.NewGuid()}.jpg"; //TODO: do not assume file will be a jpg
			var scanPath = new ScanPath { Path = $"/{collector.Id}/{collectionId}/scans/{filename}" };

			var fullPath = $"{webRoot}\\dataStorage\\{collector.Id}\\{collectionId}\\scans";
			if (!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(fullPath);
			}

			using (var stream = new FileStream($"{webRoot}\\dataStorage\\{collector.Id}\\{collectionId}\\scans\\{filename}", FileMode.Create))
			{
				await scan.CopyToAsync(stream);
			};

			return scanPath;
		}		
	}
}

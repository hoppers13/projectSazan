using ProjectSazan.Domain;
using System;

namespace ProjectSazan.Web.Persistence.FileSystem
{
	public static class PersistencePathCreator
    {
		internal static PersistencePath CreateCollectableScanPath(UserIdentity collector, Guid collectionId, string filename)
		{
			return new PersistencePath
			{
				DirectoryPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\scans",
				FilestreamPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\scans\\{filename}",
				PathToPersist = $"/{collector.Id}/{collectionId}/scans/{filename}"
			};
		}

		internal static PersistencePath CreateCollectionDocumentPath(UserIdentity collector, Guid collectionId, string documentName)
		{
			return new PersistencePath
			{
				DirectoryPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\",
				FilestreamPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\{documentName}"
			};
		}
	}
}